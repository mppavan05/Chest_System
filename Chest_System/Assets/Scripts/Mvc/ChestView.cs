using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestView : MonoBehaviour
{
    private ChestController _chestController;
    [HideInInspector]
    public Slot slotReference;

    [SerializeField] private Sprite EmptySlotSprite;

    [SerializeField] public Text chestTimerTxt;
    [SerializeField] private Image chestSlotSprite;
    [SerializeField] private Text chestTypeTxt;
    [SerializeField] private Image coinImage;
    [SerializeField] private Text coinsTxt;
    [SerializeField] private Image gemImage;
    [SerializeField] private Text gemsTxt;

    [SerializeField] private Button ChestButton;

    private ChestState currentState;

    public void SetControllerReference(ChestController chestController)
    {
        this._chestController = chestController;
    }

    private void Start()
    {
        InitializeEmptyChestView();
    }

    private void InitializeEmptyChestView()
    {
        chestTimerTxt.gameObject.SetActive(false);
        chestSlotSprite.sprite = EmptySlotSprite;
        chestTypeTxt.gameObject.SetActive(false);
        coinImage.gameObject.SetActive(false);
        coinsTxt.gameObject.SetActive(false);
        gemImage.gameObject.SetActive(false);
        gemsTxt.gameObject.SetActive(false);
        ChestButton.enabled = false;
        currentState = ChestState.None;
    }

    public void InitialiseViewUIForLockedChest()
    {
        chestTimerTxt.gameObject.SetActive(false);
        chestSlotSprite.sprite = _chestController.chestModel.ChestSprite;
        chestTypeTxt.gameObject.SetActive(true);
        chestTypeTxt.text = _chestController.chestModel.ChestName;
        coinImage.gameObject.SetActive(true);
        coinsTxt.gameObject.SetActive(true);
        coinsTxt.text = _chestController.chestModel.CoinCost.ToString();
        gemImage.gameObject.SetActive(true);
        gemsTxt.gameObject.SetActive(true);
        gemsTxt.text = _chestController.GetGemCost().ToString();
        ChestButton.enabled = true;
        currentState = ChestState.Locked;
    }

    private void InitialiseViewUIForUnlockingChest()
    {
        chestTimerTxt.gameObject.SetActive(true);
        chestSlotSprite.sprite = _chestController.chestModel.ChestSprite;
        chestTypeTxt.gameObject.SetActive(true);
        chestTypeTxt.text = _chestController.chestModel.ChestName;
        coinImage.gameObject.SetActive(false);
        coinsTxt.gameObject.SetActive(false);
        gemImage.gameObject.SetActive(false);
        gemsTxt.gameObject.SetActive(false);
        ChestButton.enabled = false;
        currentState = ChestState.Unlocking;
    }

    private void InitialiseViewUIForUnlockedChest()
    {
        chestTimerTxt.gameObject.SetActive(true);
        chestSlotSprite.sprite = _chestController.chestModel.ChestSprite;
        chestTypeTxt.gameObject.SetActive(true);
        chestTypeTxt.text = _chestController.chestModel.ChestName;
        coinImage.gameObject.SetActive(false);
        coinsTxt.gameObject.SetActive(false);
        gemImage.gameObject.SetActive(false);
        gemsTxt.gameObject.SetActive(false);
        ChestButton.enabled = true;
        currentState = ChestState.Unlocked;
    }


    public void OnClickChestButton()
    {
        if (currentState == ChestState.Locked)
        {
            if (SlotsController.Instance.isUnlocking)
            {
                UIHandler.Instance.ToggleIsBusyUnlockingPopup(true);
            }
            else
            {
                ChestService.Instance.selectedController = _chestController;
                UIHandler.Instance.ToggleUnlockChestPopup(true);
            }
        }
        else if (currentState == ChestState.Unlocking)
        {
          // for popup
        }
        else if (currentState == ChestState.Unlocked)
        {
            ChestService.Instance.selectedController = _chestController;
            OpenChest();
            ChestService.Instance.ToggleRewardsPopup(true);
        }
    }


    public void EnteringUnlockingState()
    {
        SlotsController.Instance.isUnlocking = true;
        InitialiseViewUIForUnlockingChest();
        StartCoroutine(_chestController.StartTimer());

    }

    public void OpenInstantly()
    {
        InitializeEmptyChestView();
        ReceiveChestRewards();
        ChestService.Instance.selectedController = _chestController;
        slotReference.isEmpty = true;
        ChestService.Instance.ToggleRewardsPopup(true);
        slotReference.chestController = null;
    }

    public void EnteringUnlockedState()
    {
        SlotsController.Instance.isUnlocking = false;
        InitialiseViewUIForUnlockedChest();
        chestTimerTxt.text = "OPEN!";
    }

    private void OpenChest()
    {
        InitializeEmptyChestView();
        ReceiveChestRewards();
        slotReference.isEmpty = true;
        slotReference.chestController = null;
    }

    private void ReceiveChestRewards()
    {
        ResourceHandler.Instance.IncreaseCoins(_chestController.chestModel.CoinsReward);
        ResourceHandler.Instance.IncreaseGems(_chestController.chestModel.GemsReward);
    }


}
