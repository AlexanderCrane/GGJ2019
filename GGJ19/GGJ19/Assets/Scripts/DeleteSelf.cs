using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteSelf : MonoBehaviour {

    public bool shouldDelete = false;

	// Use this for initialization
	void Start () {

        //waitToDelete();

        if (shouldDelete)
        {
            Destroy(this.gameObject, 0.5f);
        }

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
