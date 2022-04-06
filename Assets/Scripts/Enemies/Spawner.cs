using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject[] enemies;

    private void Start()
    {
        Instantiate(enemies[Random.Range(0,enemies.Length)], transform.position, transform.rotation);
    }
}
