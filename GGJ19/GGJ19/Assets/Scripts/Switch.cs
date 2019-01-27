using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour {

    private GameObject player;
    private Vector3 playerPos;
    private Vector3 switchPos;

    public bool button;

	// Use this for initialization
	void Start () {
        player = GameObject.FindWithTag("Player");
        playerPos = player.transform.position;
        switchPos = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        playerPos = player.transform.position;
        if(playerPos.x <= switchPos.x + 0.2 || playerPos.x <= switchPos.x - 0.2)
        {
            activate();
        }
    }

    public void activate()
    {
        button = true;
    }
}
