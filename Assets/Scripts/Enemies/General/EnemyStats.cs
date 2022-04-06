using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour, IEnemy
{
    Animator animator;
    Rigidbody2D rb;
    PlayerStats playerStats;

    int currentHealth;

    [SerializeField] int maxHealth;
    [SerializeField] float secondsToWaitAfterAttack = 0.5f;
    [SerializeField] int damage;


    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    public void Attack()
    {
        throw new System.NotImplementedException();
    }

    public void Cure(int amount)
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += amount;
        }
    }

    public void Die()
    {
        GetComponent<EnemyDrop>().CreateDrop();
        Destroy(gameObject);
    }

    public RewardType GiveReward()
    {
        throw new System.NotImplementedException();
    }

    public void Move()
    {
        throw new System.NotImplementedException();
    }

    public void TakeDamage(int dmg)
    {
        currentHealth -= dmg;

        if (currentHealth > 0)
        {
            ApplyKickbackEffect();
            animator.SetBool("isHit", true);
            StartCoroutine(DisableHitAnimation());
        } else
        {
            Die();
        }
    }

    void ApplyKickbackEffect()
    {
        if(name.Equals("Ogre")) {
            GetComponent<OgreMovement>().enabled = false;
        }

        int kickback = playerStats.GetKickBack();
        rb.velocity += new Vector2(kickback, kickback);
        StartCoroutine(ReactivateMovement("Ogre"));
    }

    IEnumerator ReactivateMovement(string name)
    {
        yield return new WaitForSeconds(1f);
        switch(name)
        {
            case "Ogre":
                GetComponent<OgreMovement>().enabled = true;
                break;
            default:
                break;
        }    
    }

    IEnumerator DisableHitAnimation()
    {
        yield return new WaitForSeconds(secondsToWaitAfterAttack);
        animator.SetBool("isHit", false);
    }

    public int IsPlayerToLeft()
    {
        Vector2 playerCoordinates = CalculatePlayerCoordinates();
        return transform.position.x > playerCoordinates.x ? -1 : 1;
    }

    public Vector2 CalculatePlayerCoordinates()
    {
        return GameObject.Find("Player").transform.position;
    }

    public int GetDamageDealtByEnemy()
    {
        return damage;
    }
}
