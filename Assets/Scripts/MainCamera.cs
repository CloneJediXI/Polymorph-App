﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset = new Vector3(0, 3.1f, -10);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        //transform.position = new Vector3(player.potistion.x + offset.x,)
        transform.position = player.transform.position + offset;
    }
}
