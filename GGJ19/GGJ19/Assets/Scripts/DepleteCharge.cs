using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DepleteCharge : MonoBehaviour {

    public GameObject player;
    public RoboCharController playerScript;
    public Image chargeBar;

    private float percent = 1;

    float charge;

	// Use this for initialization
	void Start ()
    {

        //player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<RoboCharController>();
		
	}
	
	// Update is called once per frame
	void Update () {
        charge = playerScript.charge;

        percent = charge / 100;

        chargeBar.fillAmount = percent;
		
	}
}
