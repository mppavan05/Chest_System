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
    public Text TimerText;
    float currentTime = 0;
    float startingTime = 0;

    void Start()
    {
        currentTime = startingTime;
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
                {
                    chestList[i] = Instantiate(commonChest, slots[i].position - new Vector3(0, 0, 1), Quaternion.identity);
                    Debug.Log("Common chest");

                }
                else if (value >= 36 && value <= 45)
                {
                    chestList[i] = Instantiate(rareChest, slots[i].position - new Vector3(0, 0, 1), Quaternion.identity);
                    Debug.Log("rear chest");
                }
                else if (value >= 46 && value <= 49)
                {
                    chestList[i] = Instantiate(epicChest, slots[i].position - new Vector3(0, 0, 1), Quaternion.identity);
                    Debug.Log("epic chest");
                }
                else
                {
                    chestList[i] = Instantiate(legendaryChest, slots[i].position - new Vector3(0, 0, 1), Quaternion.identity);
                    Debug.Log("legendary chest");
                }
            }
        }
    }
    void Update()
    {
        displayTime();
        DestroyChest();

    }


    void displayTime()
    {
        currentTime += 1 * Time.deltaTime;
        TimerText.text = currentTime.ToString("00");

        if(currentTime <= 0)
        {
            currentTime = 0;
        }

    }


    void DestroyChest()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                if (hit.transform.gameObject.tag == "Common")
                {
                    numCoins += hit.transform.gameObject.GetComponent<Chests>().rewardCoins();
                    numGems += hit.transform.gameObject.GetComponent<Chests>().rewardGems();
                    Destroy(hit.transform.gameObject, 2f);
                }

                else if (hit.transform.gameObject.tag == "Rare")
                {
                    numCoins += hit.transform.gameObject.GetComponent<Chests>().rewardCoins();
                    numGems += hit.transform.gameObject.GetComponent<Chests>().rewardGems();
                    Destroy(hit.transform.gameObject, 4f);
                }

                else if (hit.transform.gameObject.tag == "Epic")
                {
                    numCoins += hit.transform.gameObject.GetComponent<Chests>().rewardCoins();
                    numGems += hit.transform.gameObject.GetComponent<Chests>().rewardGems();
                    Destroy(hit.transform.gameObject, 6f);
                }

                else if (hit.transform.gameObject.tag == "Legend")
                {
                    numCoins += hit.transform.gameObject.GetComponent<Chests>().rewardCoins();
                    numGems += hit.transform.gameObject.GetComponent<Chests>().rewardGems();
                    Destroy(hit.transform.gameObject, 8f);
                }
            }
        }
        coinText.text = numCoins.ToString();
        gemText.text = numGems.ToString();

    }

    IEnumerator waiting()
    {
        yield return new WaitForSeconds(10);
    }

  
    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Common")
        {
            Destroy(gameObject, 5f);
        }
    }


    private void OnMouseDown()
    {
        if (gameObject.tag == "Common")
        {
            Destroy(gameObject, 5f);
        }

    }*/

}
