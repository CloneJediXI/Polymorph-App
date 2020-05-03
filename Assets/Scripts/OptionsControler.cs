using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OptionsControler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool holding;
    private float time = 0;
    public float holdTime;
    private bool show;
    public GameObject optionsPannel;
    private ColorBlock startColorBlock;
    private Color startColor;
    private Button button;
    public void OnPointerDown(PointerEventData eventData)
    {
        if (show)
        {
            show = false;
            optionsPannel.SetActive(false);
        }
        else
        {
            holding = true;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        holding = false;
        time = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        startColorBlock = button.colors;
        startColor = startColorBlock.pressedColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (holding)
        {
            time += Time.deltaTime;
            startColorBlock.pressedColor = Color.Lerp(startColor, Color.white, time / holdTime);
            button.colors = startColorBlock;
            if(time > holdTime)
            {
                show = true;
                optionsPannel.SetActive(true);
            }
        }
    }
}
