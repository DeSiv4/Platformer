using UnityEngine;

public class EnemyAnimLink : MonoBehaviour
{

    Animator Animator;
    public EnemyController Parent;

    void Awake()
    {
        Animator = GetComponent<Animator>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AttackEnd()
    {
        Parent.HurtObjectEC();
    }

}
