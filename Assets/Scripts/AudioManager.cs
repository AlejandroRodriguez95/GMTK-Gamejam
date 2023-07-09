using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource source;
    [SerializeField] AudioClip soundtrack;
    // Start is called before the first frame update
    void Start()
    {
        source = new AudioSource();
        source.clip = soundtrack;
        source.loop = true;
        source.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
