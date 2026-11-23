using UnityEngine;

public class EnemyAttackCollider : MonoBehaviour
{

    EnemyController Parent;
    public PlayerController Player;
    public TowerController Tower;
    public Animator Animator;
    public bool PlayerInRange;
    public bool TowerInRange;

    public bool WasAttacking;

    void Awake()
    {
        Parent = GetComponentInParent<EnemyController>();
        Animator = Parent.Animator;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Attackable"))
        {
            Parent.StopMovement();
            Parent.Attack();
            WasAttacking = true;

            if (other.CompareTag("Player"))
            {
                Player = other.GetComponent<PlayerController>();
                PlayerInRange = true;
            }

            if (other.CompareTag("Tower"))
            {
                Tower = other.GetComponent<TowerController>();
                TowerInRange = true;
            }

        }
    }


    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Attackable"))
        {
            Parent.StopAttack();
            WasAttacking = false;

            if (other.CompareTag("Player"))
            {
                PlayerInRange = false;
            }

            if (other.CompareTag("Tower"))
            {
                TowerInRange = false;
            }
        }
    }

}
