using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chests : MonoBehaviour
{
    public float TimeLeft;
    public bool TimerOn = false;

    public Text TimerTxt;
    public ScritableObject chestData;
    int minCoins;
    int maxCoins;
    int minGems;
    int maxGems;
    int timeTaken;
    // Start is called before the first frame update
    void Start()
    {
        TimerOn = true;
        minCoins = chestData.minCoins;
        maxCoins = chestData.maxCoins;
        minGems = chestData.minGems;
        maxGems = chestData.maxGems;
        timeTaken = chestData.timeTaken;
    }


    public int rewardCoins()
    {
        return Random.Range(minCoins, maxCoins + 1);
    }
    public int rewardGems()
    {
        return Random.Range(minGems, maxGems + 1);
    }
}
