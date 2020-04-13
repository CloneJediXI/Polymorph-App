using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private PortalMananger pm;
    private bool inTop;
    private bool inBottom;
    
    public void setBlock(PortalMananger pm)
    {
        this.pm = pm;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "PTop" && !inTop)
        {
            Vector3 newPos = new Vector3(transform.position.x, pm.getBotY());
            transform.position = newPos;
            inBottom = true;
        }else if(other.tag == "PBottom" && !inBottom)
        {
            Vector3 newPos = new Vector3(transform.position.x, pm.getTopY());
            transform.position = newPos;
            inTop = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "PTop") {
            inTop = false;
        }
        if(other.tag == "PBottom")
        {
            inBottom = false;
        }
        
    }
}
