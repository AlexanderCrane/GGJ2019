using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoboCharController : MonoBehaviour
{

    public bool facingRight = true;
    public bool jump = true;
    public bool grounded = true;
    public float charge = 100;
    //public Transform groundCheck;
    //public GameObject textObj;
    //public Text textComp;
    //public GameObject GameOverUI;
    //public GameObject YouWinUI;
    public GameObject bullet;

    Animator anim;
    GameObject shotBullet;
    bool bulletShot = false;
    bool isBounced;
    float moveForce = 300f;
    float maxSpeed = 3f;
    float jumpForce = 800;
    //private Animator anim;
    private Rigidbody2D rb2d;
    private bool depleting = true;


    // Use this for initialization
    void Awake()
    {
        //anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();

        //textObj = GameObject.FindWithTag("ScoreText");
        //textComp = textObj.GetComponent<Text>();

        //GameOverUI = GameObject.FindWithTag("GameOverUI");
        //GameOverUI.SetActive(false);

        //YouWinUI = GameObject.FindWithTag("YouWinUI");
        //YouWinUI.SetActive(false);

        anim = GetComponent<Animator>();


    }

    // Update is called once per frame
    void Update()
    {
        //grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        if ((Input.GetKeyDown("up") || Input.GetKeyDown("w") || Input.GetButtonDown("Fire1")) && grounded && !jump)
        {
            jump = true;
        }

        if(Input.GetKeyDown("space") || Input.GetButtonDown("Fire3"))
        {
            //shoot projectile


            if (facingRight && !bulletShot)
            {
                shotBullet = Instantiate(bullet, transform.position, transform.rotation);
                shotBullet.GetComponent<DeleteSelf>().shouldDelete = true;

                shotBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(20, 0);

                bulletShot = true;
                StartCoroutine(waitToShoot());
                charge -= 10;
            }
            else if(!bulletShot)
            {
                shotBullet = Instantiate(bullet, transform.position, transform.rotation);
                shotBullet.GetComponent<DeleteSelf>().shouldDelete = true;

                shotBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(-20, 0);

                bulletShot = true;
                StartCoroutine(waitToShoot());
                charge -= 10;

            }
        }

        if (depleting && charge >= 0.0f)
        {
            charge -= 0.01f;

            //Debug.Log(charge);
        }

    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal"); //what is this?
        //Debug.Log(h);
        //anim.SetFloat("Speed", Mathf.Abs(h));

        if (h * rb2d.velocity.x < maxSpeed && !isBounced)
        {
            rb2d.AddForce(Vector2.right * h * moveForce);
        }

        if (Mathf.Abs(rb2d.velocity.x) > maxSpeed && !isBounced)
        {
            rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);
        }

        if (h > 0 && !facingRight)
        {
            Flip();
        }
        else if (h < 0 && facingRight)
        {
            Flip();
        }

        if (jump)
        {
            //anim.SetTrigger("Jump");
            rb2d.AddForce(new Vector2(0f, jumpForce));
            grounded = false;

            jump = false;
            //anim.SetInteger("State", 2);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "EndGoal")
        {
            Debug.Log("End goal reached!");

        }
        else if (collision.gameObject.tag == "Deadly")
        {

            if (facingRight)
            {
                rb2d.velocity = new Vector2(-10, 7);
                isBounced = true;
                charge -= 10f;

            }
            else
            {
                rb2d.velocity = new Vector2(10, 7);
                isBounced = true;
                charge -= 10f;

            }
            //rb2d.AddForce(new Vector2(0f, jumpForce));
        }
        else if (collision.gameObject.tag == "Ground" && grounded == false)
        {
            //dontMove = true;
            //anim.SetInteger("State", 0);


            //anim.SetInteger("State", 0);


            grounded = true;
            isBounced = false;
            Debug.Log("Grounded");
        }
        else if (collision.gameObject.tag == "MovingPlatform")
        {
            Debug.Log("Collided with moving platform");
            //anim.SetInteger("State", 2);
            grounded = true;
            //tempTrans = transform.parent;
            transform.parent = collision.transform;
            //dontMove = true;
            //onMovingPlatform = true;

        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = false;
            //Debug.Log("No longer grounded");
        }
        else if (collision.gameObject.tag == "MovingPlatform")
        {
            Debug.Log("Collision ended with moving platform");

            grounded = false;

            //transform.parent = null;
            //dontMove = false;
            //onMovingPlatform = false;
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
        }
        else if(other.gameObject.tag == "Recharge")
        {
            charge = 100;
            depleting = false;
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Recharge")
        {
            depleting = true;
        }
    }

    IEnumerator waitToShoot()
    {
        Debug.Log("Waiting");
        //returning 0 will make it wait 1 frame
        yield return new WaitForSeconds(0.5f);


        //code goes here
        bulletShot = false;

        Debug.Log("Waiting done");

    }

}
