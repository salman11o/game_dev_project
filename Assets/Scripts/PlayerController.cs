using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

using UnityEngine.Experimental.Rendering.Universal; 

public class PlayerController : MonoBehaviour
{
    public float movespeed = 5f;

    public Animator animator;

    public float speedX;
    public float speedY;

    public Transform flashlight;

    public Vector2 movement;

    public Rigidbody2D rb;

    private Grid grid;
    private Tilemap wallTiles;
    private Tilemap floorTiles;
    private Tilemap overlayTiles;
    private Tilemap captainKey;
    private Tilemap quartersDoor;
    private Tilemap controlRoomDoor;
    private Tilemap engine;


    public Joystick joystick;

    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody2D>();
        flashlight = transform.Find("Flashlight");

        grid = FindObjectOfType<Grid>();

        wallTiles = GameObject.Find("WallTiles").GetComponent<Tilemap>();
        floorTiles = GameObject.Find("FloorTiles").GetComponent<Tilemap>();
        overlayTiles = GameObject.Find("ObjectTiles").GetComponent<Tilemap>();
        captainKey = GameObject.Find("CaptainKey").GetComponent<Tilemap>();
        quartersDoor = GameObject.Find("QuartersDoor").GetComponent<Tilemap>();
        controlRoomDoor = GameObject.Find("ControlRoomDoor").GetComponent<Tilemap>();
        engine = GameObject.Find("Engine").GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = joystick.Horizontal;
        movement.y = joystick.Vertical;
        

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


        if (Input.touchCount > 0)
        {
            Vector2 touchPos = Input.GetTouch(0).position;

            Vector3 touchWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(touchPos.x, touchPos.y, 6));

            Vector3Int coordinate = grid.WorldToCell(touchWorldPos);
            
            Debug.Log(coordinate);

            if (overlayTiles.HasTile(coordinate))
            {
                Debug.Log("is");
            }

            if (quartersDoor.HasTile(coordinate))
            {
                Debug.Log("quarterdoor");
                quartersDoor.ClearAllTiles();
            }
        }
    }

    void FixedUpdate() {
        rb.MovePosition(rb.position + movement * movespeed * Time.fixedDeltaTime);

        // Flashlight rotation
        if (Mathf.Abs(movement.x) > 0.01 || Mathf.Abs(movement.y) > 0.01)
        {
            flashlight.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg - 90);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name != "WallTiles")
        {
            if (collision.gameObject.name == "Engine")
            {
                Debug.Log("Near Engine");
            } else if (collision.gameObject.name == "CaptainKey")
            {
                Debug.Log("Collide with Key");
                Debug.Log(collision.otherRigidbody.position);
                captainKey.SetTile(captainKey.WorldToCell(collision.otherRigidbody.position), null);
            }
        }

    }

}
