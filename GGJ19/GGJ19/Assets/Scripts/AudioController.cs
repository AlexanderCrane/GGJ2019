using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour
{
    public AudioClip[] clips;
    private int clipIndex;
    private AudioSource audio;
    private bool audioPlaying = false;
    public AudioSource staticSound;

    public AudioSource song1;
    public AudioSource song2;

    void Start()
    {
        audio = gameObject.GetComponent<AudioSource>();
        StartCoroutine(waitToLoop());
    }
    void Update()
    {
        if (!audio.isPlaying)
        {
            clipIndex = Random.Range(0, clips.Length - 1);
            audio.clip = clips[clipIndex];
            audio.PlayDelayed(Random.Range(5f, 10f));
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

    private void musicPlayer()
    {
        //while(true)
        //{
            waitToLoop();
        //}

    }

    IEnumerator waitToLoop()
    {
        yield return new WaitForSeconds(17.528f);
        int song = Random.Range(0, 2);

        if(song == 0)
        {
            song1.Play();
            Debug.Log("Playing song 2");

        }
        else
        {
            song2.Play();
            Debug.Log("Playing song 2");

        }
        waitToLoop();
    }

}
