using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCouple : MonoBehaviour
{
    PlayerStats playerStats;
    [SerializeField] int valueOfGold = 2;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
    }

    //If the player is full health then add life to its max health points
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerStats.AddGold(valueOfGold);
            Destroy(gameObject);
        }
    }

}
