using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour {

    public GameObject icon;
    public GameObject player;

    private bool playerHere = false;

    public AudioSource relief;


    // Use this for initialization
    void Start () {
        icon.SetActive(false);

        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        if(playerHere && (Input.GetKeyDown("e") || Input.GetButtonDown("Fire4")))
        {
            player.GetComponent<RoboCharController>().recharge();
            //relief.Play();
            StartCoroutine(waitToPlayRecharge());
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //Debug.Log("Collision entered");
            icon.SetActive(true);
            playerHere = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            icon.SetActive(false);
            playerHere = false;
        }
    }

    IEnumerator waitToPlayRecharge()
    {
        //returning 0 will make it wait 1 frame
        yield return new WaitForSeconds(2f);
        relief.Play();

        //code goes here

    }


}
