using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Yarn.Unity;

public class Cheolsu : NpcBehavior
{
    GameObject Player;
    Rigidbody2D rigid;
    [SerializeField] Mission mainmission;
    [SerializeField] TextMeshProUGUI timerTmp;
    [SerializeField] InMemoryVariableStorage variableStorage;
    Vector3 dir;

    bool timer0n=false;
    float timer = 0;
    float maxspeed = 3;
    bool isChasing = false;
    int hadconvo = 0;

    void Start()
    {
        //hadconvo = PlayerPrefs.GetInt("cheolsu_hadconvo", 0);
        Anim = GetComponent<Animator>();
        Sr = GetComponent<SpriteRenderer>();
        rigid= GetComponent<Rigidbody2D>();
        Player = GameObject.Find("Soyeon");
        Dr.onDialogueComplete.AddListener(() =>
        {
            MissionAlarm.Instance.show_mission(mainmission);
        });
    }

    private void FixedUpdate()
    {
        rigid.velocity = new Vector2(dir.x * 3, 0);
        if (rigid.velocity.x > maxspeed)
            rigid.velocity = new Vector2(maxspeed, rigid.velocity.y);
        if (rigid.velocity.x < maxspeed * (-1))
            rigid.velocity = new Vector2(maxspeed * (-1), rigid.velocity.y);
    }

    protected override void Update()
    {
        if(Vector3.Distance(Player.transform.position, transform.position) < 5f && hadconvo==0)
        {
            if (!timer0n)
                StartCoroutine("coNoticePlayer");
            timer0n = true;
        }

        if (timer0n) //timer는 hadconvo가 0일 경우에만 켜진다
        {
            //소연이에게 다가간다
            timer += Time.deltaTime;
            timerTmp.text = "timer: "+timer.ToString()+", distance: "+ Vector3.Distance(Player.transform.position, transform.position).ToString();
            
            if (isChasing)
            {
                dir=(Player.transform.position - transform.position).normalized;
                dir.y = 0;
                if(Player.transform.position.x - transform.position.x<0)
                    Sr.flipX = false;
                else
                    Sr.flipX=true;
            }
            

            if(Mathf.Abs(Vector3.Distance(Player.transform.position, transform.position)) <= 2f && isChasing) //소연이를 정상적으로 불러세운 경우(거리가 3 이하)
            {
                Anim.SetBool("IsWalking", false);
                hadconvo = 1;
                isChasing = false;
                rigid.velocity = Vector2.zero;
                rigid.drag = 1972;
                timer0n = false;
                timer = 0;
                timerTmp.text = timer.ToString()+", timer ended";
                PlayerBehavior.Instance.DisableMove();
                Debug.Log("소연이를 정상적으로 불러세움");
                variableStorage.SetValue("$didranaway", false);
                Dr.StartDialogue("Cheolsu");
            }
            else if (timer >= 5)//소연이가 계속 도망가다가 5초 경과 후
            {
                Anim.SetBool("IsWalking", false);
                hadconvo = 1;
                isChasing = false;
                rigid.velocity = Vector2.zero;
                rigid.drag = 1972;
                timer0n =false;
                timer=0;
                timerTmp.text = timer.ToString() + ", timer ended after 5sec";
                PlayerBehavior.Instance.DisableMove();
                Debug.Log("소연이를 5초 후 겨우 불러세움");
                variableStorage.SetValue("$didranaway", true);
                Dr.StartDialogue("Cheolsu");
            }
        }   
    }
    IEnumerator coNoticePlayer()
    {
        Anim.SetTrigger("Notice");
        yield return new WaitForSeconds(1);
        Anim.SetBool("IsWalking", true);
        isChasing = true;
    }
}
