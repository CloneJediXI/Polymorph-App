using UnityEngine;

public class ShapeChange : MonoBehaviour
{

    public Behaviour active;
    public GameObject Block;
    private GameObject playerObj = null;
    public int range = 7;

    void Start()
    {
        active = (Behaviour)GetComponent("Halo");
        active.enabled = false;
        playerObj = GameObject.FindGameObjectWithTag("Player");
        BlockSize temp = GetComponent<BlockSize>();
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
        if (active.enabled == true)
        {
            PlayerSize playerSize = playerObj.GetComponent<PlayerSize>();
            BlockSize blockSize = GetComponent<BlockSize>();
            transform.localScale = playerSize.getSize();
            playerSize.changeSize(blockSize.width, blockSize.height);
            blockSize.width = (int)transform.localScale.x;
            blockSize.height = (int)transform.localScale.y;
        }
    }

}
