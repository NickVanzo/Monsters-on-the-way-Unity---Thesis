using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrop : MonoBehaviour
{
    [SerializeField] GameObject[] rewards;

    public void CreateDrop()
    {
        int indexOfItem = Random.Range(0, rewards.Length);
        Instantiate(rewards[indexOfItem], transform.position, transform.rotation);
    }
}
