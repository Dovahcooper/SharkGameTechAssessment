using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishpool : MonoBehaviour
{
    public List<GameObject> fishList;

    public GameObject pooledObj;

    public int poolSize = 10;

    void Awake()
    {
        fishList = new List<GameObject>();
        for(int i = 0; i < poolSize; i++)
        {
            GameObject temp = Instantiate(pooledObj);
            Fish modifier = temp.GetComponent<Fish>();
            //modifier.startPos = new Vector3(Random.Range(-40f, 40f), Random.Range(-40f, 40f), Random.Range(-40f, 40f));
            
            fishList.Add(temp);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetFish()
    {
        foreach(GameObject f in fishList)
        {
            f.SetActive(false);
            f.SetActive(true);
        }
    }

    public void Repopulate()
    {
        foreach(GameObject f in fishList)
        {
            if (!f.activeInHierarchy)
            {
                f.SetActive(true);
            }
        }
    }
}
