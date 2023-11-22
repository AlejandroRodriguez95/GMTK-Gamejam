using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] float loadDelay = 3f;
    int currentSceneIndex;
    // Start is called before the first frame update
    void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        /*if (currentSceneIndex == 0)
        {
            StartCoroutine(LoadScreenDelay());
        }*/
        
    }

    private IEnumerator LoadScreenDelay()
    {
        yield return new WaitForSeconds(loadDelay);
        LoadNextScene();
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void LoadTitle()
    {
        SceneManager.LoadScene("Title Screen");
    }

    public void LoadOptionsScreen()
    {
        SceneManager.LoadScene("Options Screen");
    }

    public void LoadGameOver()
    {
        //StartCoroutine(LoadScreenDelay());
        SceneManager.LoadScene("Game Over");
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(currentSceneIndex);
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

 
