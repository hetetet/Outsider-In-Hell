using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using Yarn.Unity;
public class StarterHandler : MonoBehaviour
{
    public static int startAgain;
    [SerializeField] TextMeshProUGUI howMuchHold;
    [SerializeField] Image GameLogo;
    [SerializeField] DialogueRunner dialogueRunner;
    
    [SerializeField] public SerializableDictionary<string, Sprite[]> CharDict;

    [SerializeField] Mission ranaway;

    [SerializeField] GameObject TopUI;
    [SerializeField] GameObject HPbar;

    [SerializeField] AudioClip StartBGM;
    void Start()
    {
        startAgain = PlayerPrefs.GetInt("start_again", 0);
        if (GameManager.LanguageCode == "ko")
        {
            howMuchHold.text ="총 "+(PlayerBehavior.maxHP-100).ToString()+"초 동안 의식을 붙들었습니다.";
        }else if (GameManager.LanguageCode == "en")
        {
            howMuchHold.text = "You've been conscious for " + (PlayerBehavior.maxHP - 100).ToString() + " seconds.";
        }
        else
        {
            howMuchHold.text = "You've been conscious for " + (PlayerBehavior.maxHP - 100).ToString() + " seconds.(*LangCode not set correctly)";
        }

        TopUI.SetActive(false);
        HPbar.SetActive(false);
        PortalBehavior.prevScenename = "Starter";
        
        StartCoroutine(StartMainGame());
    }

    void Update()
    {

    }

    IEnumerator StartMainGame()
    {
        if (startAgain==0)
        {
            UIEffect.Instance.enableCanvas(998);
            UIEffect.Instance.setColor(0, 0, 0, 1);
            howMuchHold.color = new Color(1, 1, 1, 0);
            GameLogo.color = new Color(1, 1, 1, 0);

            Sequence startMainGame = DOTween.Sequence();
            startMainGame.Insert(1, howMuchHold.DOColor(Color.white, 1f));
            startMainGame.Insert(4.5f, howMuchHold.DOColor(new Color(1, 1, 1, 0), 1f).OnComplete(() =>
            {
                StartCoroutine(SoundManager.Instance.fadeBgmCorut(StartBGM, 2));
                
            }));
            startMainGame.Insert(6, GameLogo.DOColor(Color.white, 0.5f));
            startMainGame.Insert(6, GameLogo.transform.DOScale(new Vector2(1.1f, 1.1f), 3f));
            startMainGame.Insert(8, GameLogo.DOColor(new Color(1, 1, 1, 0), 1f));

            yield return new WaitForSeconds(10f);

            Destroy(GameObject.Find("IntroCanvas"));
            Debug.Log("main starter dialog start");
            UIEffect.Instance.enableCanvas(0);
            dialogueRunner.StartDialogue("MeetWithGhost");
            dialogueRunner.onDialogueComplete.AddListener(() =>
            {
                PlayerPrefs.SetInt("start_again", 1);
                MissionAlarm.Instance.show_mission(ranaway);
                MissionAlarm.afterAlarmGone = PlayerBehaviorOnStart.Instance.startChasing;
            });
            yield return null;
        }
        else
        {
            Destroy(GameObject.Find("IntroCanvas"));
            showScene();
        }
    }

    [YarnCommand("show_scene")]
    public void showScene()
    {
        PlayerBehavior.Instance.DisableMove();
        Debug.Log("show scene");
        TopUI.SetActive(true);
        HPbar.SetActive(true);
        UIEffect.afterEffect = delegate
        {
            if (startAgain==0)
            {
                HPmanager.Instance.showExtraHP();
            }
            else
            {
                MissionAlarm.Instance.show_mission(ranaway);
                MissionAlarm.afterAlarmGone = PlayerBehaviorOnStart.Instance.startChasing;
            }
        };
        UIEffect.Instance.Fade(0, 1);
    }
}
