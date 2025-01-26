using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private float movementX;
    private float movementY;
    private bool _grounded;
    private bool _jumpWait;
    private PlayerInput _playerInput;
    private Vector2 moveInput;
    private Animator _animator;
    private SpriteRenderer spriteRenderer;

    public float speed = 1.0f;
    public float jumpForce = 10.0f;
    public float playerKillDepth = -20f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _grounded = true;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_jumpWait)
        {
            _grounded = Grounded();
            _jumpWait = false;
        }

        _animator.SetBool("IsInAir", !_grounded);
        _animator.SetBool("IsWalking", moveInput.x != 0f);

        spriteRenderer.flipX = moveInput.x >= 0f;

        print(transform.position.y);
        if (transform.position.y <= playerKillDepth)
        {
            TakeDamage();
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput.x * speed, rb.velocity.y);
        //print("Velocity: " + rb.velocity);
    }
        
    private IEnumerator JumpFrameSkipRoutine()
    {
        while(!_grounded)
        {
            yield return new WaitForSeconds(0.1f);
            _jumpWait = true;
        }
    }

    private bool Grounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, -Vector2.up, 1.7f, LayerMask.GetMask("Ground", "Enemy"));
        if(hit.collider != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton() && Grounded())
        {
            rb.AddForce(Vector2.up * jumpForce);
            _grounded = false;
            StartCoroutine(JumpFrameSkipRoutine());
        }
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        var axisInput = context.ReadValue<Vector2>();
        float horizontalInput = axisInput.x;
        moveInput = new Vector2(horizontalInput, rb.velocity.y);
    }

    public void TakeDamage()
    {
        Destroy(gameObject);
        //play death animation

        //Reload scene
        SceneDirector.Instance.StartReload(1,1.0f);
    }
}
