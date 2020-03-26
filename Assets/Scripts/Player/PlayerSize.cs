using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSize : MonoBehaviour
{
    private Vector3 startingSize;
    private float initSpeed;
    private float initJumpPower;
    private bool interacting;
    private Behaviour halo;
    private Vector3 mousePosition;
    private LineRenderer line;

    // Start is called before the first frame update
    void Start()
    {
        startingSize = this.transform.localScale;
        initSpeed = this.GetComponent<PlayerMovement>().Speed;
        initJumpPower = this.GetComponent<PlayerMovement>().JumpPower;
        halo = (Behaviour)GetComponent("Halo");
        halo.enabled = false;
        line = GetComponent<LineRenderer>();
        line.positionCount = 2;
        line.enabled = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        //E is the interact key
        if (Input.GetKey("e"))
        {
            interacting = true;
            halo.enabled = true;
            line.enabled = true;
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            drawLine();
        }
        if (Input.GetKeyUp("e"))
        {
            interacting = false;
            halo.enabled = false;
            line.enabled = false;
        }
        //R removes all the scale modifers
        if (Input.GetKeyDown("r"))
        {
            transform.localScale = startingSize;
            this.GetComponent<PlayerMovement>().JumpPower = initJumpPower;
            this.GetComponent<PlayerMovement>().Speed = initSpeed;
        }
        if(Input.GetButtonDown("Fire1") && interacting)
        {
            interactCheck();
        }
    }
    //A 1x1 is origional size
    public void changeSize(int width, int height)
    {
        transform.localScale = new Vector3(width, height, 1);
        //                   jump increases by 2 for each unit taller it is
        this.GetComponent<PlayerMovement>().JumpPower = initJumpPower + (2*(height-1));
        //                          Same with speed
        this.GetComponent<PlayerMovement>().Speed = initSpeed + (2 * (width - 1));
    }
    private void drawLine()
    {
        line.SetPosition(0, this.transform.position);
        line.SetPosition(1, mousePosition);
    }
    public void interactCheck()
    {

    }
}
