using System.Collections;
using System.Collections.Generic;
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
        yield return new WaitForSeconds(1.5f);
        completionMessage.SetActive(true);
        overlord = GameObject.Find("Overlord");
        GameState state = overlord.GetComponent<GameState>();
        LevelData data = overlord.GetComponent<LevelData>();
        int swaps = state.getSwaps();
        if(swaps <= data.firstStar)
        {
            completionMessage.transform.GetChild(1).GetComponent<Image>().sprite = goldStar;
        }
        if (swaps <= data.SecondStar)
        {
            completionMessage.transform.GetChild(2).GetComponent<Image>().sprite = goldStar;
        }
        if (swaps <= data.parSwaps)
        {
            completionMessage.transform.GetChild(3).GetComponent<Image>().sprite = goldStar;
        }
        Text parText= completionMessage.transform.GetChild(4).GetComponent<Text>();
        parText.text = "Par Score : " + data.parSwaps;
        Text scoreText = completionMessage.transform.GetChild(5).GetComponent<Text>();
        scoreText.text = "Your Score : " + state.getSwaps();
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
