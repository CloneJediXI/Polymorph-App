using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeChange : MonoBehaviour
{
	
     public Behaviour active;
     public GameObject Block;
     private GameObject playerObj = null;
     public int range = 7;
    float ratioX;
    float ratioY;

    void Start()
     {
	     active = (Behaviour)GetComponent("Halo");
	     active.enabled = false;
	     playerObj = GameObject.FindGameObjectWithTag("Player");
        BlockSize temp = GetComponent<BlockSize>();
        ratioX = transform.localScale.x / temp.width;
        ratioY = transform.localScale.y / temp.height;
    }
     void OnMouseOver(){
        if(Vector3.Distance(Block.transform.position, playerObj.transform.position) < range)
        {
	        if(Input.GetKey("e"))
	        {		  
            active.enabled = true;
	        }
	        if(Input.GetKeyUp("e"))
	        {
		        print("TEST");
	        }
        }
	}

    void OnMouseExit()
    {
      active.enabled = false;
    }
    void OnMouseDown()
    {
        if(active.enabled == true)
        {
            PlayerSize playerSize = playerObj.GetComponent<PlayerSize>();
            BlockSize temp = GetComponent<BlockSize>();
            float x = playerSize.getWidth();
            float y = playerSize.getHeight();
            transform.localScale = new Vector3(x * ratioX, y * ratioY, transform.localScale.z);
            playerSize.changeSize(temp.width, temp.height);
            temp.width = (int)x;
            temp.height = (int)y;
        }
    }
	
}
