using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChestManager : MonoBehaviour
{
    public Transform[] slots;
    GameObject[] chestList;
    public GameObject commonChest;
    public GameObject rareChest;
    public GameObject epicChest;
    public GameObject legendaryChest;
    int numCoins = 0;
    int numGems = 0;
    public Text coinText;
    public Text gemText;

    void Start()
    {
        chestList = new GameObject[4];
    }
    public void SpawnChests()
    {
        for (int i = 0; i < 4; i++)
        {
            if (chestList[i] == null)
            {
                int value = Random.Range(1, 51);
                if (value >= 1 && value <= 35)
                    chestList[i] = Instantiate(commonChest, slots[i].position - new Vector3(0, 0, 1), Quaternion.identity);
                else if (value >= 36 && value <= 45)
                    chestList[i] = Instantiate(rareChest, slots[i].position - new Vector3(0, 0, 1), Quaternion.identity);
                else if (value >= 46 && value <= 49)
                    chestList[i] = Instantiate(epicChest, slots[i].position - new Vector3(0, 0, 1), Quaternion.identity);
                else
                    chestList[i] = Instantiate(legendaryChest, slots[i].position - new Vector3(0, 0, 1), Quaternion.identity);
            }
        }
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                if (hit.transform.gameObject.tag == "Chest")
                {
                    numCoins += hit.transform.gameObject.GetComponent<Chests>().rewardCoins();
                    numGems += hit.transform.gameObject.GetComponent<Chests>().rewardGems();
                    Destroy(hit.transform.gameObject);
                }
            }
        }
        coinText.text = numCoins.ToString();
        gemText.text = numGems.ToString();
    }

}
