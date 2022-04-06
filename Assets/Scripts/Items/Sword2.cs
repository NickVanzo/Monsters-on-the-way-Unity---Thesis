using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword2 : MonoBehaviour
{
    PlayerStats playerStats;
    [SerializeField] int damageToAddToPlayer = 1;
    [SerializeField] int healthToRemoveToPlayer = -1;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
    }

    //If the player is full health then add life to its max health points
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerStats.AddDamageStat(damageToAddToPlayer);
            playerStats.AddMaxHealth(healthToRemoveToPlayer);
            Destroy(gameObject);
        }
    }
}
