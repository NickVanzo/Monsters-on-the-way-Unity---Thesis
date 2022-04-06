using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] GameObject[] level;
    [SerializeField] GameObject pointersToCheckDistanceFromPlayer;
    PlayerStats playerStats;

    List<GameObject> pointersSpawed;

    float startingPointForMapSpawn = -13.00f;
    float startingPointForPointers = 4f;
    float offsetForLevels = 25f;

    int counterForPointersSpawed = 0;
    int distanceBetweenEachPoint = 25;
    

    // Start is called before the first frame update
    void Start()
    {
        pointersSpawed = new List<GameObject>();
        SpawnPointToCheckDistanceFromPlayer();
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        
    }

    private void Update()
    {
        if (PlayerIsCloseToLastPointSpawned())
        {
            SpawnPointToCheckDistanceFromPlayer();
            SpawnLevel();
        }
    }

    bool PlayerIsCloseToLastPointSpawned()
    {
        int sizeOfArray = pointersSpawed.Count;
        GameObject lastPointSpawned = pointersSpawed[sizeOfArray - 1];
        return CalculateDistanceFromPlayer(lastPointSpawned) < distanceBetweenEachPoint;
    }


    float CalculateDistanceFromPlayer(GameObject pointerSpawned)
    {
        Vector2 playerCoordinates = playerStats.transform.position;
        //The distance between two points 2D in space √[(x₂ - x₁)² + (y₂ - y₁)²].
        float distanceFromPlayer = Mathf.Sqrt(Mathf.Pow((playerCoordinates.x - pointerSpawned.transform.position.x), 2) + Mathf.Pow((playerCoordinates.y - pointerSpawned.transform.position.y), 2));
        return distanceFromPlayer;
    }

    void SpawnPointToCheckDistanceFromPlayer()
    {
        try
        {
            if(pointersToCheckDistanceFromPlayer == null)
            {
                throw new Exception("PointersToCheckDistanceFromPlayer is null");
            }
            Vector2 spawnPosition = new Vector2(startingPointForPointers, 0);
            GameObject pointSpawned = Instantiate(pointersToCheckDistanceFromPlayer, spawnPosition, transform.rotation);
            pointersSpawed.Add(pointSpawned);
            startingPointForPointers += distanceBetweenEachPoint;
            counterForPointersSpawed++;
            Debug.Log("Point spawned");
        } catch(Exception e)
        {
            Debug.LogError("In SpawnPointToCheckDistanceFromPlayer function: " + e.Message);
        }
        
    }

    void SpawnLevel()
    {
        Vector2 spawnPosition = new Vector2(startingPointForMapSpawn, 0);
        GameObject levelSpawned = Instantiate(level[UnityEngine.Random.Range(0, 2)], spawnPosition, transform.rotation);
        levelSpawned.transform.SetParent(GameObject.Find("Grid").transform, true);
        startingPointForMapSpawn += offsetForLevels;
    }
}
