using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movespeed = 20f;

    public Animator animator;

    public float hf = 0.0f;
    public float vf = 0.0f;

    public Vector2 movement;

    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        

        hf = movement.x > 0.01f ? movement.x : movement.x < -0.01f ? 1 : 0;
        vf = movement.y > 0.01f ? movement.y : movement.y < -0.01f ? 1 : 0;

        animator.SetFloat("Horizontal", hf);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", vf);     
        
    }

    void FixedUpdate() {
        rb.MovePosition(rb.position + movement * movespeed * Time.fixedDeltaTime);
    }
}
