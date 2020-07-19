using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonMovement : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private PlayerMovement PM;
    public bool moveRight;
    public bool jump;
    private bool moving;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (jump)
        {
            PM.buttonJump();
        }
        else
        {
            moving = true;
            PM.buttonDown = true;
            Debug.Log("Button is " + (PM.buttonDown ? "Down" : "Up"));
        }
        

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        moving = false;
        if (!jump)
        {
            PM.buttonDown = false;
            Debug.Log("Button is "+ (PM.buttonDown ? "Down" : "Up"));
        }
    }

    void Start()
    {
        PM = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            PM.move(moveRight);
            
        }
    }
}
