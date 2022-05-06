using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip coinCollected;
    [SerializeField] AudioClip gemCollected;
    [SerializeField] AudioClip deathSound;

    [SerializeField] int maxHealth = 1;
    [SerializeField] float playerSpeed = 1f;
    [SerializeField] int meleeDamage = 1;
    [SerializeField] int kickback = 40;
    [SerializeField] float attackDelay = 1.5f;
    [SerializeField] float jumpForce = 4f;
        
    Animator animator;
    bool isAlive;

    int currentHealth;
    int currentGold;
    int currentTokens;
    int totalGold;
    int totalTokens;
    int commonTickets;
    int rareTickets;
    int veryRareTickets;
    int goldPathActive;
    bool isSharpeningActive;
    bool isWrathActive;
    bool isMidnightActive;
    bool isImmersionActive;

    // Start is called before the first frame update
    void Start()
    {
        isImmersionActive = false;
        isMidnightActive = false;
        isWrathActive = false;
        isSharpeningActive = false;
        commonTickets= 0;
        rareTickets = 0;
        veryRareTickets = 0;
        totalTokens = 0;
        totalTokens = 0;
        goldPathActive = 0;
        currentTokens = 0;
        currentGold = 0;
        currentHealth = maxHealth;
        isAlive = true;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void RemoveHealth(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            animator.SetTrigger("hit");
        }
    }

    public void ResetPromethiumOfPlayer()
    {
        currentTokens = 0;
    }

    public void Die()
    {
        audioSource.PlayOneShot(deathSound);
        animator.SetTrigger("hit");
        animator.SetBool("death", true);
        isAlive = false;
        GetComponent<Collider2D>().enabled = false;
    }

      public void CurePlayer(int health)
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += health;
        }
    }

    public void AddMaxHealth(int maxHealthToAdd)
    {
        maxHealth += maxHealthToAdd;
        CurePlayer(maxHealthToAdd);
    }

    public void RemovePointsFromMaxHealth(int pointsToRemove)
    {
        maxHealth -= pointsToRemove;
        if(maxHealth == 0)
        {
            Die();        
        }
    }

    public void AddGold(int goldCollected)
    {
        audioSource.PlayOneShot(coinCollected);
        currentGold += goldCollected + goldCollected * (goldPathActive);
        totalGold += goldCollected;
    }

    public void AddPromethium(int promethiumToAdd)
    {
        audioSource.PlayOneShot(gemCollected);
        currentTokens += promethiumToAdd;
        totalTokens += promethiumToAdd;
    }

    public void AddTicket(string rarity)
    {
        if(rarity.Equals("common"))
        {
            commonTickets += 1;
        } else if(rarity.Equals("rare"))
        {
            rareTickets += 1;
        } else
        {
            veryRareTickets += 1;
        }
    }

    public void AddDamageStat(int damage)
    {
        meleeDamage += damage;
    }

    public void AddPlayerSpeed(float speed)
    {
        playerSpeed += speed;
    }

    public void ModifyAttackDelay(float delay)
    {
        attackDelay += delay;
    }

    public void AddJumpForce(float jumpForceToAdd)
    {
        jumpForce += jumpForceToAdd;
    }

    public bool PlayerIsAlive()
    {
        return isAlive;
    }

    public int GetMeleeDamage()
    {
        return meleeDamage;
    }

    public int GetKickBack()
    {
        return kickback;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public int GetGoldOfPlayer()
    {
        return currentGold;
    }

    public int GetPromethiumOfPlayer()
    {
        return currentTokens;
    }
   
    public int GetHealth()
    {
        return currentHealth;
    }

    public float GetAttackDelay()
    {
        return attackDelay;
    }

    public float GetPlayerSpeed()
    {
        return playerSpeed;
    }

    public float GetJumpForce()
    {
        return jumpForce;
    }

    public void ActivatePathOfGold()
    {
        goldPathActive = 1;
    }

    public bool IsGoldPathActive()
    {
        return goldPathActive == 1;
    }

    public void ResetGold()
    {
        this.currentGold = 0;
    }

    public int TotalGold()
    {
        return this.totalGold;
    }

    public int TotalTokens()
    {
        return this.totalTokens;
    }

    public int GetCommonTickets()
    {
        return this.commonTickets;
    }

    public int GetRareTickets()
    {
        return this.rareTickets;
    }

    public int GetVeryRareTickets()
    {
        return this.veryRareTickets;
    }


    public void SetSharpening()
    {
        this.isSharpeningActive = true;
    }

    public bool IsSharpeningActive()
    {
        return this.isSharpeningActive;
    }

    public void ActivateWrath()
    {
        this.isWrathActive = true;
    }

    public bool IsWrathActive()
    {
        return isWrathActive;
    }

    public void ActivateMidnightHunt()
    {
        this.isMidnightActive = true;
    }

    public bool IsMidnightHuntActive()
    {
        return this.isMidnightActive;
    }

    public bool IsImmersionActive()
    {
        return this.isImmersionActive;
    }

    public void ActivateImmersion()
    {
        this.isImmersionActive = true;
    }
}
