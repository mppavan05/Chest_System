using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chests : MonoBehaviour
{
    public ScritableObject chestData;
    int minCoins;
    int maxCoins;
    int minGems;
    int maxGems;
    int timeTaken;
    // Start is called before the first frame update
    void Start()
    {
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
