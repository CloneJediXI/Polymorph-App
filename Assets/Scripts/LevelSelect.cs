using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    static readonly string LEVEL = "level_";

    public int numLevels;
    public GameObject[] levels;
    public int[] stars;
    public Sprite goldStar;

    public bool mainMenu;

    public GameObject holder;
    
    // Start is called before the first frame update
    void Start()
    {
        if (!mainMenu)
        {
            levels = new GameObject[numLevels];
            stars = new int[numLevels];
            for (int i = 0; i < numLevels; i++)
            {
                levels[i] = holder.transform.GetChild(i).gameObject;
            }
            read();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void read()
    {
        for (int i = 0; i < levels.Length; i++)
        {
            if (PlayerPrefs.HasKey(LEVEL + i))
            {
                Debug.Log("Has Key " + LEVEL + i);
                stars[i] = PlayerPrefs.GetInt(LEVEL + i);
            }
            else
            {
                Debug.Log("No Key " + LEVEL + i);
                stars[i] = 0;
            }
        }
        setStars();
    }
    void loadDefaults()
    {
        StreamWriter sw = new StreamWriter("Assets/Data.txt");
        for(int i=0; i<levels.Length; i++)
        {
            sw.WriteLine(""+i+","+"0");
        }
        sw.Close();
    }
    void setStars()
    {
        Debug.Log("Processing Stars...");
        for (int i=0; i<stars.Length; i++)
        {
            if (stars[i] >= 1)
            {
                levels[i].transform.GetChild(1).GetComponent<Image>().sprite = goldStar;
            }
            if (stars[i] >= 2)
            {
                levels[i].transform.GetChild(2).GetComponent<Image>().sprite = goldStar;
            }
            if (stars[i] >= 3)
            {
                levels[i].transform.GetChild(3).GetComponent<Image>().sprite = goldStar;
            }
        }
    }
    public void back()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void loadScene(String name)
    {
        SceneManager.LoadScene(name);
    }
    public void quit()
    {
        Application.Quit();
    }
    public void resetAll()
    {
        PlayerPrefs.DeleteAll();
    }
}
