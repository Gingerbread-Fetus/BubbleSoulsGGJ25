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

    public float speed = 1.0f;
    public float jumpForce = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_jumpWait)
        {
            _grounded = Grounded();
            _jumpWait = false;
        }
    }

    void FixedUpdate()
    {
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        var axisInput = context.ReadValue<Vector2>();
        float horizontalInput = axisInput.x;
        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
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
        RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, -Vector2.up, 1.7f, LayerMask.GetMask("Ground"));
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
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            _grounded = false;
            StartCoroutine(JumpFrameSkipRoutine());
        }
    }

    public void TakeDamage()
    {
        Destroy(gameObject, .2f);
        FindObjectOfType<SceneDirector>().Reload();
    }

}
