using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    Rigidbody2D myRb;

    public float MoveSpeed;
    public float JumpForce;
    public float Health = 10;

    public SpriteRenderer PlayerSprite;
    public Animator PlayerAnimator;
    PlayerAttackCollider AttackCollider;
    GameManager GameplayManager;

    float moveInput;
    public bool IsGrounded;


    void Awake()
    {
        myRb = GetComponent<Rigidbody2D>();
        PlayerAnimator = GetComponentInChildren<Animator>();
        AttackCollider = GetComponentInChildren<PlayerAttackCollider>();

        GameplayManager = FindFirstObjectByType<GameManager>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameplayManager.Player = this;
        IsGrounded = true;
    }

    // Update is called once per frame
    void Update()
    {

        moveInput = Input.GetAxisRaw("Horizontal");

        if (moveInput > 0)
        {
            // D pressed
            PlayerSprite.flipX = true;
        }
        else if (moveInput < 0)
        {
            // A pressed
            PlayerSprite.flipX = false;
        }
        else if (moveInput == 0)
        {
            if (PlayerAnimator.GetBool("IsSprinting"))
            {
                PlayerAnimator.SetBool("IsSprinting", false);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded)
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Attack();
        }

    }


    void FixedUpdate()
    {

        MovePlayer();

    }

    void MovePlayer()
    {
        if (!PlayerAnimator.GetBool("IsSprinting") && IsGrounded)
        {
            PlayerAnimator.SetBool("IsSprinting", true);
        }
        myRb.linearVelocity = new Vector2(moveInput * MoveSpeed,myRb.linearVelocity.y);
    }

    void Jump()
    {
        PlayerAnimator.SetBool("IsSprinting", false);
        PlayerAnimator.SetBool("IsJumping", true);
        myRb.linearVelocity = new Vector2(myRb.linearVelocity.x, JumpForce);
    }

    void Attack()
    {
        if (!PlayerAnimator.GetBool("isAttacking") && IsGrounded)
        {
            PlayerAnimator.SetBool("isAttacking", true);
            if (AttackCollider.Enemies.Count>0)
            {
                for(int i  = 0; i< AttackCollider.Enemies.Count; i++)
                {
                    AttackCollider.Enemies[i].TakeDamage();
                }
            }
        }
    }

    public void Hurt()
    {
        Health = Health - 1;
        if (Health < 0)
        {
            Health = 10;
            GameplayManager.Respawn();
        }
    }


}
