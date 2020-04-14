using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{

    private GameObject Block;
    private GameObject playerObj = null;
    private PlayerSize pSize;
    public int range = 7;

    public GameObject haloSprite;

    // Start is called before the first frame update
    void Start()
    {
        Block = this.gameObject;
        haloSprite.SetActive(false);
        playerObj = GameObject.FindGameObjectWithTag("Player");
        pSize = playerObj.GetComponent<PlayerSize>();
    }
    void OnMouseOver()
    {
        if (Vector3.Distance(Block.transform.position, playerObj.transform.position) < range)
        {
            if (pSize.interacting)
            {
                haloSprite.SetActive(true);
            }
        }
    }

    void OnMouseExit()
    {
        haloSprite.SetActive(false);
    }
    void OnMouseDown()
    {
        if (haloSprite.activeSelf == true)
        {
            Vector3 temp = this.transform.position;
            transform.position = playerObj.transform.position;
            playerObj.transform.position = temp;
        }
    }
}
