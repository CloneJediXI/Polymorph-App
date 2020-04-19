using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalMananger : MonoBehaviour
{
    private GameObject[] blocks;
    private ShapeChange[] sc;
    private Rigidbody2D[] rb;

    private GameObject top;
    private GameObject bottom;

    public int speed;
    // Start is called before the first frame update
    void Start()
    {
        top = transform.GetChild(0).gameObject;
        Transform blockHolder = transform.GetChild(1);
        bottom = transform.GetChild(2).gameObject;

        blocks = new GameObject[blockHolder.childCount];
        sc = new ShapeChange[blocks.Length];
        rb = new Rigidbody2D[blocks.Length];

        for(int i = 0; i < blocks.Length; i++)
        {
            blocks[i] = blockHolder.GetChild(i).gameObject;
            blocks[i].AddComponent<Portal>();
            blocks[i].GetComponent<Portal>().setBlock(this);
            sc[i] = blocks[i].GetComponent<ShapeChange>();
            rb[i] = blocks[i].GetComponent<Rigidbody2D>();
            rb[i].gravityScale = 0;
            rb[i].velocity = new Vector3(0, -speed, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < blocks.Length; i++)
        {
            if (!sc[i].frozen && speed != 0)
            {
                rb[i].velocity = new Vector3(0, -speed, 0);
            }
        }
    }
    public float getTopY()
    {
        return top.transform.position.y;
    }
    public float getBotY()
    {
        return bottom.transform.position.y;
    }
}
