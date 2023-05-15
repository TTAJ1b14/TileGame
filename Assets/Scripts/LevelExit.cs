using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 3f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        Debug.Log ("You Finished!");
        StartCoroutine(LoadNextLevel());
    }
    
    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSecondsRealtime (levelLoadDelay);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
        nextSceneIndex = 1;
        }

        FindObjectOfType<ScenePersist>().ResetCurrentGameScene();
        SceneManager.LoadScene(nextSceneIndex);
        
    }
}
 