using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour {


    public GameObject icon;
    public GameObject player;
    Rigidbody2D playerRB;
    
    public GameObject topCollider;
    public GameObject bottomCollider;

    //public float bottomPoint = 10f;
    //public float topPoint = 10f;

    public bool goingUp;
    bool activated;
    //float playerGravity;

    private bool playerHere = false;

    public AudioSource elevator;


    // Use this for initialization
    void Start()
    {
        icon.SetActive(false);

        player = GameObject.FindGameObjectWithTag("Player");
        playerRB = player.GetComponent<Rigidbody2D>();
        //playerGravity = playerRB.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHere && (Input.GetKeyDown("e") || Input.GetButtonDown("Fire4")))
        {
            //relief.Play();
            player.transform.parent = transform;

            activated = true;
            /*if(goingUp)
            {
                goingUp = false;
            }
            else
            {
                goingUp = true;
            }*/
            Debug.Log("Button pressed");

            //StartCoroutine(waitToPlaySound());
        }

        if(activated && goingUp)
        {
            //transform.Translate(Vector2.up * 10 * Time.deltaTime);

            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, topCollider.transform.position.y), 20 * Time.deltaTime);
            //Debug.Log("Going Up");
            //transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, topPoint, transform.position.z), 1 * Time.deltaTime);
            if(transform.position.y >= topCollider.transform.position.y)
            {
                Debug.Log("Collided with top");
                goingUp = false;
                activated = false;

                player.transform.parent = null;

            }
        }
        else if(activated && !goingUp)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, bottomCollider.transform.position.y), 20 * Time.deltaTime);

            //Debug.Log("Going Down");
            //playerRB.gravityScale = 0;

            if (transform.position.y <= bottomCollider.transform.position.y)
            {
                Debug.Log("Collided with bottom");
                goingUp = true;
                activated = false;

                player.transform.parent = null;

            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //collision.transform.parent = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Collision entered");
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

    IEnumerator waitToPlaySound()
    {
        //returning 0 will make it wait 1 frame
        yield return new WaitForSeconds(2f);
        //elevator.Play();

        //code goes here

    }
}
