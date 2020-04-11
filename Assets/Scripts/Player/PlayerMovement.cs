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

    private float speed = 7f;
    private float sprintModifier = 4f;

    private Animator anim;
    private bool walking;

    public bool frozen;
    public Transform eyeLocation;
    private float eyeStart;
    public float eyeOfset;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        overlord = GameObject.Find("Overlord");
        state = overlord.GetComponent<GameState>();
        bottom = new GameObject[] { GameObject.Find("Player/Bottom1"), GameObject.Find("Player/Bottom2"), GameObject.Find("Player/Bottom3") };

        anim = GetComponent<Animator>();
        eyeStart = eyeLocation.localPosition.x;
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
        //GetComponent<SpriteRenderer>().flipX = (!right);
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
    bool grounded()
    {
        for (int i = 0; i < bottom.Length; i++)
        {
            //If you are touching the ground
            RaycastHit2D hit = Physics2D.Raycast(bottom[i].transform.position, -Vector2.up, groundCheckDistance, jumpCheckMask);
            if (hit.collider != null)
            {
                return true;
            } 
        }
        return false;

    }
    public float JumpPower{ get{return jumpPower;} set{jumpPower = value;} }
    public float Speed{ get{return speed;} set{speed = value;} }

    public void freeze(bool toFreeze)
    {
        if (toFreeze)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            anim.SetBool("Walk", false);
            walking = false;
            this.GetComponent<SpriteRenderer>().color = Color.blue;
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            this.GetComponent<SpriteRenderer>().color = Color.white;
        }
        state.Paused = toFreeze;
        frozen = toFreeze;
        
    }
    public void freeze()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        anim.SetBool("Walk", false);
        walking = false;
    }
}
