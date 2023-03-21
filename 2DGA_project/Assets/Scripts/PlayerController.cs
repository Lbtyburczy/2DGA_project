using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float direction;
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float jumpHeight = 5f;

    private bool isGrounded = false;
    private bool facingRight = true;

    private Rigidbody2D rb;
    private Animator anim;

    private enum MovementState { idle, running, jumping }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // movement by changing transform
        direction = Input.GetAxis("Horizontal");
        anim.SetFloat("speed", Mathf.Abs(direction));
        transform.Translate(transform.right * direction * speed * Time.deltaTime);

        // jump by applying force to character
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            isGrounded = false;
            rb.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
        }

        Flip();

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

        if (rb.velocity.y > 0.1f && !isGrounded) {
            state = MovementState.jumping;
        }

        anim.SetInteger("state", (int)state);
    }

    // triggers when object collider hits another objects collider
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }
}
