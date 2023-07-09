using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver2Manager : MonoBehaviour
{
    [SerializeField] GameObject bannerAndButton;
    [SerializeField] SpriteRenderer image;


    private void Start()
    {
        StartCoroutine(FadeOutAfter(2));

        StartCoroutine(GoBackToMainMenu());
    }


    IEnumerator DisplayBannerAndButton()
    {
        yield return new WaitForSeconds(2);

        bannerAndButton.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("StartScreen");
    }

    IEnumerator FadeOutAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        StartCoroutine(FadeIn());

        yield return new WaitForSeconds(seconds + 2);
        StartCoroutine(DisplayBannerAndButton());
    }

    IEnumerator GoBackToMainMenu()
    {
        yield return new WaitForSeconds(100);

        RestartGame();
    }

    IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        float fadeTime = 3f;

        Color color = image.color;

        while (elapsedTime < fadeTime)
        {
            yield return new WaitForEndOfFrame();

            elapsedTime += Time.deltaTime;
            float normalizedTime = elapsedTime / fadeTime;

            color.a = Mathf.Lerp(1f, 0f, normalizedTime);
            image.color = color;
        }
        color.a = 0f;
        image.color = color;

        image.gameObject.SetActive(false);
    }
}
