using UnityEngine;
using UnityEngine.EventSystems;

public class UIhide : MonoBehaviour, IPointerDownHandler
{
    public GameObject UI;
    public bool ison;

    public void OnPointerDown(PointerEventData eventData)
    {
        this.gameObject.SetActive(false);
    }

    void Start()
    {
        ison = true;
    }
    /*void Update()
    {
        
        if (Input.GetKeyDown("h"))
        {
            if (ison)
            {
                UI.SetActive(false);
                ison = false;
            }
            else
            {
                UI.SetActive(true);
                ison = true;
            }

        }

    }*/
}
