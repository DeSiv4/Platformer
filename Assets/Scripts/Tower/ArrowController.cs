using UnityEngine;
using System.Collections;

public class ArrowController : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Death());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.right * 4.0f * Time.deltaTime;
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("PlayerAttackable"))
        {
            if (other.CompareTag("Enemy"))
            {
                EnemyController Enemy = other.GetComponent<EnemyController>();
                Enemy.TakeDamage();
                Destroy(gameObject);
            }
        }
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }


}
