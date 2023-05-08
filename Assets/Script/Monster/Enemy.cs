using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator anim;
    SingleMobSpawner singleSpawner;
    SpriteRenderer spriteRenderer;
    int dir = 0;
    private int maxhp = 30;
    public int hp = 30;
    protected virtual void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        singleSpawner=gameObject.transform.parent.GetComponent<SingleMobSpawner>();
    }
    private void Start()
    {
        Invoke("think", 3);
    }

    protected virtual void FixedUpdate()
    {
        rigid.velocity=new Vector2(dir,rigid.velocity.y);

        Vector2 originvec=new Vector2(rigid.position.x + dir*(float)(Mathf.Abs(transform.localScale.x)/2), rigid.position.y);
        Debug.DrawRay(originvec, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit=Physics2D.Raycast(originvec, Vector3.down, 1, LayerMask.GetMask("Platform"));
        if (rayHit.collider == null)
        {
            dir = dir * (-1);
            if(dir!=0)
                transform.localScale = new Vector3(-dir*Mathf.Abs(gameObject.transform.localScale.x), gameObject.transform.localScale.y, 0);
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
        if(dir!=0)
            transform.localScale = new Vector3(-dir * Mathf.Abs(gameObject.transform.localScale.x), gameObject.transform.localScale.y, 0);

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
            int isLeft = (collision.gameObject.transform.position.x - gameObject.transform.position.x) > 0 ? -1 : 1; //enemy가 플레이어보다 오른쪽에 있?
            attack(isLeft);
        }

    }

    public void damaged(int damage)
    {
        StartCoroutine("coDamaged",damage);
    }

    IEnumerator coDamaged(int damage)
    {
        dir = 0;
        CancelInvoke();
        gameObject.layer = 8;//noDamage;
        hp -= damage;   
        if (hp <= 0)
        {
            Debug.Log("쥬거써...ㅠ");
            //죽는 애니메이션
            anim.SetTrigger("die");
            //애니메이션 재생시간동안 딜레이
            yield return new WaitForSeconds(0.5f);
            Debug.Log("0.5초 지남");
            hp = maxhp;
            singleSpawner.CoRevive();
            Destroy(gameObject);
        }
        anim.SetTrigger("damaged");
        Debug.Log("데미지를 입었습니다.");
        SpriteRenderer sr=gameObject.GetComponent<SpriteRenderer>();
        sr.color = new Color(255,255,255,0.5f);       
        yield return new WaitForSeconds(1);
        sr.color = Color.white;
        gameObject.layer = 7;//enemy;
        think();
    }

    public void attack(int isLeft)
    {
        transform.localScale = new Vector3(isLeft * Mathf.Abs(gameObject.transform.localScale.x), gameObject.transform.localScale.y, 0);
        anim.SetTrigger("attack");
        new WaitForSeconds(1);
        Debug.Log("플레이어와 닿았습니다");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Fist")
        {
            int dir=(collision.gameObject.transform.position.x-gameObject.transform.position.x) > 0? 1:-1; //enemy가 플레이어보다 오른쪽에 있?
            transform.localScale = new Vector3(-dir * Mathf.Abs(gameObject.transform.localScale.x), gameObject.transform.localScale.y, 0);
            rigid.AddForce(new Vector2(dir * gameObject.transform.localScale.x * 15, 3), ForceMode2D.Impulse);
            damaged(3); //3
        }
    }
}
