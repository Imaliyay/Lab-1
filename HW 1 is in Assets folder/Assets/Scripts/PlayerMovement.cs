using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public KeyCode moveLeft = KeyCode.A;
    public KeyCode moveRight = KeyCode.D;
    public KeyCode jump = KeyCode.Space;

    public float speedMultiplier = 5.0f;
    public float jumpPower = 5.0f;
    public Transform groundDetection;
    public LayerMask ground;
    public LayerMask obstacle;
    private Rigidbody2D rb; // SETS RIDGED BODY POSITION
    private bool isGrounded;
    private bool isObstacle;
    int JumpCount = 0;
    // Start is called before the first frame update
    void Start()
    {
       rb = GetComponent<Rigidbody2D>(); 
    }

    private void FixedUpdate() // USED FOR PHYSICS
    {
        isGrounded = Physics2D.OverlapCircle(groundDetection.position, 0.1f, ground);
        isObstacle = Physics2D.OverlapCircle(groundDetection.position, 0.1f, obstacle);

        Debug.Log("isGrounded" + isGrounded);
        Debug.Log("isObstacle" + isObstacle);
        if (isGrounded || isObstacle)
        {
            JumpCount = 0;
            if (Input.GetKeyDown(jump))
            {
                rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);  //Impulse is more NATURAL
                Debug.Log("I am jumpijng!");
                JumpCount += 1;
            }
        }
        else if (Input.GetKeyDown(jump))
        {
            if (JumpCount < 2)
            {
                rb.AddForce(Vector2.up * jumpPower *1.5f, ForceMode2D.Impulse);
                JumpCount += 1;
            }
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        Vector2 newMovement;

        if (Input.GetKey(moveLeft))
        {
            newMovement = new Vector2(rb.position.x - (Time.deltaTime * speedMultiplier), rb.position.y);
            rb.position = newMovement;
        }
        if (Input.GetKey(moveRight))
        {
            newMovement = new Vector2(rb.position.x + (Time.deltaTime * speedMultiplier), rb.position.y);
            rb.position = newMovement;
        }
    }
}
