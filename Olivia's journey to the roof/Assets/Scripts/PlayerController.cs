using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D bc2d;
    private SpriteRenderer sr;
    private Animator animator;
    private LayerMask unJumpable;
    private float extraHeight = .1f;
    private float idleTimer;

    [SerializeField] float speed = 8f;
    [SerializeField] float fallMultiplier = 4f;
    [SerializeField] float lowJumptMultiplier = 2f;
    [SerializeField] float jumpForce = 8f;
    [SerializeField] private float timeToSitTimer = 4f;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        rb = GetComponent<Rigidbody2D>();
        bc2d = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        unJumpable = LayerMask.GetMask("Platform"); // player can't jump from out of this layer's objects.
    }

    // Update is called once per frame
    void Update()
    {
        float x     = Input.GetAxis("Horizontal");
        float y     = Input.GetAxis("Vertical");
        Vector2 dir = new Vector2(x, y);
        Walk(dir);
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            animator.SetTrigger("timeToJump");
            Jump();
        }
        Fall();
        OnLanding();
    }

    private void Walk(Vector2 dir) // set walking speed.
    {
        IdleStateMachine();
        RunStateMachine();
        Vector3 characterScale = transform.localScale;
        rb.velocity = new Vector2(dir.x * speed, rb.velocity.y);
        if (rb.velocity.x < 0 )
        {
            sr.flipX = true;
        }
        else if (rb.velocity.x > 0)
        {
            sr.flipX = false;
        }
        characterScale = transform.localScale;
    }


    private void Jump() // set jump force onto player. Falsifies 'isGrounded'.
    {
        
        rb.velocity  = new Vector2(rb.velocity.x, 0);
        rb.velocity += Vector2.up * jumpForce;
    }

    private void Fall() // increases falling speed depending on player input and current velocity.
    {
        if (rb.velocity.y < 0)
        {
            Debug.Log("Fall!");
            animator.SetTrigger("isFalling");
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumptMultiplier - 1) * Time.deltaTime;
        }
    }

    public bool IsGrounded()
    {
        RaycastHit2D groundBoxCastInfo =  Physics2D.BoxCast(bc2d.bounds.center, bc2d.bounds.size, 0f, Vector2.down, extraHeight, unJumpable);
        return groundBoxCastInfo.collider != null;
    }

    private void OnLanding()
    {
        if (IsGrounded())
        {
            animator.SetTrigger("haveFallen");
        }
    }



    private void RunStateMachine()
    {

        if (Input.GetAxis("Horizontal") != 0 && rb.velocity.y == 0)
        {

            animator.SetBool("timeToRun", true);
        }
        else if (Input.GetAxis("Horizontal") == 0 || rb.velocity.y != 0)
        {

            animator.SetBool("timeToRun", false);
        }
    }
    private void IdleStateMachine()
    {
        idleTimer += Time.deltaTime;
        if (Input.anyKeyDown)
        {
            idleTimer = 0;
            animator.SetBool("timeToSit", false);
        }
        else if (idleTimer > timeToSitTimer)
        {
            animator.SetBool("timeToSit", true);
        }

    }

}
