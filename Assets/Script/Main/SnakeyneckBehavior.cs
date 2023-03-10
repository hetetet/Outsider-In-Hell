using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeyneckBehavior : MonoBehaviour
{
    GameObject snek;
    Rigidbody2D rigid_snek;
    Animator anim_snek;
    float maxspeed = 3;
    public static SnakeyneckBehavior Instance;

    [SerializeField] AudioClip bump;
    private void Awake()
    {
        Instance = this;
        snek = GameObject.Find("SnakeyNeck");
        rigid_snek = snek.GetComponent<Rigidbody2D>();
        anim_snek = snek.GetComponent<Animator>();
    }
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        if (PlayerBehaviorOnStart.isChasing)
        {
            rigid_snek.AddForce(Vector2.right, ForceMode2D.Impulse);
            if (rigid_snek.velocity.x > maxspeed)
            {
                rigid_snek.velocity = new Vector2(maxspeed, 0);
            }
        }
    }

    public void startChasing()
    {
        anim_snek.SetBool("isWalking", true);
    }

    public void doBump()
    {
        //Destroy(rigid_snek);
        rigid_snek.velocity = Vector2.zero;
        anim_snek.SetTrigger("Bump");
        SoundManager.Instance.playEffectSound(bump);
    }
}
