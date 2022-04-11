using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    Animator animator;
    bool canAttack;
    int meleeDamage;
    public Transform attackPointPosition;
    public LayerMask enemyLayers;

    AudioSource audioSource;
    [SerializeField] AudioClip attackAudioClip;

    [SerializeField] float attackRange = 0.5f;

    PlayerStats playerStats;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        canAttack = true;
        playerStats = GetComponent<PlayerStats>();
        meleeDamage = playerStats.GetMeleeDamage();
        animator = GetComponent<Animator>();
    }

    void OnFire(InputValue value)
    {
        if(canAttack && playerStats.PlayerIsAlive())
        {
            audioSource.PlayOneShot(attackAudioClip);
            Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPointPosition.position, attackRange, enemyLayers);
            foreach(Collider2D enemy in enemies)
            {
                enemy.GetComponent<EnemyStats>().TakeDamage(meleeDamage);
            }
            animator.SetTrigger("attack");
            StartCoroutine(WaitForDelay());
        }
    }   

    IEnumerator WaitForDelay()
    {
        canAttack = false;
        yield return new WaitForSeconds(playerStats.GetAttackDelay());
        canAttack = true;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPointPosition == null) return;
        Gizmos.DrawWireSphere(attackPointPosition.position, attackRange);
    }    
}
