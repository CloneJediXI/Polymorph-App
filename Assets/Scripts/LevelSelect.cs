using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    public int numLevels;
    public GameObject[] levels;
    public int[] stars;
    public Sprite goldStar;
    // Start is called before the first frame update
    void Start()
    {
        GameObject canvas = GameObject.Find("Canvas");
        levels = new GameObject[numLevels];
        stars = new int[numLevels];
        for(int i=0; i<numLevels; i++)
        {
            levels[i] = canvas.transform.GetChild(i + 3).gameObject;
        }
        read();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void read()
    {
        String line;
        try
        {
            //Pass the file path and file name to the StreamReader constructor
            StreamReader sr = new StreamReader("Assets/Test.txt");
            Debug.Log("Reading...");
            String[] parts;
            for (int i =0; i<levels.Length; i++)
            {
                line = sr.ReadLine();
                parts = line.Split(',');
                stars[i] = int.Parse(parts[1]);
            }
            Debug.Log("Done Reading...");
            //close the file
            sr.Close();
            setStars();
        }
        catch (Exception e)
        {
            loadDefaults();
            Debug.LogError(e);
        }
    }
    void loadDefaults()
    {
        StreamWriter sw = new StreamWriter("Assets/Test.txt");
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
    void write()
    {
        try
        {
            //Pass the filepath and filename to the StreamWriter Constructor
            StreamWriter sw = new StreamWriter("Assets/Test.txt");
            //Write a line of text
            sw.WriteLine("Hello World!!");
            //Write a second line of text
            sw.WriteLine("From the StreamWriter class");
            //Close the file
            sw.Close();
        }
        catch (Exception e)
        {
            Debug.Log("Exception: " + e.Message);
        }
        finally
        {
            Debug.Log("Executing finally block.");
        }
    }

}
