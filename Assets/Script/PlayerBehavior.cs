using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public static PlayerBehavior Instance;
    //movement
    private float maxspeed=3;
    private float jumpPower=12;
    public static bool canmove = true;
    private bool isJumping = false;
    private bool isWalking = false;
    Rigidbody2D rigid;
    Animator anim;
    float h=0;
    //hp
    public static int maxHP=100;
    public static int currentHP = 100;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(this);
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    void Start()
    {

    }

    private void FixedUpdate()
    {
        if (canmove)
        {
            h = Input.GetAxisRaw("Horizontal");
            if (h != 0)
            {
                rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);
                if (!isWalking)
                {
                    isWalking = true;
                    anim.SetBool("isWalking", true);
                }
            }
            rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);
        }
            if (rigid.velocity.x > maxspeed)
                rigid.velocity = new Vector2(maxspeed, rigid.velocity.y);
            if (rigid.velocity.x < maxspeed * (-1))
                rigid.velocity = new Vector2(maxspeed * (-1), rigid.velocity.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (canmove)
        {
            if (Input.GetKeyDown(KeyCode.A))
                transform.localScale = new Vector3(-0.5f, 0.5f, 1);

            if (Input.GetKeyDown(KeyCode.D))
                transform.localScale = new Vector3(0.5f, 0.5f, 1);

            if (Input.GetButtonUp("Horizontal"))
            {
                anim.SetBool("isWalking", false);
                isWalking = false;
                rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
            }

            if (Input.GetButtonDown("Jump") && !isJumping)
            {
                Debug.Log("jump!");
                anim.SetTrigger("DoJump");
                isJumping = true;
                anim.SetBool("isJumping", true);
                rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Land" && collision.relativeVelocity.y > 0)
        {
            anim.SetBool("isJumping", false);
            isJumping = false;
        }

        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("적과 닿았습니다");
        }
    }

    public void EnableMove()
    {
        canmove = true;
    }

    public void DisableMove()
    {
        anim.SetBool("isWalking", false);
        anim.SetBool("isJumping", false);
        canmove = false;
    }
}
