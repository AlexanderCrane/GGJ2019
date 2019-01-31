using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour
{
    public AudioClip[] clips;
    public AudioClip[] music;
    private int clipIndex;
    private int musicIndex;
    public AudioSource audio;
    public AudioSource musicPlayer; //must set reference in scene

    public RoboCharController playerRef;

    //private bool audioPlaying = false;
    //private bool musicPlaying = false;
    public AudioSource staticSound;

    void Start()
    {
        //audio = gameObject.GetComponent<AudioSource>();
        //musicPlayer = gameObject.GetComponent<AudioSource>();
        playerRef = gameObject.GetComponent<RoboCharController>();

    }
    void Update()
    {
        float charge = playerRef.charge;

        if (!audio.isPlaying)
        {
            clipIndex = Random.Range(0, clips.Length - 1);
            audio.clip = clips[clipIndex];
            audio.PlayDelayed(Random.Range(5f, 10f));
        }

        if(!musicPlayer.isPlaying)
        {
            musicIndex = Random.Range(0, music.Length - 1);
            musicPlayer.clip = music[musicIndex];
            musicPlayer.Play();

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
