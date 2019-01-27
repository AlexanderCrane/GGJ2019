using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchReceiver : MonoBehaviour {

    public Transform trans;
    private Vector3 oldPos;
    private Vector3 oldRot;
    private Vector3 newPos;
    private Vector3 newRot;
    private bool activated = true;

	// Use this for initialization
	void Start () {
        oldPos = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(trans.GetComponent<Switch>().button == true)
        {
            activateObj();
        }
	}

    void activateObj()
    {
        newRot.x = 0;
        newRot.y = -90;
        newRot.z = 0;
        newPos.x = oldPos.x + 0.8346f;
        newPos.y = oldPos.y;
        newPos.z = oldPos.z + 0.644f;

        if (activated == true)
        {
            this.transform.Rotate(newRot);
            this.transform.position = newPos;
            activated = false;
        }
        
    }
}
