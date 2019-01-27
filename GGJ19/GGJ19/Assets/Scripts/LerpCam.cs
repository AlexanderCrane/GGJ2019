using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpCam : MonoBehaviour
{


    public float FollowSpeed = 3f;
    public GameObject player;
    public float verticalOffset = 0f;
    public float horizontalOffset = 0f;
    //public float zOffset = -1f;


    Transform Target;

    void Start()
    {
        Target = player.transform;
    }

    private void LateUpdate()
    {
        if (Target != null)
        {
            Vector3 newPosition = Target.position;
            //newPosition.z = zOffset;
            transform.position = Vector3.Lerp(transform.position, newPosition + new Vector3(0, 1, -15), FollowSpeed * Time.deltaTime);
        }
    }
}
