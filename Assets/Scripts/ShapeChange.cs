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

    private ParticleSystem snowParticalsSystem;
    public bool frozen;
    private Rigidbody2D rb;
    private GameState state;

    void Start()
    {
        active = (Behaviour)GetComponent("Halo");
        active.enabled = false;
        playerObj = GameObject.FindGameObjectWithTag("Player");
        blockSize = GetComponent<BlockSize>();

        rb = GetComponent<Rigidbody2D>();
        state = GameObject.Find("Overlord").GetComponent<GameState>();
        snowParticalsSystem = GetComponentInChildren<ParticleSystem>();
        //Start with the object frozen or not
        if (frozen)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            this.GetComponent<SpriteRenderer>().color = Color.blue;
        }
        else
        {
            snowParticalsSystem.Pause();
        }
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
    }
    void OnMouseOver()
    {
        if (Vector3.Distance(Block.transform.position, playerObj.transform.position) < range)
        {
            if (Input.GetKey("e"))
            {
                active.enabled = true;
            }

        }
    }

    void OnMouseExit()
    {
        active.enabled = false;
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
                this.GetComponent<SpriteRenderer>().color = Color.blue;
                frozen = true;
                snowParticalsSystem.Play();
                playerMovement.freeze(false);
                state.swap();
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
