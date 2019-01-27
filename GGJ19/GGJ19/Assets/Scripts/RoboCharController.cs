using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class RoboCharController : MonoBehaviour
{

    public bool facingRight = true;
    public bool jump = true;
    public bool grounded = true;
    public float charge = 100;
    //public GameObject textObj;
    //public Text textComp;
    //public GameObject GameOverUI;
    //public GameObject YouWinUI;
    public GameObject bullet;
    public int lives = 3;
    public float moveForce = 300f;
    public float maxSpeed = 3f;
    public float jumpForce = 800;
    public bool canMove = true;
    public GameObject livesText;
    public TextMeshProUGUI lv;
    public GameObject pauseMenu;
    public GameObject gameOver;
    public GameObject gameWin;
    bool paused = false;
    public GameObject rechargeStation;

    public AudioSource damageSound;
    public AudioSource jumpSound;
    public AudioSource dieSound;
    public AudioSource landSound;
    public AudioSource shootSound;

    Animator anim;
    GameObject shotBullet;
    bool bulletShot = false;
    bool isBounced;
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

    private void Start()
    {
        bullet = GameObject.Find("Projectile");

        lv = livesText.GetComponent<TextMeshProUGUI>();

        pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
        pauseMenu.SetActive(false);

        gameOver = GameObject.FindGameObjectWithTag("GameOver");
        gameOver.SetActive(false);

        gameWin = GameObject.FindGameObjectWithTag("GameWin");
        gameWin.SetActive(false);

        rechargeStation = GameObject.FindGameObjectWithTag("Recharge");
    }

    // Update is called once per frame
    void Update()
    {
        //grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        if(charge <= 0f)
        {
            lives -= 1;
            charge = 100;

            dieSound.PlayDelayed(0.5f);

            if (lives == 3)
            {
                lv.text = "Lives: 3";
                transform.position = rechargeStation.transform.position;
            }
            else if(lives == 2)
            {
                lv.text = "Lives: 2";
                transform.position = rechargeStation.transform.position;

            }
            else if(lives == 1)
            {
                lv.text = "Lives: 1";
                transform.position = rechargeStation.transform.position;

            }
            else if(lives == 0)
            {
                lv.text = "Lives: 0";
                transform.position = rechargeStation.transform.position;

            }
            else
            {
                gameOver.SetActive(true);
                //game over

            }
        }

        if ((Input.GetKeyDown("up") || Input.GetKeyDown("w") || Input.GetButtonDown("Fire1")) && grounded && !jump && canMove && !paused)
        {
            jump = true;
            jumpSound.Play();
        }

        if(Input.GetButtonDown("Cancel") && !paused)
        {
            paused = true;
            pauseMenu.SetActive(true);
        }
        else if (Input.GetButtonDown("Cancel") && paused)
        {
            paused = false;
            pauseMenu.SetActive(false);
        }

        if (Input.GetKeyDown("space") || Input.GetButtonDown("Fire3") && canMove)
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
        float h = Input.GetAxis("Horizontal");
        //Debug.Log(h);
        //anim.SetFloat("Speed", Mathf.Abs(h));

        if (h * rb2d.velocity.x < maxSpeed && !isBounced && canMove)
        {
            rb2d.AddForce(Vector2.right * h * moveForce);
        }

        if (Mathf.Abs(rb2d.velocity.x) > maxSpeed && !isBounced && canMove)
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

        if (jump && canMove)
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
            gameWin.SetActive(true);

        }
        else if (collision.gameObject.tag == "Deadly")
        {
            damageSound.Play();


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
            landSound.Play();

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
        if (other.gameObject.tag == "Deadly")
        {

            damageSound.Play();


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

        }
        else if (other.gameObject.tag == "Recharge")
        {
            //charge = 100;
            //depleting = false;
            //anim.SetInteger("State", 1);

        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Recharge")
        {
            depleting = true;

        }
    }

    public void recharge()
    {
        charge = 100;
        anim.SetInteger("State", 1);
        canMove = false;
        StartCoroutine(waitToRecharge());

        //depleting = true;
    }

    IEnumerator waitToShoot()
    {
        Debug.Log("Waiting");
        shootSound.Play();

        //returning 0 will make it wait 1 frame
        yield return new WaitForSeconds(0.5f);


        //code goes here
        bulletShot = false;

        Debug.Log("Waiting done");

    }

    IEnumerator waitToRecharge()
    {
        Debug.Log("Waiting");
        //returning 0 will make it wait 1 frame
        yield return new WaitForSeconds(3f);


        //code goes here
        canMove = true;
        anim.SetInteger("State", 0);


        Debug.Log("Waiting done");

    }


}
