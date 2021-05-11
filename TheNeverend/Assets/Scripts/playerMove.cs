using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerMove : MonoBehaviour
{
    public float runSpeed = 40f;
    public float sprintSpeed = 20f;
    float horizontalMove = 0f;
    bool jump = false;
    public Transform player;
    public Transform respawnPoint;
    private bool ladder = false;
    public int maxHealth = 100;
    public int currentHealth = 100;
    public Image healthBar;
    public GameObject key;
    public GameObject death;
    public static bool isDead = false;
    public static bool hasKey = false;
    public Animator animatior;

    public CharacterController2D controller;
    public Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        healthBar.fillAmount = (float)currentHealth / (float)maxHealth;
        death.SetActive(false);
        hasKey = false;
        key.SetActive(true);

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "key")
        {
            hasKey = true;
            Debug.Log("Got Key");
        }
    }


    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Ladder" && Input.GetKey("w"))
        {
            ladder = true;

        }

        if (col.gameObject.tag == "Ladder" && Input.GetKey("s"))
        {
            ladder = false;

        }

        

        if (col.gameObject.tag == "lava")
        {
            transform.position = new Vector3(respawnPoint.position.x, respawnPoint.position.y, transform.position.z);
            die();
            
        }

        if (col.gameObject.tag == "key")
        {
            hasKey = true;
        }



    }

        
        
            
        


    void OnTriggerExit2D(Collider2D col)
    {
        ladder = false;
    }
    // Update is called once per frame
    void Update()
    {
        animatior.SetFloat("Speed", Mathf.Abs(horizontalMove));
        loseHealth();
        healthBar.fillAmount = (float)currentHealth / (float)maxHealth;
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animatior.SetBool("isJumping", true);
        }

        if (Input.GetKeyDown("u"))
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * sprintSpeed;
        }

        if (player.position.y < -60)
        {
            transform.position = new Vector3(respawnPoint.position.x, respawnPoint.position.y, transform.position.z);
        }

        if(currentHealth<=0)
        {
            die();
        }

        if(hasKey == true)
        {
            key.SetActive(true);
        }

        respawn();
    }

    public void OnLanding()
    {
        animatior.SetBool("isJumping", false);
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
        if (ladder == true)
        {
            horizontalMove = 0f;
            rb.velocity = new Vector3(0, 10, 0);
        }
        if (Input.GetKeyDown("u"))
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * sprintSpeed;
        }
    }

    private void loseHealth()
    {
        if (Input.GetKeyDown("k"))
        {
            currentHealth = currentHealth - 20;
            healthBar.fillAmount = (float)currentHealth / (float)maxHealth;
        }
    }

    void die()
    {
        currentHealth = 0;
        healthBar.fillAmount = (float)currentHealth / (float)maxHealth;
        isDead = true;
        Time.timeScale = 0;
        death.SetActive(true);
    }


    void respawn()
    {
        if(isDead == true && Input.GetKeyDown("space"))
        {
            Time.timeScale = 1;
            isDead = false;
            currentHealth = 100;
            healthBar.fillAmount = (float)currentHealth / (float)maxHealth;
            transform.position = new Vector3(respawnPoint.position.x, respawnPoint.position.y, transform.position.z);
            death.SetActive(false);

        }
    }
}
