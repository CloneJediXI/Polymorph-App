using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonMananger : MonoBehaviour
{
    
    public void reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void menu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
