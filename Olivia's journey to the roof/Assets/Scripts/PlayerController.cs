using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Camera cam;
    private GameObject ray;
    private bool _isGrounded;
    [SerializeField] float _speed = 8f;
    [SerializeField] float _fallMultiplier = 4f;
    [SerializeField] float _lowJumpMultiplier = 2f;
    [SerializeField] float _jumpForce = 8f;



    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        _isGrounded = true;
        _rb         = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float x     = Input.GetAxis("Horizontal");
        float y     = Input.GetAxis("Vertical");
        Vector2 dir = new Vector2(x, y);
        Walk(dir);
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            Jump();
        }
        Fall();
        
    }

    

    private void Walk(Vector2 dir)
    {
        _rb.velocity = new Vector2(dir.x * _speed, _rb.velocity.y);
    }

    private void Jump()
    {
        _rb.velocity  = new Vector2(_rb.velocity.x, 0);
        _rb.velocity += Vector2.up * _jumpForce;
        _isGrounded   = false;
    }

    private void Fall()
    {
        if (_rb.velocity.y < 0)
        {
            _rb.velocity += Vector2.up * Physics2D.gravity.y * (_fallMultiplier - 1) * Time.deltaTime;
        }
        else if (_rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            _rb.velocity += Vector2.up * Physics2D.gravity.y * (_lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    
    
    // to be changed...
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            _isGrounded = true;
        }
    }
}
