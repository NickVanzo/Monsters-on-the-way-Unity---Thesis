using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The white skeleton walk left and right at random, it doesn't attack directly
//the player
public class WhiteSkeletonMovement : MonoBehaviour
{
    [SerializeField] int movementSpeed = 2;
    bool goLeft;

    Rigidbody2D rb;
    CapsuleCollider2D capsuleCollider;
    Animator animator;
    Vector2 enemyVelocity = new(0, 0);

    void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        goLeft = ChooseADirection();
        SetUpMovementAnimation();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {

        if(EnemyIsTouchingAWall())
        {
            movementSpeed *= -1;
        }

        if (goLeft)
        {
            enemyVelocity = new Vector2(movementSpeed, rb.velocity.y);
        } else
        {
            enemyVelocity = new Vector2(-movementSpeed, rb.velocity.y);
        }
        rb.velocity = enemyVelocity;
        transform.localScale = new(-Mathf.Sign(movementSpeed), 1f);
    }

    bool EnemyIsTouchingAWall()
    {
        return capsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }

    void SetUpMovementAnimation()
    {
        animator.SetBool("isRunning", true);
    }

    bool ChooseADirection()
    {        
        return Random.Range(0, 2) == 1 ? true : false;
    }    
}
