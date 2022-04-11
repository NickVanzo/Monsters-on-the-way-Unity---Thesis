using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OgreMovement : MonoBehaviour
{
    [SerializeField] int movementSpeed = 2;
    [SerializeField] float range = 8f;
    [SerializeField] float timeToWaitToCheckDistances = 1f;

    float distanceFromPlayer = 1000;
    bool canFollowPlayer = true;

    Animator animator;
    Rigidbody2D rb;
    EnemyStats stats;
    PlayerStats playerStats;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        stats = GetComponent<EnemyStats>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(playerStats.PlayerIsAlive() && canFollowPlayer)
        {
            Move();
        }
    }

    void Move()
    {
        bool isPlayerInRange = PlayerIsInRange();
        int isPlayerToLeft = stats.IsPlayerToLeft();
        animator.SetBool("isRunning", isPlayerInRange);

        if (isPlayerInRange)
        {
            Vector2 enemyVelocity = new(isPlayerToLeft * movementSpeed, rb.velocity.y);
            transform.localScale = new(Mathf.Sign(isPlayerToLeft), 1f);
            rb.velocity = enemyVelocity;
        }
        else
        {
            Vector2 enemyVelocity = new(0f, 0f);
            rb.velocity = enemyVelocity;
        }        
    }

    bool PlayerIsInRange()
    {       
        return CalculateDistanceFromPlayer() < range;
    }

    float CalculateDistanceFromPlayer()
    {
        Vector2 playerCoordinates = stats.CalculatePlayerCoordinates();

        //The distance between two points 2D in space √[(x₂ - x₁)² + (y₂ - y₁)²].
        distanceFromPlayer = Mathf.Sqrt(Mathf.Pow((playerCoordinates.x - transform.position.x), 2) + Mathf.Pow((playerCoordinates.y - transform.position.y), 2));
        return distanceFromPlayer;
    }   

    IEnumerator WaitToCheckDistance()
    {
        yield return new WaitForSeconds(timeToWaitToCheckDistances);
    }

    public void Disable()
    {
        canFollowPlayer = false;
    }

    public void Active()
    {
        canFollowPlayer = true;
    }
}
