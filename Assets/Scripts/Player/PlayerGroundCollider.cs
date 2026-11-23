using UnityEngine;

public class PlayerGroundCollider : MonoBehaviour
{

    PlayerController Parent;

    void Awake()
    {
        Parent = GetComponentInParent<PlayerController>();
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
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Parent.IsGrounded = true;
            Parent.PlayerAnimator.SetBool("IsJumping", false);
            //Debug.Log("OnGround");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Parent.IsGrounded = false;
            //Debug.Log("OnAir");
        }
    }

}
