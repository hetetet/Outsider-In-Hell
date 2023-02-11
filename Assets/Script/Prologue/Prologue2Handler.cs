using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;
using DG.Tweening;
public class Prologue2Handler : MonoBehaviour
{
    [SerializeField] DialogueRunner DialogueRunner;
    [SerializeField] Mission fainthold;

    [SerializeField] MissionAlarm missionAlarm;
    [SerializeField] BgTilt bgTilt;

    [SerializeField] GameObject Guide;
    Image LeftArrow;
    Image RightArrow;

    // Start is called before the first frame update
    void Start()
    {
        UIEffect.Instance.setColor(0, 0, 0, 1);
        StartCoroutine(startProlouge());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator startProlouge()
    {
        UIEffect.Instance.enableCanvas(999);
        UIEffect.Instance.Fade(0,1);
        yield return new WaitForSeconds(1f);
        DialogueRunner.StartDialogue("Prologue2");
        DialogueRunner.onDialogueComplete.AddListener(() =>
        {
            missionAlarm.show_mission(fainthold);
        });
        LeftArrow = Guide.transform.Find("LeftArrow").GetComponent<Image>();
        RightArrow = Guide.transform.Find("RightArrow").GetComponent<Image>();


        MissionAlarm.whenAlarmShown = () => {
            Guide.SetActive(true);

            Color coral = new Color((float)248 / 255, (float)131 / 255, (float)121 / 255);
            Sequence blink = DOTween.Sequence();
            for (int i = 0; i < 3 * 2; i += 2)
            {
                blink.Insert(i * 0.2f, LeftArrow.DOColor(coral, 0.2f));
                blink.Insert(i * 0.2f, RightArrow.DOColor(coral, 0.2f));
                blink.Insert((i + 1) * 0.2f, LeftArrow.DOColor(Color.white, 0.2f));
                blink.Insert((i + 1) * 0.2f, RightArrow.DOColor(Color.white, 0.2f));
            }
            blink.Restart();
        };
        MissionAlarm.afterAlarmGone = bgTilt.startTilt; //미션 창을 끄면 bgTilt 스트립트의 startTilt 함수 실행
    }
}
