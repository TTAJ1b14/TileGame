using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput;
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    CapsuleCollider2D myCapsuleCollider2D;
    BoxCollider2D feetCollider2D;
    SpriteRenderer mySpriteRenderer;
    private CinemachineImpulseSource myImpulseSource;


    float myStartGravity;

    bool isAlive =true;

    [SerializeField] float runSpeed = 1f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbspeed = 5f;
    [SerializeField] Color32 normalColor = new Color32 (1,1,1,1);
    [SerializeField] Color32 deadColor = new Color32 (1,1,1,1);
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;

    
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCapsuleCollider2D = GetComponent<CapsuleCollider2D>();
        feetCollider2D = GetComponent<BoxCollider2D>();
        myStartGravity = myRigidBody.gravityScale;
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myImpulseSource = GetComponent<CinemachineImpulseSource>();
    }

    
    void Update()
    {
        if (!isAlive) {return;}
        Run(); 
        FlipSprite();
        ClimbLadder();
        Die();
    }

    void OnFire(InputValue value)
    {
        if (!isAlive) {return;}
        Instantiate (bullet, gun.position, transform.rotation);
    }

    void OnMove(InputValue value)
    {
        if (!isAlive) {return;}
        moveInput = value.Get<Vector2>();
        Debug.Log("Mi");
    }

    void OnJump(InputValue value)
    {
        if (!isAlive) {return;}

        if  (!feetCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            Debug.Log ("NotOnGroundCheck");
            return;
        }
       
        if(value.isPressed)
        {
            myRigidBody.velocity += new Vector2 (0f, jumpSpeed);
        }
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2 (moveInput.x * runSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;          
        myAnimator.SetBool("IsRunning", playerHasHorizontalSpeed);
    }

    void ClimbLadder()
    {
        if (!myCapsuleCollider2D.IsTouchingLayers(LayerMask.GetMask("Ladder"))) 
        {
            myRigidBody.gravityScale = myStartGravity;
            myAnimator.SetBool("IsClimbing", false);
            return;  
        }
        {
          Debug.Log ("OnLadderCheck");  
          Vector2 climbVelocity = new Vector2 (myRigidBody.velocity.x, moveInput.y * climbspeed); 
          myRigidBody.velocity = climbVelocity; 
          myRigidBody.gravityScale = 0f;
        
            bool playerHasVerticalSpeed = Mathf.Abs(myRigidBody.velocity.y) > Mathf.Epsilon;
            myAnimator.SetBool("IsClimbing", playerHasVerticalSpeed);
        }
    }

    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed)
        {
        transform.localScale = new Vector2 (Mathf.Sign(myRigidBody.velocity.x), 1f);
        }
    }

    void Die()
    {
        if (myRigidBody.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards"))) 
        {
            isAlive = false;
            myAnimator.SetTrigger("Dying"); 
            mySpriteRenderer.color = deadColor;
            myImpulseSource.GenerateImpulse(1);
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }




}
