using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombspawnscript : MonoBehaviour
{
public GameObject bombPrefab;
public float spawnTime = 5f;
public float minY = 5f;
public float maxY = 10f;
public float minX = -5f;
public float maxX = 5f;

void Start()
{
    InvokeRepeating("SpawnBomb", spawnTime, spawnTime);
}

void SpawnBomb()
{
    float x = Random.Range(minX, maxX);
    float y = Random.Range(minY, maxY);
    Vector3 spawnPosition = new Vector3(x, y, 0);
    Instantiate(bombPrefab, spawnPosition, Quaternion.identity);
}

}
