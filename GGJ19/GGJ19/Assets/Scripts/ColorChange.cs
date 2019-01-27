using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour {

    public Transform trans;
    private Color myColor;
    private Light lite;
    private bool activated = true;

    //public AudioSource interactSound;

    // Use this for initialization
    void Start () {
        myColor = this.GetComponentInParent<DoorSwitchColor>().baseColor;
        lite = this.GetComponent<Light>();
    }
	
	// Update is called once per frame
	void Update () {
        myColor = this.GetComponentInParent<DoorSwitchColor>().baseColor;
        lite.color = myColor;
        if (trans.GetComponent<Switch>().button == true)
        {
            activateObj();
        }
    }

    void activateObj()
    {
        if (activated == true)
        {
            lite.enabled = false;
            //interactSound.Play();
            activated = false;
        }

    }
}
