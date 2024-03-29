using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBehavior : MonoBehaviour
{
    public static PlayerBehavior Instance;
    [SerializeField] Collider2D FistArea;
    public Transform MaskArea;
    public Transform ToolWeaponArea;
    public Collider2D ToolWeaponCollider;
    public float SIZE=0.4f;
    //movement
    private float maxspeed=3;
    private float jumpPower=12;
    public float attackPower = 3;
    public static bool canmove = true;
    private bool isJumping = false;
    private bool isWalking = false;
    private bool isDead=false;

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
    private bool onLadder = false;
    private bool isClimbing = false;

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
        isDead = false;
        currentHP = maxHP;
        HPmanager.Instance.showHpBar(maxHP);
        FistArea.gameObject.SetActive(false);
        ToolWeaponCollider = ToolWeaponArea.GetComponent<Collider2D>();
        ToolWeaponCollider.enabled = false;
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

        //Climbing
        if (isClimbing)
        {            
            rigid.velocity = new Vector2(rigid.velocity.x, Input.GetAxis("Vertical"));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (canmove)
        {
            DetectDir();

            //Walk
            if (Input.GetButtonUp("Horizontal"))
            {
                anim.SetBool("isWalking", false);
                isWalking = false;
                isClimbing = false;
                isClimbing = false;
                rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
            }

            //Jump
            if (Input.GetButtonDown("Jump") && !isJumping && !onLadder)
            {
                anim.SetTrigger("DoJump");
                isJumping = true;
                anim.SetBool("isJumping", true);
                rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            }

            //Climb
            if (Input.GetButtonDown("Vertical") && onLadder)
            {
                Debug.Log("isClimbing true");
                isClimbing = true;                
                anim.SetBool("isClimbing",true);
            }              
            else if (Input.GetButtonUp("Vertical") && onLadder)
            {
                Debug.Log("isClimbing false");
                isClimbing = false;
                anim.SetBool("isClimbing", false);
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
            }

            if (dodgeState == 2 && !isDodging)
            {
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
                    gameObject.layer = 0;//default
                    anim.SetBool("isDodging", false);
                }
            }
        }
        if(currentHP<=0 && !isDead)
            GameOver();

        if (Icon_Backpack.Weapon!=null && Icon_Backpack.Weapon.key == "drill") //드릴을 들고 가만히 서 있을 때
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
                StartExcavate("UpperDrill");
            else if (Input.GetKeyUp(KeyCode.UpArrow))
                StopExcavate("UpperDrill");
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                StartExcavate("FrontDrill");
                transform.localScale = new Vector3(SIZE, SIZE, 1);
            }
            else if (Input.GetKeyUp(KeyCode.RightArrow))
                StopExcavate("FrontDrill");
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                StartExcavate("FrontDrill");
                transform.localScale = new Vector3(-SIZE, SIZE, 1);
            }
            else if (Input.GetKeyUp(KeyCode.LeftArrow))
                StopExcavate("FrontDrill");
            if (Input.GetKeyDown(KeyCode.DownArrow))
                StartExcavate("UnderDrill");
            else if (Input.GetKeyUp(KeyCode.DownArrow))
                StopExcavate("UnderDrill");
        }
    }

    public void StartExcavate(string TriggerName)
    {
        anim.SetBool(TriggerName, true);
        ToolWeaponCollider.enabled = true;
    }

    public void StopExcavate(string TriggerName)
    {
        anim.SetBool(TriggerName, false);
        ToolWeaponCollider.enabled = false;
    }

    public void OnDamaged(int damage)
    {
        Debug.Log("적과 닿았습니다, -gameObject.transform.localScale.x: "+ (-gameObject.transform.localScale.x).ToString());
        rigid.AddForce(new Vector2(-gameObject.transform.localScale.x * damage * 6, 3), ForceMode2D.Impulse);
        anim.SetTrigger("Damaged");
        currentHP -= damage;
        if(currentHP < 0)
            currentHP = 0;
        HPmanager.Instance.showHpBar(currentHP);
        gameObject.layer = 8;//noDamage layer
        setColor(new Color(1, 0.5f, 0.5f));
        canmove = false;
        Invoke("OffDamaged", 1);
    }

    public void OffDamaged()
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

    public void InitState()
    {
        anim.SetBool("isWalking", false);
        anim.SetBool("isJumping", false);
        canmove = true;
    }

    public void DetectDir()
    {
        if (isDodging)
            return;
            
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKey(KeyCode.A) && !(Icon_Backpack.Weapon != null && Input.GetKey(KeyCode.RightArrow)))//무기를 들고 있으면서 무기를 반대 방향으로 사용하는 경우는 제외
            transform.localScale = new Vector3(-SIZE, SIZE, 1);
            
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKey(KeyCode.D) && !(Icon_Backpack.Weapon != null && Input.GetKey(KeyCode.LeftArrow)))
            transform.localScale = new Vector3(SIZE, SIZE, 1);                   
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Land" && collision.relativeVelocity.y > 0)
        {
            anim.SetBool("isJumping", false);
            isJumping = false;
        }

        if (collision.gameObject.tag == "Enemy") //나중에는 적 종류따라 데미지량을 달리 할 예정
        {
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ladder")
        {
            Debug.Log("onladder true");
            //gameObject.layer = 9;//ladder layer
            onLadder = true;
            rigid.drag = 20f;
            rigid.gravityScale = 0f;
            anim.SetBool("isJumping", false);
            isJumping = false;
        }
        else if (collision.gameObject.tag == "Lava") //나중에는 적 종류따라 데미지량을 달리 할 예정
        {
            Debug.Log("용암에 빠졌습니다");
            currentHP = 0;
            HPmanager.Instance.showHpBar(0);
            GameOver();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ladder")
        {
            Debug.Log("onladder false");
            //gameObject.layer = 0;//default layer
            onLadder = false;
            isClimbing = false;
            anim.SetBool("isClimbing", false);
            rigid.drag = 2f;
            rigid.gravityScale = 2f;
        }
    }
    public void GameOver()
    {
        StartCoroutine("CoGameOver");
    }

    IEnumerator CoGameOver()
    {
        isDead = true;
        canmove = false;
        anim.SetTrigger("Die");
        UIEffect.Instance.enableCanvas(999);
        UIEffect.Instance.setColor(0, 0, 0, 0);
        UIEffect.Instance.Fade(1, 1);

        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Main_map01");//SceneManager.GetActiveScene().name
        //정보 불러오는 -3000드
        GameManager.Instance.Revive();
        Destroy(gameObject);  
    }

    public bool getDodgeState()
    {
        return isDodging;
    }
}
