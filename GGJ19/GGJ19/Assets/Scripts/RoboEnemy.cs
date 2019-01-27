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
	private Vector3 pos;
	private Rigidbody2D rb;
    private SpriteRenderer mySpriteRenderer;
    private bool paused = false;
    private IEnumerator coroutine;
    public bool gunEnemy = false;
    public bool flying = false;
    public AudioSource deathSound;
    public GameObject bullet;

    GameObject shotBullet;
    bool bulletShot = false;

    private IEnumerator delay(float waitTime)
    {
        paused = true;
        yield return new WaitForSeconds(waitTime);
        rb.bodyType = RigidbodyType2D.Dynamic;
        paused = false;

        if (gunEnemy)
        {
            if (mySpriteRenderer.flipX == true && !flying)
            {
                shotBullet = Instantiate(bullet, transform.position, transform.rotation);
                shotBullet.GetComponent<DeleteSelf>().shouldDelete = true;

                shotBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(-20, 0);

                //bulletShot = true;
            }
            else if(mySpriteRenderer.flipX == false && !flying)
            {
                shotBullet = Instantiate(bullet, transform.position, transform.rotation);
                shotBullet.GetComponent<DeleteSelf>().shouldDelete = true;

                shotBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(20, 0);
            }
            else if (mySpriteRenderer.flipX == true && flying)
            {
                shotBullet = Instantiate(bullet, transform.position, transform.rotation);
                shotBullet.GetComponent<DeleteSelf>().shouldDelete = true;
                shotBullet.transform.eulerAngles = new Vector3(0, 0, -135);

                shotBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(-20, -20);

            }
            else if (mySpriteRenderer.flipX == false && flying)
            {
                shotBullet = Instantiate(bullet, transform.position, transform.rotation);
                shotBullet.GetComponent<DeleteSelf>().shouldDelete = true;
                shotBullet.transform.eulerAngles = new Vector3(0, 0, -45);

                shotBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(20, -20);

            }

        }

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

        bullet = GameObject.Find("EnemyProjectile");
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "DeadlyToEnemies")
        {
            Debug.Log("Trying to kill enemy");
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
            deathSound.Play();
        }
    }

    /*IEnumerator waitToShoot()
    {
        yield return new WaitForSeconds(3);
       

        
    }*/
}
