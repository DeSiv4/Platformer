using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{

    Rigidbody2D myRb;

    public float MoveSpeed;
    public int MoveDirection = -1 ;
    public float Health = 2;

    public SpriteRenderer spriteCh;
    public Animator Animator;
    EnemyAttackCollider AttackColChd;
    EnemyAnimLink AnimLink;

    public GameManager GameplayManager;
    
    public bool IsGrounded;
    public bool AllowRun = true;

    void Awake()
    {
        myRb = GetComponent<Rigidbody2D>();
        Animator = GetComponentInChildren<Animator>();
        AttackColChd = GetComponentInChildren<EnemyAttackCollider>();
        AnimLink = GetComponentInChildren<EnemyAnimLink>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AnimLink.Parent = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsGrounded && AllowRun)
        {
            MoveEnemy();
        }
    
    }

    void MoveEnemy()
    {
        if (!Animator.GetBool("IsRunning"))
        {
            ResetAnimator();
            Animator.SetBool("IsRunning", true);
        }
        myRb.linearVelocity = new Vector2(MoveDirection * MoveSpeed, myRb.linearVelocity.y);
    }

    public void StopMovement()
    {
        ResetAnimator();
        AllowRun = false;
    }

    void ResetAnimator()
    {
        if (!Animator.GetBool("IsDead"))
        {
            Animator.SetBool("IsRunning", false);
            Animator.SetBool("IsAttacking", false);
            Animator.SetBool("IsHurt", false);
            Animator.SetBool("IsDead", false);
        }
    }

    public void Attack()
    {
        if (!AllowRun && !Animator.GetBool("IsAttacking"))
        {
            Animator.SetBool("IsAttacking", true);
        }
    }

    public void StopAttack()
    {
        if (!AllowRun)
        {
            ResetAnimator();
            Animator.SetBool("IsRunning", true);
            AllowRun = true;
        }
    }

    public void HurtObjectEC()
    {
        if (AttackColChd.PlayerInRange)
        {
            AttackColChd.Player.Hurt();
        }

        if (AttackColChd.TowerInRange)
        {
            AttackColChd.Tower.TowerTakeDmg();
        }
    }

    public void TakeDamage()
    {
        Health = Health - 1;
        StopMovement();
        Animator.SetBool("IsHurt", true);

        if (Health == 0 && !Animator.GetBool("IsDead"))
        {
            ResetAnimator();
            Animator.SetBool("IsDead", true);
            StartCoroutine(DestroySelf());
            return;
        }

        StartCoroutine(SetHurtFalse());

    }

    IEnumerator DestroySelf()
    {
        GameplayManager.EnemDeadCheck();

        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    IEnumerator SetHurtFalse()
    {
        if (!Animator.GetBool("IsDead"))
        {
            yield return new WaitForSeconds(.75f);
            ResetAnimator();
            // once hurt return to normal state
            if (AttackColChd.WasAttacking)
            {
                StopMovement();
                Attack();
            }
            else
            {
                AllowRun = true;
            }
        }
    }

}
