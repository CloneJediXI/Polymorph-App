using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameState : MonoBehaviour
{
    public Text score;
    private int numSwaps;
    public bool Paused { get; set; }
    public void swap()
    {
        try
        {
            numSwaps++;
            score.text = "Swaps: " + numSwaps;
        }catch(NullReferenceException e)
        {

        }
        
    }
    public int getSwaps()
    {
        return numSwaps;
    }
    public void resetSwaps()
    {
        numSwaps = 0;
    }
    public void resetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
