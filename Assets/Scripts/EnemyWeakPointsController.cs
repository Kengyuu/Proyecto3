using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeakPointsController : MonoBehaviour
{
    public List<Transform> weakPointsList;
    List<int> spawnersUsed = new List<int>();
    public int weakPointsMax = 3;
    public int currentWeakPoints = 0;
    public GameObject[] weakPoint;
    // Start is called before the first frame update
    /*void Awake()
    {
        foreach (Transform weakPoint in GetComponentInChildren<Transform>().transform)
        {
            weakPointsList.Add(weakPoint);
        }
    }*/
    void Start()
    {
        SpawnWeakPoints(weakPointsMax-currentWeakPoints);
    }

    // Update is called once per frame
    void Update()
    {
        /*if(Input.GetMouseButtonDown(0))
        {
            for (int i = 0; i < weakPoint.Length; i++)
            {
                TakeDamage(weakPoint[i]);
            }
            /*currentWeakPoints = 0;
            spawnersUsed.Clear();
            for (int i = 0; i < weakPoint.Length; i++)
            {
                weakPoint[i].SetActive(false);
            }
            SpawnWeakPoints(weakPointsMax - currentWeakPoints);
        }*/
    }

    public void TakeDamage(GameObject wp)
    {
        if(currentWeakPoints > 0)
        {
            currentWeakPoints--;
            spawnersUsed.Remove(wp.GetComponent<WeakPoint>().spawnPosition);
        }

        if(currentWeakPoints == 0)
        {
            GetStunned();
        }
    }

    public void GetStunned()
    {
        Debug.Log("No puto, me estuniaste");
    }

    public void SpawnWeakPoints(int numberWeakPoints)
    {
        int spawnPosition = -1;
        for (int i = 0; i < numberWeakPoints; i++)
        {
            bool spawnable = false;
            while(spawnable == false)
            {
                bool canSpawn = true;

                spawnPosition = Random.Range(0, weakPointsList.Count);
                if(spawnersUsed.Count > 0)
                {
                    for (int j = 0; j < spawnersUsed.Count; j++)
                    {
                        if(spawnPosition == spawnersUsed[j])
                            canSpawn = false;
                    }
                }
                

                if(canSpawn)
                    spawnable = true;
            }
            spawnersUsed.Add(spawnPosition);
            weakPoint[i].transform.position = weakPointsList[spawnersUsed[i]].position;
            weakPoint[i].GetComponent<WeakPoint>().spawnPosition = spawnersUsed[i];
            weakPoint[i].SetActive(true);
            currentWeakPoints++;
            //Instantiate(weakPoint, weakPointsList[spawnersUsed[i]].position, Quaternion.identity);
        }
    }
}
