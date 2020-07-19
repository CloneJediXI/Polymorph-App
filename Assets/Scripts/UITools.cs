using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITools : MonoBehaviour
{
    private Button positive;
    private Button negative;
    private bool positiveClicked;
    private bool negativeClicked;
    public GameObject prompt;
    // Start is called before the first frame update
    public void Prompt(Func<bool> pos, string message)
    {
        GameObject promptInst = Instantiate(prompt, GameObject.FindGameObjectWithTag("Canvas").transform);
        Transform card = promptInst.transform.GetChild(0);
        card.GetChild(0).GetComponent<Text>().text = message;
        positive = card.GetChild(1).GetComponent<Button>();
        positive.onClick.AddListener(delegate { pos(); Destroy(promptInst); });
        negative = card.GetChild(2).GetComponent<Button>();
        negative.onClick.AddListener(delegate { Destroy(promptInst); });
        
    }
    public void Close(GameObject gm)
    {
        Destroy(gm);
    }
}
