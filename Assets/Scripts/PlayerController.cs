using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movespeed = 5f;

    public Animator animator;

    public float hf;
    public float vf;

    public Vector2 movement;

    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody2D>();

        this.hf = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        

        // hf = movement.x > 0.01f ? movement.x : movement.x < -0.01f ? 1 : 0;
        // vf = movement.y > 0.01f ? movement.y : movement.y < -0.01f ? 1 : 0;

        if (Mathf.Abs(movement.x) < 0.01f && Mathf.Abs(movement.y) < 0.01f)
        {
            animator.SetBool("isMoving", false);
        } else {
            animator.SetBool("isMoving", true);
        }
        animator.SetFloat("xMove", movement.x);
        
    }

    void FixedUpdate() {
        rb.MovePosition(rb.position + movement * movespeed * Time.fixedDeltaTime);
    }
}
