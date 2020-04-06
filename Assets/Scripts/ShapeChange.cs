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

    void Start()
    {
        active = (Behaviour)GetComponent("Halo");
        active.enabled = false;
        playerObj = GameObject.FindGameObjectWithTag("Player");
        blockSize = GetComponent<BlockSize>();
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

            //transform.localScale = playerSize.getSize();
            StartCoroutine(ChangeScaleOverTime(playerSize.getSize()));

            playerSize.changeSize(blockSize.width, blockSize.height);
            //blockSize.width = (int)transform.localScale.x;
            //blockSize.height = (int)transform.localScale.y;
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
