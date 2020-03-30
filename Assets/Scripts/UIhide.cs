using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIhide : MonoBehaviour
{
	public GameObject UI;
    void Update()
    {
        if(Input.GetKey("h"))
		{
			UI.SetActive(false);
		}
    }
}
