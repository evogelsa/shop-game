using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedSpawner : MonoBehaviour
{
    public GameObject[] peds;
    private GameObject ped;

    private float maxTimeBetweenSpawn = 2f;
    private float minTimeBetweenSpawn = 0.2f;
    private float timeBetweenSpawn = 0f;

    void FixedUpdate()
    {
        if (timeBetweenSpawn <= 0)
        {
            ped = peds[Random.Range(0, peds.Length)];
            Instantiate(ped, transform.position, Quaternion.identity);
            timeBetweenSpawn = Random.Range(minTimeBetweenSpawn, maxTimeBetweenSpawn);
        }
        else
        {
            timeBetweenSpawn -= Time.deltaTime;
        }
    }
}
