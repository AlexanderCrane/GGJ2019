using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour {


    public GameObject icon;
    public GameObject player;
    Rigidbody2D playerRB;

    public float bottomPoint = 10f;
    public float topPoint = 10f;

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

            StartCoroutine(waitToPlaySound());
        }

        if(activated && goingUp && transform.position.y <= topPoint)
        {
            //Vector3 newPosition = player.transform.position;
            //newPosition.z = zOffset;
            //playerRB.gravityScale = 0;
            // player.transform.Translate(new Vector2(0, 1));
            //player.transform.position = Vector2.MoveTowards(player.transform.position, new Vector2(player.transform.position.x, topPoint), 2 * Time.deltaTime);

            Debug.Log("Going Up");
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, topPoint, transform.position.z), 2 * Time.deltaTime);

        }
        else if(activated && !goingUp && transform.position.y >= bottomPoint)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, bottomPoint, transform.position.z), 2 * Time.deltaTime);
            //player.transform.Translate(new Vector2(0,-1));
            //player.transform.position = Vector2.MoveTowards(player.transform.position, new Vector2(player.transform.position.x, bottomPoint), 2 * Time.deltaTime);

            Debug.Log("Going Down");
            //playerRB.gravityScale = 0;
        }

        if(activated && goingUp && player.transform.position.y >= topPoint)
        {
            activated = false;
            Debug.Log("Deactivated");
            player.transform.parent = null;
            //Debug.Log(player.transform.parent.name);

            // playerRB.gravityScale = playerGravity;

        }

        if (activated && !goingUp && player.transform.position.y <= bottomPoint)
        {
            activated = false;
            Debug.Log("Deactivated");
            player.transform.parent = null;
            //Debug.Log(player.transform.parent.name);

            //playerRB.gravityScale = playerGravity;

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
