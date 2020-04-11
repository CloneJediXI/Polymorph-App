using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eyes : MonoBehaviour
{
    public Transform target;
    public float speed = .9F;
    

    void FixedUpdate()
    {
        /*if((transform.position - target.position).magnitude > radius)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, (transform.position - target.position).magnitude - radius);
        }*/
        //float temp = Mathf.Abs((transform.position - target.position).magnitude) * speed;
        transform.position = Vector3.Lerp(transform.position, target.position, speed);

    }
}
