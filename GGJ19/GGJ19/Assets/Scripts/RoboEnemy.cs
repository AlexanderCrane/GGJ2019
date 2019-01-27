using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboEnemy : MonoBehaviour
{
	public float leftPath;
	public float rightPath;
	public int pauseTime = 3;
    public float moveSpeed = 300;
    private GameObject obj;
    private Vector3 speed;
    private int direction = 1;
	private Vector3 pos;
	private Rigidbody2D rb;
    private SpriteRenderer mySpriteRenderer;
    private bool paused = false;
    private IEnumerator coroutine;

    private IEnumerator delay(float waitTime)
    {
        paused = true;
        yield return new WaitForSeconds(waitTime);
        rb.bodyType = RigidbodyType2D.Dynamic;
        paused = false;
    }

	// Use this for initialization
	void Start ()
	{
        obj = GetComponent<GameObject>();
        coroutine = delay(pauseTime);
        speed.x = moveSpeed;
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(speed);
        pos = this.transform.position;
    }
	// Update is called once per frame
	void FixedUpdate ()
	{
        rb.bodyType = RigidbodyType2D.Static;
        if (paused)
        {
            return;
        }
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.AddForce(speed);
        pos = this.transform.position;
		if(pos.x > rightPath)
		{
            this.transform.position = new Vector3(rightPath, pos.y, pos.z);
            StartCoroutine(delay(pauseTime));
            speed.x = -moveSpeed;
            rb.AddForce(speed);
            mySpriteRenderer.flipX = true;
        }
		if(pos.x < leftPath)
		{
            this.transform.position = new Vector3(leftPath, pos.y, pos.z);
            StartCoroutine(delay(pauseTime));
            speed.x = moveSpeed;
            rb.AddForce(speed);
            mySpriteRenderer.flipX = false;
        }
	}
}
