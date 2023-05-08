using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public static PlayerBehavior Instance;
    [SerializeField] Collider2D FistArea;
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
    //dodge
    private float dodgeCoolTime = 5;
    private float dodgeRemainTime = 5;
    private int dodgeState = 0;
    private KeyCode prevDodgeKey = KeyCode.B;
    private float lastPressedTime=0.0f;
    private bool isDodging = false;

    //attack
    RaycastHit2D rayHit;

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

    private void OnEnable()
    {
        DetectDir();
    }
    void Start()
    {
        FistArea.gameObject.SetActive(false);
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

        Vector2 originvec = new Vector2(rigid.position.x, rigid.position.y+rigid.transform.lossyScale.y);
        Vector2 dir = new Vector2(rigid.transform.localScale.x * 2, 0);
        rayHit = Physics2D.Raycast(originvec, dir, 0.75f, LayerMask.GetMask("Enemy"));
        Debug.DrawRay(originvec, dir, new Color(0, 1, 0));

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
            DetectDir();
            if (Input.GetButtonUp("Horizontal"))
            {
                anim.SetBool("isWalking", false);
                isWalking = false;
                rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
            }

            if (Input.GetButtonDown("Jump") && !isJumping)
            {
                anim.SetTrigger("DoJump");
                isJumping = true;
                anim.SetBool("isJumping", true);
                rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            }

            //Attack
            if (Input.GetKeyDown(KeyCode.R) && !FistArea.gameObject.activeSelf)
            {
                StopCoroutine("BasicAttack");
                StartCoroutine("BasicAttack");
            }

            //ZeroTwo Dodge
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (prevDodgeKey == KeyCode.D && Time.time - lastPressedTime <= 0.3f)
                    dodgeState = Mathf.Min(2, ++dodgeState);
                else
                    dodgeState = 0;

                prevDodgeKey = KeyCode.A;
                lastPressedTime = Time.time;
                Debug.Log("Dodge state: " + dodgeState.ToString());
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                if (prevDodgeKey == KeyCode.A && Time.time - lastPressedTime <= 0.3f)
                    dodgeState = Mathf.Min(2, ++dodgeState);
                else
                    dodgeState = 0;

                prevDodgeKey = KeyCode.D;
                lastPressedTime = Time.time;
                Debug.Log("Dodge state: " + dodgeState.ToString());
            }

            if (dodgeState == 2 && !isDodging)
            {
                Debug.Log("제로투 회피 시작");
                isDodging = true;
                gameObject.layer = 8;//nodamage
                anim.SetBool("isDodging", true);
            }

            if (Time.time - lastPressedTime > 0.3f)
            {
                dodgeState = 0;
                if (isDodging)
                {
                    isDodging = false;
                    gameObject.layer = 0;//nodamage
                    anim.SetBool("isDodging", false);
                }

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

        if (collision.gameObject.tag == "Enemy") //나중에는 적 종류따라 데미지량을 달리 할 예정
        {
            Debug.Log("적과 닿았습니다");
            rigid.AddForce(new Vector2(-rigid.transform.localScale.x * 8, 1), ForceMode2D.Impulse);
            anim.SetTrigger("Damaged");
            currentHP -= 5;
            HPmanager.Instance.showHpBar(currentHP);
            gameObject.layer = 8;//noDamage layer
            setColor(new Color(1, 0.5f, 0.5f));
            canmove = false;
            Invoke("offDamaged", 1);
        }
    }

    public void offDamaged()
    {
        canmove = true;
        gameObject.layer = 0;//default
        setColor(Color.white);
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

    public void DetectDir()
    {
        if (isDodging)
            return;

            

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKey(KeyCode.A))
            transform.localScale = new Vector3(-0.5f, 0.5f, 1);
            
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKey(KeyCode.D))
            transform.localScale = new Vector3(0.5f, 0.5f, 1);                   
    }



    IEnumerator BasicAttack()
    {
        FistArea.gameObject.SetActive(true);
        Debug.Log("basic attack");
        //기본 공격
        anim.SetTrigger("BasicAttack");
        yield return new WaitForSeconds(0.5f);
        FistArea.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.1f);
    }

    public void setColor(Color color)
    {
        SpriteRenderer[] sprites =gameObject.GetComponentsInChildren<SpriteRenderer>();
        foreach(SpriteRenderer sprite in sprites)
        {
            sprite.color=color;
        }
    }
}
