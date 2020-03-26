using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;//The rigid body on the player
    private GameObject overlord;//The State mananger fo the game
    

    public float speed;
    public float sprintModifier;

    

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        overlord = GameObject.Find("Overlord");
    }

    // Update is called once per frame
    void Update()
    {

        //Checks if moving left or right, no difference right now but might need it later
        if (Input.GetAxisRaw("Horizontal") > 0.0f)
        {
            move(Input.GetButton("Fire3"), Input.GetAxisRaw("Horizontal"));
        }else if (Input.GetAxisRaw("Horizontal") < 0.0f)
        {
            move(Input.GetButton("Fire3"), Input.GetAxisRaw("Horizontal"));
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
    
}
