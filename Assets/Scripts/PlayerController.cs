using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movespeed = 5f;

    public Animator animator;

    public float speedX;
    public float speedY;

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
        

        // hf = movement.x > 0.01f ? movement.x : movement.x < -0.01f ? 1 : 0;
        // vf = movement.y > 0.01f ? movement.y : movement.y < -0.01f ? 1 : 0;


        if (Mathf.Abs(movement.x) > 0.01f || Mathf.Abs(movement.y) > 0.01f)
        {
            speedX = Mathf.Abs(movement.x);
            speedY = Mathf.Abs(movement.y);

            animator.SetFloat("speedX", speedX);
            animator.SetFloat("speedY", speedY);
        }
        animator.SetFloat("xMove", movement.x);
        /*
        animator.SetFloat("speedX", Mathf.Abs(movement.x));
        animator.SetFloat("speedY", Mathf.Abs(movement.y));
        */
    }

    void FixedUpdate() {
        rb.MovePosition(rb.position + movement * movespeed * Time.fixedDeltaTime);
    }
}
