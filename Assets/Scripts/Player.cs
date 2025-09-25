using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rigidbody;

    public float velocity;
    public SpriteRenderer spriteRenderer;
    public float jumpforce;
    private float PlayerHalfHeight;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        PlayerHalfHeight = spriteRenderer.bounds.extents.y;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        jump();
    }
    void Movement()
    {
        float MovementInput = Input.GetAxis("Horizontal");
        

        rigidbody.velocity = new Vector2 (MovementInput * velocity, rigidbody.velocity.y);
    }
    void jump()
    {

        if (Input.GetKeyDown(KeyCode.Space) && IsGround())
        {
            rigidbody.AddForce(Vector2.up * jumpforce, ForceMode2D.Impulse);
        }
    }

    bool IsGround()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, PlayerHalfHeight+0.1f, LayerMask.GetMask("Ground"));
    }
}
