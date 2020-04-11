using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    private int numSwaps;
    public bool Paused { get; set; }
    public void swap()
    {
        numSwaps++;
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
