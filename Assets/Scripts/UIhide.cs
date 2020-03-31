using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIhide : MonoBehaviour
{
	public GameObject UI;
	public bool ison;
	void Start()
	{
		ison = true;
	}
    void Update()
    {
        if(Input.GetKeyDown("h"))
		{
			if(ison)
			{
			UI.SetActive(false);
			ison = false;
			}
			else{
			UI.SetActive(true);
			ison = true;
			}
			
		}
		
    }
}
