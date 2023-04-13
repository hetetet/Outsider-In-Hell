using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator anim;
    [SerializeField] SingleMobSpawner singleSpawner;
    int dir = 0;
    protected virtual void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        Invoke("think", 3);
    }

    protected virtual void FixedUpdate()
    {
        rigid.velocity=new Vector2(dir,rigid.velocity.y);

        Vector2 frontvec=new Vector2(rigid.position.x + dir*(float)(transform.localScale.x/2), rigid.position.y);
        Debug.DrawRay(frontvec, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit=Physics2D.Raycast(frontvec, Vector3.down, 1, LayerMask.GetMask("Platform"));
        if (rayHit.collider == null)
        {
            dir = dir * (-1);
            CancelInvoke();
            Invoke("think", 3);
        }
    }
    void Update()
    {
        
    }

    void think()
    {
        dir = Random.Range(-1, 2);
        Debug.Log("dir" + dir.ToString());
        Invoke("think", 3);
        if (dir == 0)
            anim.SetBool("isWalking", false);
        else
            anim.SetBool("isWalking", true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("플레이어와 닿았습니다");
        }
    }
}
