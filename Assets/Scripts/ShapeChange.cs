using System.Collections;
using System.Collections.Generic;
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
 }
 void OnMouseOver()
    {
	if(Vector3.Distance(Block.transform.position, playerObj.transform.position) < range)
	{
      active.enabled = true;
    }
	}

    void OnMouseExit()
    {
      active.enabled = false;
    }
}
