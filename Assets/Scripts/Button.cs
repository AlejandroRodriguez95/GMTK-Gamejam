using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private AudioClip[] hoverClips;
    [SerializeField] private AudioClip clickClip;

    private AudioSource source;
    private float clickClipLength;
    
    void Start()
    {
        this.source = gameObject.GetComponent<AudioSource>();
        this.clickClipLength = this.clickClip.length;
    }

    public void OnPointerEnter(PointerEventData data)
    {
        this.source.PlayOneShot(this.getClip());
        Debug.Log("Playing");
    }

    public void OnPlayButton()
    {
        this.source.Stop();
        this.source.clip = this.clickClip;
        this.source.Play();
        StartCoroutine(this.NavigateAfterSound(this.source));
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }

    private IEnumerator NavigateAfterSound(AudioSource source)
    {
        yield return new WaitForSecondsRealtime(this.clickClipLength);
        SceneManager.LoadScene(1);
    }

    private AudioClip getClip()
    {
        return this.hoverClips[Random.Range(0, this.hoverClips.Length)];
    }
}
