using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBehaviorOnStart : MonoBehaviour
{
    public float maxspeed = 3;
    public static bool isRanaway = false;
    public bool isChasing = false;

    public bool isWalking = false;
    [SerializeField] Mission ranaway;

    [SerializeField] GameObject snek;
    Rigidbody2D rigid_snek;
    Animator anim_snek;


    public static PlayerBehaviorOnStart Instance;

    [SerializeField] AudioClip bump;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        PlayerBehavior.canmove = false;
        rigid_snek =snek.GetComponent<Rigidbody2D>();
        anim_snek= snek.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");

        if (isChasing)
        {           
            rigid_snek.AddForce(Vector2.right, ForceMode2D.Impulse);
            if (rigid_snek.velocity.x > maxspeed)
            {
                Debug.Log("no faster than maxspeed");
                rigid_snek.velocity = new Vector2(maxspeed, 0);
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "goToMap01")
        {
            Destroy(this);
            Icon_Mission.Instance.deleteMission(ranaway);
            SceneManager.LoadScene("Main_map01");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "SnakeyNeck" && isChasing)
        {
            StarterHandler.startAgain = true;
            StartCoroutine(GameOver());
        }
    }

    IEnumerator GameOver()
    {
        isRanaway = false;
        isChasing = false;
        rigid_snek.velocity = Vector2.zero;
        UIEffect.Instance.enableCanvas(999);
        UIEffect.Instance.setColor(0, 0, 0, 0);
        UIEffect.Instance.Fade(1, 1);
        
        yield return new WaitForSeconds(1.5f);
        Icon_Mission.Instance.deleteMission(ranaway);
        SceneManager.LoadScene("Main_starter");
    }

    public void startChasing()
    {
        isRanaway = true;
        isChasing = true;
        PlayerBehavior.canmove = true;
        Debug.Log("snakeyneck is chasing");
        anim_snek.SetBool("isWalking", true);
    }

    public void doBump()
    {
        isChasing = false;
        Destroy(rigid_snek);
        SoundManager.Instance.playEffectSound(bump);
        anim_snek.SetTrigger("Bump");
    }
}
