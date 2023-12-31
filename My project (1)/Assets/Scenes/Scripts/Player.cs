using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int health = 3;
    public float speed;
    public float jumpForce;

    public GameObject bow;
    public Transform firepoint;

    private Rigidbody2D rig;
    private Animator anim;
    
    private bool isJumping;
    private bool doubleJump;
    private bool isFire;

    public AudioSource tiro;

    private float movement;
    
    public Vector3 posInicial;

    public AudioSource AudioJump;
    public AudioSource AudioWalk;
    public AudioSource AudioAttack;
    

    // Start is called before the first frame update
    void Start()
    {
        posInicial = new Vector3(-9, -3, 0);
        transform.position = posInicial;
    
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
        GameController.instance.UpdateLives(health);
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
        BowFire();
    }

    private void FixedUpdate()
    {
        Mover();
    }

    void Mover()
    {
        float movement = Input.GetAxis("Horizontal");
        rig.velocity = new Vector2(movement * speed, rig.velocity.y);

        
        if (movement > 0)
        {
            if (!isJumping)
            {
                anim.SetInteger("transition", 1);
                AudioWalk.Play();
            }
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        if (movement < 0)
        {
            if (!isJumping)
            {
                anim.SetInteger("transition", 1);
                AudioWalk.Play();
            }
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        if (movement == 0 && !isJumping && !isFire)
        {
            anim.SetInteger("transition", 0);
        }
    }

    public void IncreaseLife(int value)
    {
        health += value;
        GameController.instance.UpdateLives(health);
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (!isJumping)
            {
                anim.SetInteger("transition", 2);
                rig.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                doubleJump = true;
                isJumping = true;
                AudioJump.Play();
            }
            else
            {
                if (doubleJump)
                {
                    anim.SetInteger("transition", 2);
                    rig.AddForce(new Vector2(0, jumpForce * 2), ForceMode2D.Impulse);
                    doubleJump = false;
                }
            }
        }
        

    }

    void BowFire()
    {
        StartCoroutine("Fire");
    }

    IEnumerator Fire()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            isFire = true;
            anim.SetInteger("transition", 3);
            AudioAttack.Play();
           /* GameObject Bow = Instantiate(bow, firepoint.position, firepoint.rotation);
            tiro.Play();

            if (transform.rotation.y == 0)
            {
                Bow.GetComponent<bow>().isRight = true;
            }

            if (transform.rotation.y == 180)
            {
                Bow.GetComponent<bow>().isRight = false;
            }
            */
            yield return new WaitForSeconds(1f);
            anim.SetInteger("transition", 0);
            isFire = false;

        }
    }

    public void Damage(int dmg)
    {
        health -= dmg;
        GameController.instance.UpdateLives(health);
        anim.SetTrigger("hit");
        
        if (transform.rotation.y == 0)
        {
            transform.position += new Vector3(-0.5f, 0, 0);
        }

        if (transform.rotation.y == 180)
        {
            transform.position += new Vector3(0.5f, 0, 0);
        }
        
        if (health <= 0)
        {
            GameController.instance.GameOver();
        }
    }
    
     
    

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.layer == 8)
        {
            isJumping = false;
        }
        
        if (coll.gameObject.layer == 9)
        {
            GameController.instance.GameOver();
        }
    }
}
