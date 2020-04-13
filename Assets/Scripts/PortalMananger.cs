using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalMananger : MonoBehaviour
{
    private GameObject block;
    private ShapeChange sc;
    private Rigidbody2D rb;
    private GameObject top;
    private GameObject bottom;
    public int speed;
    // Start is called before the first frame update
    void Start()
    {
        top = transform.GetChild(0).gameObject;
        block = transform.GetChild(1).gameObject.transform.GetChild(0).gameObject;
        block.AddComponent<Portal>();
        block.GetComponent<Portal>().setBlock(this);
        rb = block.GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        sc = block.GetComponent<ShapeChange>();
        bottom = transform.GetChild(2).gameObject;
        rb.velocity = new Vector3(0, -speed, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (!sc.frozen)
        {
            rb.velocity = new Vector3(0, -speed, 0);
        }
    }
    public void moveTop()
    {
        block.transform.position = top.transform.position;
    }
    public void moveBottom()
    {
        block.transform.position = bottom.transform.position;
    }
}
