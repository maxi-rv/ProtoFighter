using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //COMPONENTS
    private Rigidbody2D rigidBody;
    private Animator animator;
    private BoxCollider2D pushBox;
    private CircleCollider2D pushBoxHead;
    private BoxCollider2D hurtBoxHigh;
    private BoxCollider2D hurtBoxMid;
    private BoxCollider2D hurtBoxLow;
    private CheckGround CheckGroundScript;
    private CheckHitAll CheckHitAllScript;
    
    //VARIABLES
    public int life;
    private float moveSpeed;
    private float knockBack;
    public bool isGrounded;
    public bool isHurt;
    public bool isBlocking;
    public bool LightAttacking;
    public bool HeavyAttacking;
    private bool isFacingRight;
    

    void Awake()
    {
        //CHILDREN
        GameObject PushBoxes = gameObject.transform.Find("PushBoxes").gameObject;
        GameObject HurtBoxes = gameObject.transform.Find("HurtBoxes").gameObject;
        GameObject CheckGround = gameObject.transform.Find("CheckGround").gameObject;

        GameObject HurtBoxHigh = HurtBoxes.transform.Find("HurtBoxHigh").gameObject;
        GameObject HurtBoxMid = HurtBoxes.transform.Find("HurtBoxMid").gameObject;
        GameObject HurtBoxLow = HurtBoxes.transform.Find("HurtBoxLow").gameObject;
        

        //COMPONENTS
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        pushBox = PushBoxes.GetComponent<BoxCollider2D>();
        pushBoxHead = PushBoxes.GetComponent<CircleCollider2D>();
        hurtBoxHigh = HurtBoxHigh.GetComponent<BoxCollider2D>();
        hurtBoxMid = HurtBoxMid.GetComponent<BoxCollider2D>();
        hurtBoxLow = HurtBoxLow.GetComponent<BoxCollider2D>();

        //SCRIPTS
        CheckGroundScript = CheckGround.GetComponent<CheckGround>();
        CheckHitAllScript = HurtBoxes.GetComponent<CheckHitAll>();

        //VARIABLES
        life = 9;
        moveSpeed = 5f;
        knockBack = 10f;
        isGrounded = false;
        isHurt = false;
        isFacingRight = true;
    }

    void FixedUpdate()
    {
        //Checks movement input
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        //Checks action input
        bool LAButton = Input.GetButton("LightAttack");
        bool HAButton = Input.GetButton("HeavyAttack");
        bool blockButton = Input.GetButton("Block");

        //Checks States
        bool isIdle = animator.GetBool("isIdle");
        bool isWalking = animator.GetBool("isWalking");
        bool isCrouching = animator.GetBool("isCrouching");
        bool onAction = animator.GetBool("LightAttacking") || animator.GetBool("HeavyAttacking") || animator.GetBool("isBlocking") || animator.GetBool("isHurt");

        LightAttacking = animator.GetBool("LightAttacking");
        HeavyAttacking = animator.GetBool("HeavyAttacking");
        isBlocking = animator.GetBool("isBlocking");

        //Check Other Scripts Behaviour
        isGrounded = CheckGroundScript.isGrounded;
        isHurt = CheckHitAllScript.isHurt;
        

        //If Left or Right is NOT pressed.
        if (moveVertical==0 && isGrounded)
        {
            Move(0f);
        }

        //If Left or Right is pressed.
        if (moveHorizontal!=0 && isGrounded && !isCrouching && !onAction)
        {
            //Rotate the object by converting the angles into a quaternion.
            Quaternion faceRight = Quaternion.Euler(0, 0, 0);
            Quaternion faceLeft = Quaternion.Euler(0, 180, 0);
            

            if (moveHorizontal>0)
            {
                //Dampen towards the target rotation
                transform.rotation = Quaternion.Slerp(transform.rotation, faceRight,  180);
                isFacingRight = true;
            }
            else
            {
                // Dampen towards the target rotation
                transform.rotation = Quaternion.Slerp(transform.rotation, faceLeft,  180);
                isFacingRight = false;
            }

            Move(moveHorizontal);
        }

        //If Down is pressed.
        if (moveVertical==-1 && isGrounded)
        {
            Crouch();
        }

        //If Down is NOT pressed.
        if (moveVertical==0 && isGrounded)
        {
            animator.SetBool("isCrouching", false);
        }

        //Getting Hurt
        if(isHurt)
        {
            GetHurt();
        }

        //LightAttack
        if(LAButton && !onAction)
        {
            LightAttack();
        }

        //HeavyAttack
        if(HAButton && !onAction)
        {
            HeavyAttack();
        }

        ///If Block is pressed
        if(blockButton && !onAction)
        {
            Block();
        }

        ///If Block is NOT pressed
        if(!blockButton && onAction)
        {
            animator.SetBool("isBlocking", false);
        }
    }

    void Move(float moveHorizontal)
    {
        animator.SetBool("isWalking", moveHorizontal!=0);

        Vector2 aux = new Vector2(moveHorizontal, 0f);
        rigidBody.velocity = (aux*moveSpeed);
    }

    void Crouch()
    {
        animator.SetBool("isCrouching", true);
        animator.SetBool("isWalking", false);
        rigidBody.velocity = new Vector2(0f, 0f);
    }

    void Block()
    {
        animator.SetBool("isBlocking", true);
    }

    void LightAttack()
    {
        animator.SetBool("LightAttacking", true);
        animator.SetBool("OnStartUp", true);
    }

    void HeavyAttack()
    {
        animator.SetBool("HeavyAttacking", true);
        animator.SetBool("OnStartUp", true);
    }

    void GetHurt()
    {
        CheckHitAllScript.SetFalse();
        animator.SetBool("isHurt", true);

        if(isFacingRight)
        {
            //rigidBody.velocity = new Vector2(-knockBack, 0f);
            rigidBody.AddForce(new Vector2(-knockBack, 0f));
        }
        else
        {
            //rigidBody.velocity = new Vector2(knockBack, 0f);
            rigidBody.AddForce(new Vector2(-knockBack, 0f));
        }

        hurtBoxHigh.enabled = false;
        hurtBoxMid.enabled = false;
        hurtBoxLow.enabled = false;

        life--;
    }

    public void StopHurt()
    {
        animator.SetBool("isHurt", false);

        hurtBoxHigh.enabled = true;
        hurtBoxMid.enabled = true;
        hurtBoxLow.enabled = true;
    }

    public void StopStartup()
    {
        animator.SetBool("OnStartUp", false);
        animator.SetBool("OnActive", true);
    }

    public void StopActive()
    {
        animator.SetBool("OnActive", false);
        animator.SetBool("OnRecovery", true);
    }

    public void StopRecovery()
    {
        animator.SetBool("OnRecovery", false);

        if(LightAttacking)
        {
           animator.SetBool("LightAttacking", false);
        }

        if(HeavyAttacking)
        {
           animator.SetBool("HeavyAttacking", false);
        }

    }
}
