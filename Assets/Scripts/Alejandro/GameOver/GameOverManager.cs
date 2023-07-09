using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] SpriteRenderer image;
    [SerializeField] SpriteRenderer enemy1;

    private void Start()
    {
        StartCoroutine(FadeOutAfter(2));
    }

    IEnumerator FadeOutAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        StartCoroutine(FadeIn());

        yield return new WaitForSeconds(seconds + 2);
        StartCoroutine(FadeOut());

        yield return new WaitForSeconds(seconds + 2);

        SceneManager.LoadScene("GameOver2");
    }

    IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        float fadeTime = 3f;

        Color color = image.color;

        while (elapsedTime < fadeTime)
        {
            yield return new WaitForEndOfFrame();

            elapsedTime += Time.deltaTime;
            float normalizedTime = elapsedTime / fadeTime;

            color.a = Mathf.Lerp(0f, 1f, normalizedTime);
            image.color = color;
        }

        color.a = 1f;
        image.color = color;
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
    }
}