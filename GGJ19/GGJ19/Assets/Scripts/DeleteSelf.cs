using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteSelf : MonoBehaviour {

	// Use this for initialization
	void Start () {

        //waitToDelete();

        Destroy(this.gameObject, 3);


    }

    // Update is called once per frame
    void Update () {
		
	}

    IEnumerator waitToDelete()
    {

        //returning 0 will make it wait 1 frame
        yield return(5);

        Destroy(this.gameObject);

        //code goes here

    }
}
