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

    public List<string> inventory;

    // Start is called before the first frame update
    void Start()
    {
        // To be later implemented as a HUD element
        inventory = new List<string>();

        // Animator is passed absolute speed (x and y) and movement in the x direction
        animator = this.GetComponent<Animator>();

        // Rigidbody handles movement and enables collisions
        rb = this.GetComponent<Rigidbody2D>();

        // transform.Find() finds the child object. Flashlight moves with the player so it's childed.
        flashlight = transform.Find("Flashlight");

        // Used to find coordinates when screen is tapped (in Update() function)
        grid = FindObjectOfType<Grid>();

        // Tilemap objects. These are later to be removed from here and all collisions are
        // to be handled in the tilemap objects themselves. 
        // Used to change and remove tiles. Note that this is not the Tilemap Objects itself but
        // its "Tilemap" Component.
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
        // Vector for movement. Value from -1 to 1 is passed from joystick object 
        // for each axis
        movement.x = joystick.Horizontal;
        movement.y = joystick.Vertical;
        
        // Animator needs an absolute velocity (0-1) to detect if the player
        // is moving. Animation stops when both x and y are less than 0.01
        animator.SetFloat("speedX", Mathf.Abs(movement.x));
        animator.SetFloat("speedY", Mathf.Abs(movement.y));

        // Animator also needs an x movement to see if the player is moving left or right
        // so it can change the animation (from spritesheet) accordingly
        animator.SetFloat("xMove", movement.x);

        // touchCount returns the number of touches currently on screen. 
        if (Input.touchCount > 0)
        {
            // GetTouch(int i) gets number ith touch on screen. The position property is
            // a Vector2. 
            Vector2 touchPos = Input.GetTouch(0).position;

            // Convert the Vector2 touch position to world position in 3D space.
            // The z-coordinate added is the same as that of the grid here.
            Vector3 touchWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(touchPos.x, touchPos.y, 6));

            // We need discrete cell coordinates for our grid instead of world coordinates
            // WorldToCell returns a Vector3Int.
            Vector3Int coordinate = grid.WorldToCell(touchWorldPos);
            
            Debug.Log("touch at" + coordinate);
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
        Debug.Log("collided with " + collision.gameObject.name);


        if (collision.gameObject.name != "WallTiles")
        {
            if (collision.gameObject.name == "Engine")
            {
                Debug.Log("Near Engine");
            } else if (collision.gameObject.name == "CaptainKey")
            {
                Debug.Log(collision.otherRigidbody.position);
                captainKey.ClearAllTiles();
                inventory.Add("CaptainKey");
            } else if (collision.gameObject.name == "QuartersDoor")
            {
                if (inventory.Contains("CaptainKey"))
                {
                    TaskController.instance.EnterDoorArea();
                }
            }
        }

    }


    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "QuartersDoor")
        {
            TaskController.instance.ExitDoorArea();
        }
    }

}
