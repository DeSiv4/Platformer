using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackCollider : MonoBehaviour
{

    EnemyController EnemyAdd;
    EnemyController EnemyRemove;

    public List<EnemyController> Enemies = new List<EnemyController>();

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
        if (other.gameObject.layer == LayerMask.NameToLayer("PlayerAttackable"))
        {
            if (other.CompareTag("Enemy"))
            {
                EnemyAdd = other.GetComponent<EnemyController>();
                Enemies.Add(EnemyAdd);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("PlayerAttackable"))
        {
            if (other.CompareTag("Enemy"))
            {
                EnemyRemove = other.GetComponent<EnemyController>();
                Enemies.Remove(EnemyRemove);
            }
        }
    }

}
