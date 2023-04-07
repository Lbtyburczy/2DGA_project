using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable
{
    private float direction;
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float jumpHeight = 5f;
    [SerializeField]
    private int health = 100;

    private bool isGrounded = false;
    private bool facingRight = true;

    private bool immovable = false;

    private Rigidbody2D rb;
    private Animator anim;

    private enum MovementState { idle, running, jumping, falling, attacking }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!immovable) {
            direction = Input.GetAxis("Horizontal");
            anim.SetFloat("speed", Mathf.Abs(direction));
            transform.Translate(transform.right * direction * speed * Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
            {
                isGrounded = false;
                rb.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
            }

            Flip();
        }
 
        UpdateAnimations();
    }

    private void Flip() { 
        if(facingRight && direction < 0f || !facingRight && direction > 0f)
        {
            facingRight = !facingRight;
            transform.Rotate(new Vector3(0, 180f, 0));
        }
    }

    private void UpdateAnimations() {
        MovementState state;

        if (direction != 0)
        {
            state = MovementState.running;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > 0.1f) 
        {
            state = MovementState.jumping;
        } 
        else if (rb.velocity.y < -0.1f) 
        {
            state = MovementState.falling;
        }

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            state = MovementState.attacking;
        }

        anim.SetInteger("state", (int)state);
    }

    public void SwitchImmovable() 
    {
        immovable = !immovable;
    }

    public void Attack() 
    {
        print("Attack");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        print(health);

        if (health <= 0) {
            Destroy(gameObject);
        }
    }
}
