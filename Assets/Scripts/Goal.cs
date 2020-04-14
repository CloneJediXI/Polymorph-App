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
        GameObject button = completionMessage.transform.GetChild(7).gameObject;
        button.GetComponent<Button>().interactable = false;
        
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
            yield return new WaitForSeconds(.5f);
        }
        
        if (swaps <= data.SecondStar)
        {
            completionMessage.transform.GetChild(2).GetComponent<Image>().sprite = goldStar;
            numStars++;
            yield return new WaitForSeconds(.5f);
        }
        
        if (swaps <= data.parSwaps)
        {
            completionMessage.transform.GetChild(3).GetComponent<Image>().sprite = goldStar;
            numStars++;
            yield return new WaitForSeconds(.5f);
        }
        scoreText.text = "Your Score : " + state.getSwaps();
        button.GetComponent<Button>().interactable = true;
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
