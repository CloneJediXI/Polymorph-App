using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    public Animator glassDoorAnima;

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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
