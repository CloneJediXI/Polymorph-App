using UnityEngine;
using System.Collections;

public class ShapeChange : MonoBehaviour
{

    public Behaviour active;
    public GameObject Block;
    private GameObject playerObj = null;
    public int range = 7;
    private BlockSize blockSize;

    private bool changingSize;

    public GameObject haloSprite;
    public ParticleSystem snowParticalsSystem;
    public ParticleSystem bubbleParticalsSystem;

    public bool frozen;
    public Color freezeColor = new Color(165, 250, 255);
    public Color floatColor = new Color(180, 180, 180, 180);
	public bool fly;
    private Rigidbody2D rb;
    private GameState state;

    void Start()
    {
        active = (Behaviour)GetComponent("Halo");
        active.enabled = false;
        haloSprite.SetActive(false);

        playerObj = GameObject.FindGameObjectWithTag("Player");
        blockSize = GetComponent<BlockSize>();

        rb = GetComponent<Rigidbody2D>();
        state = GameObject.Find("Overlord").GetComponent<GameState>();
        //Start with the object frozen or not
        if (frozen)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            this.GetComponent<SpriteRenderer>().color = freezeColor;
        }
        else
        {
            snowParticalsSystem.Stop();
        }
        if(fly)
            this.GetComponent<SpriteRenderer>().color = floatColor;
        else
            bubbleParticalsSystem.Stop();
    }
    void Update()
    {
        if (frozen)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
		if(fly)
		{
			rb.gravityScale = -1;
		}
		else{
			rb.gravityScale = 1;
		}
    }
    void OnMouseOver()
    {
        if (Vector3.Distance(Block.transform.position, playerObj.transform.position) < range)
        {
            if (Input.GetKey("e"))
            {
                active.enabled = true;
                haloSprite.SetActive(true);
            }

        }
    }

    void OnMouseExit()
    {
        active.enabled = false;
        haloSprite.SetActive(false);
    }
    void OnMouseDown()
    {
        if (active.enabled == true && !changingSize)
        {
            PlayerSize playerSize = playerObj.GetComponent<PlayerSize>();
            PlayerMovement playerMovement = playerObj.GetComponent<PlayerMovement>();
            //If the block is frozen and the player is not
            if (frozen && !playerMovement.frozen)
            {
                //Unfreze the block and freeze the player
                this.GetComponent<SpriteRenderer>().color = Color.white;
                frozen = false;
                snowParticalsSystem.Stop();
                playerMovement.freeze(true);
                state.swap();
            }else if(!frozen && playerMovement.frozen)//if the block is not frozen and the player is
            {
                this.GetComponent<SpriteRenderer>().color = freezeColor;
                frozen = true;
                snowParticalsSystem.Play();
                playerMovement.freeze(false);
                state.swap();
            }
			else if(fly && !playerMovement.fly)
			{
				fly = false;
                bubbleParticalsSystem.Stop();
                this.GetComponent<SpriteRenderer>().color = Color.white;
                playerMovement.flight(true);
			}
			else if(!fly && playerMovement.fly)
			{
				fly = true;
                bubbleParticalsSystem.Play();
                this.GetComponent<SpriteRenderer>().color = floatColor;
                playerMovement.flight(false);
			}
            else
            {
                //swap size
                StartCoroutine(ChangeScaleOverTime(playerSize.getSize()));
                playerSize.changeSize(blockSize.width, blockSize.height);
                state.swap();
            }
            
        }
    }

    IEnumerator ChangeScaleOverTime(Vector3 playerScale)
    {
        float time = 20;
        changingSize = true;
        playerScale = new Vector3(Mathf.Abs(playerScale.x), playerScale.y, playerScale.z);
        Vector3 originalScale = transform.localScale;

        for (int i = 0; i <= time; i++)
        {
            transform.localScale = Vector3.Lerp(originalScale, playerScale, i / time);
            yield return null;
        }

        changingSize = false;
        transform.localScale = playerScale;

        blockSize.width = (int)transform.localScale.x;
        blockSize.height = (int)transform.localScale.y;
    }

}
