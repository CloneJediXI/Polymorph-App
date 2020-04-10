using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frozen : MonoBehaviour
{
    public bool frozen;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        //Start with the object frozen or not
        if (frozen)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (frozen)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.None;
        }
    }

    public bool Freeze {
        get
        {
            return frozen;
        }
        set
        {
            frozen = value;
        }
    }
}
