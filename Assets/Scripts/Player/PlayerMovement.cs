using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;//The rigid body on the player
    private GameObject overlord;//The State mananger fo the game
    private GameState state;

    private int jumpCounter;
    private int maxJumps = 1;//Number of extar jumps you get
    private float jumpPower = 7;//How high you jump
    private float groundCheckDistance = .1f;//How far to check for the ground
    public LayerMask jumpCheckMask; //Set to what you want to be checked ie. the ground
    private GameObject bottom;

    private float speed = 7f;
    private float sprintModifier = 4f;

    

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        overlord = GameObject.Find("Overlord");
        state = overlord.GetComponent<GameState>();
        bottom = GameObject.Find("Player/Bottom");
    }

    void FixedUpdate()
    {
        if (grounded())
        {
            jumpCounter = 0;
        }
    }
    void Update()
    {
        //If the game is not paused
        if (!state.Paused)
        {
            //Checks if moving left or right, no difference right now but might need it later
            if (Input.GetAxisRaw("Horizontal") > 0.0f)
            {
                move(Input.GetButton("Fire3"), Input.GetAxisRaw("Horizontal"));
            }else if (Input.GetAxisRaw("Horizontal") < 0.0f){
                move(Input.GetButton("Fire3"), Input.GetAxisRaw("Horizontal"));
            }
            
            if (Input.GetButtonDown("Jump"))
            {
                jump();
            }
        }
        
    }
    void move(bool sprint, float axisData)
    {
        if (sprint){
            rb.velocity = new Vector2((speed + sprintModifier)*axisData, rb.velocity.y);
        }else{
            rb.velocity = new Vector2(speed * axisData, rb.velocity.y);
        }
    }
    void jump()
    {
        if (jumpCounter < maxJumps)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            jumpCounter++;
        }
    }
    bool grounded()
    {
        //If you are touching the ground
        RaycastHit2D hit = Physics2D.Raycast(bottom.transform.position, -Vector2.up, groundCheckDistance, jumpCheckMask);
        if (hit.collider != null)
        {
            return true;
        }
        else return false;
        
    }
    
}
