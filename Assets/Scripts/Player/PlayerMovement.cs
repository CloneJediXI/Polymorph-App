using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;//The rigid body on the player
    private GameObject overlord;//The State mananger fo the game
    private GameState state;

    private int jumpCounter;
    private int maxJumps = 1;//Number of extar jumps you get
    private float jumpPower = 5.5f;//How high you jump
    private float groundCheckDistance = .1f;//How far to check for the ground
    public LayerMask jumpCheckMask; //Set to what you want to be checked ie. the ground
    private GameObject[] bottom;

    private float speed = 6.5f;
    private float sprintModifier = 4f;

    private Animator anim;
    private bool walking;

    public ParticleSystem snowParticalsSystem; 
    public ParticleSystem bubbleParticalsSystem;

    public Color freezeColor = new Color(165, 250, 255);
    public Color floatColor = new Color(217, 217, 217, 242);
    public bool frozen;
	public bool fly;

    public Transform eyeLocation;
    private float eyeStart;
    private float eyeOfset = .1f;

    
    private bool[] feet;

    private bool inWallRight = false;
    private bool inWallLeft = false;
    private bool onGround;
    private bool tempOnground;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        overlord = GameObject.Find("Overlord");
        state = overlord.GetComponent<GameState>();
        bottom = new GameObject[5];
        feet = new bool[5];
        for(int i = 0; i<bottom.Length; i++)
        {
            bottom[i] = transform.GetChild(i+1).gameObject;
        }

        anim = GetComponent<Animator>();
        eyeStart = eyeLocation.localPosition.x;

        bubbleParticalsSystem.Stop();
        snowParticalsSystem.Stop();
    }

    void FixedUpdate()
    {
        grounded();
        if (onGround)
        {
            jumpCounter = 0;
        }
    }
    void Update()
    {
        //If the game is not paused
        if (!state.Paused){
            //Checks if moving left or right, no difference right now but might need it later
            if (Input.GetAxisRaw("Horizontal") > 0.0f){
                move(Input.GetButton("Fire3"), Input.GetAxisRaw("Horizontal"));
                faceRight(true);
            }
            else if (Input.GetAxisRaw("Horizontal") < 0.0f){
                move(Input.GetButton("Fire3"), Input.GetAxisRaw("Horizontal"));
                faceRight(false);
            }
            else{
                if (walking){
                    anim.SetBool("Walk", false);
                    walking = false;
                    eyeLocation.localPosition = new Vector3(eyeStart, eyeLocation.localPosition.y, eyeLocation.localPosition.z);

                }
            }

            if (Input.GetButtonDown("Jump")){
                jump();
            }
        }
    }
    void faceRight(bool right){
        if (right)
        {
            this.transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else
        {
            this.transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1, transform.localScale.y, transform.localScale.z);
        }
    }
    void move(bool sprint, float axisData){
        if (sprint){
            rb.velocity = new Vector2((speed + sprintModifier) * axisData, rb.velocity.y);
        }
        else{
            rb.velocity = new Vector2(speed * axisData, rb.velocity.y);
        }
        eyeLocation.localPosition = new Vector3(eyeStart+eyeOfset, eyeLocation.localPosition.y, eyeLocation.localPosition.z);
        anim.SetBool("Walk", true);
        walking = true;
    }
    void jump(){
        if (jumpCounter < maxJumps){
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            jumpCounter++;
        }
    }
    void grounded()
    {

        RaycastHit2D hit;
        //Check all of the feet
        for (int i = 0; i < bottom.Length; i++)
        {
            //If you are touching the ground
            hit = Physics2D.Raycast(bottom[i].transform.position, -Vector2.up, groundCheckDistance, jumpCheckMask);
            if (hit.collider != null)
            {
                feet[i] = true;
            }
            else
            {
                feet[i] = false;
            }
        }
        tempOnground = false;
        //If it is not in the wall, any of the feet will do
            for (int i = 0; i < feet.Length; i++)
            {
                if (feet[i])
                {
                tempOnground = true;
                }
            }
        if (tempOnground)
        {
            onGround = true;
        }
        else
        {
            StartCoroutine(jumpWait());
        }
    }
    IEnumerator jumpWait()
    {
        yield return new WaitForSeconds(.2f);
        onGround = false;
    }
    public float JumpPower{ get{return jumpPower;} set{jumpPower = value;} }
    public float Speed{ get{return speed;} set{speed = value;} }

    public void freeze(bool toFreeze)
    {
        if (toFreeze)
        {
            //rb.constraints = RigidbodyConstraints2D.FreezeAll;
            anim.SetBool("Walk", false);
            walking = false;
            this.GetComponent<SpriteRenderer>().color = freezeColor;
            snowParticalsSystem.Play();
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            this.GetComponent<SpriteRenderer>().color = Color.white;
            snowParticalsSystem.Stop();
        }
        
        frozen = toFreeze;
        
    }
	public void flight(bool tofly)
    {
		if(tofly)
		{
			rb.gravityScale = -1;
            this.GetComponent<SpriteRenderer>().color = floatColor;
            bubbleParticalsSystem.Play();
        }
		else 
		{
			rb.gravityScale = 1;
            this.GetComponent<SpriteRenderer>().color = Color.white;
            bubbleParticalsSystem.Stop();
        }
		fly = tofly;
	}

    public void freeze()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        anim.SetBool("Walk", false);
        walking = false;
        state.Paused = true;
    }
}
