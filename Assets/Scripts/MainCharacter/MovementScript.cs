using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementScript : MonoBehaviour
{
    Vector2 moveInput;

    Animator animator;
    Rigidbody2D rb;
    BoxCollider2D myCollider;
    PlayerStats playerStats;
    float playerSpeed;
   
    [SerializeField] float timeToWaitAfterBeingAttackedToMoveAgain = 1f;
    [SerializeField] int kickbackFromEnemyAttack = 40;

    void Start()
    {
        myCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerStats = GetComponent<PlayerStats>();
    }

    void FixedUpdate()
    {
        playerSpeed = playerStats.GetPlayerSpeed();
        if(playerStats.PlayerIsAlive())
        {
            Run();
            FlipSprite();
            CheckIfPlayerIsFalling();
        }
        GetComponent<BoxCollider2D>().offset = new Vector2(-0.3f, -0.04267314f);
    }

    private void CheckIfPlayerIsFalling()
    {
        LayerMask mask = LayerMask.GetMask("Ground");
        if(myCollider.IsTouchingLayers(mask))
        {
            animator.SetBool("fall", false);
        } else
        {
            animator.SetBool("fall", true);
        }
    }

    //If the player goes left the sprite flips left, otherwise it flips to the right
    void FlipSprite()
    {
        //We could check if the velocity of the player is > than 0 but that would
        //cause some problems with Unity
        bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) >= Mathf.Epsilon;

        if(playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f);
        }
    }

    void Run()
    {        
        Vector2 playerVelocity = new(moveInput.x * playerSpeed, rb.velocity.y);
        rb.velocity = playerVelocity;
        
        if (myCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            animator.SetBool("run", Mathf.Abs(rb.velocity.x) >= Mathf.Epsilon);
        }   
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }    

    void OnJump(InputValue value)
    {
        LayerMask mask = LayerMask.GetMask("Ground");
        if (value.isPressed && myCollider.IsTouchingLayers(mask) && playerStats.PlayerIsAlive())
        {
            animator.SetTrigger("jump");
            rb.velocity += new Vector2(0f, playerStats.GetJumpForce());
        }
    }    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy") && playerStats.PlayerIsAlive())
        {            
            this.enabled = false;
            TriggerKickup();
            StartCoroutine(ActivateMovement());
        }
    }

    //the movements MUST be disabled otherwise the force is not applied when the user
    //is hit by a monster
    IEnumerator ActivateMovement()
    {
        yield return new WaitForSeconds(timeToWaitAfterBeingAttackedToMoveAgain);
        this.enabled = true;
    }

    void TriggerKickup()
    {
        rb.velocity += new Vector2(-kickbackFromEnemyAttack, 3f);
    }

    public void MoveToNearestSecretDoor()
    {
        Vector2 positionToGoTo = new Vector2(transform.localPosition.x - 8, transform.localPosition.y + 13);
        transform.localPosition = positionToGoTo;
    }
}
