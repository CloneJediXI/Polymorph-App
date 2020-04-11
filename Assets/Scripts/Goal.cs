using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Goal : MonoBehaviour
{
    public Animator glassDoorAnima;
    public GameObject completionMessage;
    public string nextScene;
    public Sprite goldStar;
    private GameObject overlord;
    private int numStars = 0;
    private int swaps;
    private LevelData data;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        //print("collided door");
        Transform t = collision.transform;
        if (t.CompareTag("Player"))
        {
            glassDoorAnima.SetBool("Close", true);
            collision.gameObject.GetComponent<PlayerMovement>().freeze();
            collision.gameObject.GetComponent<PlayerSize>().reset();
            GameObject.Find("Overlord").GetComponent<GameState>().Paused = true;
            t.position = transform.position;
            StartCoroutine(LoadNextScene());
        }
    }

    IEnumerator LoadNextScene()
    {
        overlord = GameObject.Find("Overlord");
        yield return new WaitForSeconds(1.3f);
        
        completionMessage.SetActive(true);
        
        GameState state = overlord.GetComponent<GameState>();
        data = overlord.GetComponent<LevelData>();
        Text parText = completionMessage.transform.GetChild(4).GetComponent<Text>();
        Text scoreText = completionMessage.transform.GetChild(5).GetComponent<Text>();
        parText.text = "Par Score : " + data.parSwaps;
        scoreText.text = "Your Score : ";
        swaps = state.getSwaps();
        saveData();
        yield return new WaitForSeconds(.5f);
        numStars = 0;
        if (swaps <= data.firstStar)
        {
            completionMessage.transform.GetChild(1).GetComponent<Image>().sprite = goldStar;
            numStars++;
        }
        yield return new WaitForSeconds(.5f);
        if (swaps <= data.SecondStar)
        {
            completionMessage.transform.GetChild(2).GetComponent<Image>().sprite = goldStar;
            numStars++;
        }
        yield return new WaitForSeconds(.5f);
        if (swaps <= data.parSwaps)
        {
            completionMessage.transform.GetChild(3).GetComponent<Image>().sprite = goldStar;
            numStars++;
        }
        yield return new WaitForSeconds(.5f);
        scoreText.text = "Your Score : " + state.getSwaps();
    }
    void saveData()
    {
        numStars = 0;
        if (swaps <= data.firstStar)
        {
            numStars++;
        }
        if (swaps <= data.SecondStar)
        {
            numStars++;
        }
        if (swaps <= data.parSwaps)
        {
            numStars++;
        }
        if (PlayerPrefs.HasKey("level_" + (data.levelNumber-1)))
        {
            int temp = PlayerPrefs.GetInt("level_" + (data.levelNumber - 1));
            if(numStars > temp)
            {
                PlayerPrefs.SetInt("level_"+ (data.levelNumber - 1), numStars);
            }
        }
        else
        {
            PlayerPrefs.SetInt("level_" + (data.levelNumber - 1), numStars);
        }
        /*
        String line;
        String total = "";
        numStars = 0;
        if (swaps <= data.firstStar)
        {
            numStars++;
        }
        if (swaps <= data.SecondStar)
        {
            numStars++;
        }
        if (swaps <= data.parSwaps)
        {
            numStars++;
        }
        try
        {
            //Pass the file path and file name to the StreamReader constructor
            StreamReader sr = new StreamReader("Assets/Data.txt");
            Debug.Log("Reading...");
            String[] parts;
            line = sr.ReadLine();
            int level = overlord.GetComponent<LevelData>().levelNumber;
            while (line != null)
            {
                parts = line.Split(',');
                //Once you get to the right level key
                if(int.Parse(parts[0]) == (level-1))
                {
                    //if your new score is better
                    if(numStars > int.Parse(parts[1]))
                    {
                        //Save score
                        total += "" + (level-1) + "," + numStars+"\n";
                    }
                    else
                    {
                        //Don't change the data
                        total += line + "\n";
                    }
                    
                }
                else
                {
                    //Don't change the data
                    total += line + "\n";
                }
                line = sr.ReadLine();
            }
            Debug.Log("Done Reading...");
            //close the file
            sr.Close();
            //Pass the filepath and filename to the StreamWriter Constructor
            StreamWriter sw = new StreamWriter("Assets/Data.txt", false);
            Debug.Log("Writing: "+ total);
            sw.Write(total);
            sw.Close();
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }*/
    }
    public void retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void next()
    {
        SceneManager.LoadScene(nextScene);
    }
}
