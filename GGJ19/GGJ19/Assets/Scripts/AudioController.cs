using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour
{
    public AudioClip[] clips;
    private int clipIndex;
    private AudioSource audio;
    private bool audioPlaying = false;
    public AudioSource staticSound;

    void Start()
    {
        audio = gameObject.GetComponent<AudioSource>();
    }
    void Update()
    {
        if (!audio.isPlaying)
        {
            clipIndex = Random.Range(0, clips.Length - 1);
            audio.clip = clips[clipIndex];
            audio.PlayDelayed(Random.Range(5f, 10f));
            Debug.Log("Nothing playing, we set new audio to " + audio.clip.name);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Obstacle" || other.gameObject.tag == "Enemy")
        {

        }
        else if (other.gameObject.tag == "Recharge")
        {
            staticSound.volume = 0f;
            audio.volume = 0f;

        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Recharge")
        {
            staticSound.volume = 0.07f;
            audio.volume = 1f;

        }
    }

}
