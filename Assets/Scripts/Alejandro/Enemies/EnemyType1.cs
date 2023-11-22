using System.Collections;
using System.Collections.Generic;
using Dan.Demo;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyType1 : EnemyBase
{
    [SerializeField] private AudioClip[] deathSounds;
    
    private AudioSource source;
    private bool gameOver;

    public GameObject L_forearmBone;
    public GameObject L_armBone;
    public GameObject R_forearmBone;
    public GameObject R_armBone;


    private void Start()
    {
        L_forearmBone = GameObject.Find("L_forearmBone");
        L_armBone = GameObject.Find("L_armBone");
        R_forearmBone = GameObject.Find("R_forearmBone");
        R_armBone = GameObject.Find("R_armBone");



        // audio setup
        source = gameObject.GetComponent<AudioSource>();
        source.clip = this.deathSounds[Random.Range(0, deathSounds.Length)];
        source.volume = 0.5f;

    }

    private void Update()
    {
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
                if (!gameOver)
                {
                    gameOverImage.gameObject.SetActive(true);
                    gameOver = true;
                    StartCoroutine(FadeOutAfter(0));
                }
    }

    #region gameover stuff


    IEnumerator FadeOutAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds + .5f);
        StartCoroutine(FadeOut());

        yield return new WaitForSeconds(seconds + .5f);
        
        SceneManager.LoadScene("GameOver");
        
    }

    IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        float fadeTime = .5f;

        
        Color color = gameOverImage.color;

        while (elapsedTime < fadeTime)
        {
            yield return new WaitForEndOfFrame();

            elapsedTime += Time.deltaTime;
            float normalizedTime = elapsedTime / fadeTime;

            color.a = Mathf.Lerp(0f, 1f, normalizedTime);
            gameOverImage.color = color;
        }

        color.a = 1f;
        gameOverImage.color = color;
    }

    IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        float fadeTime = .5f;

        Color color = gameOverImage.color;

        while (elapsedTime < fadeTime)
        {
            yield return new WaitForEndOfFrame();

            elapsedTime += Time.deltaTime;
            float normalizedTime = elapsedTime / fadeTime;

            color.a = Mathf.Lerp(1f, 0f, normalizedTime);
            gameOverImage.color = color;
        }
        color.a = 0f;
        gameOverImage.color = color;
    }

    #endregion

}
