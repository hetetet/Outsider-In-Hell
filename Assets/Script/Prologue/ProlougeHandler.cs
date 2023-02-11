using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class ProlougeHandler : MonoBehaviour
{    
    [SerializeField] public SerializableDictionary<string, Sprite[]> CharDict;
    [SerializeField] Sprite fadeBackgroudSprite;
    [SerializeField] Button getOutBtn;
    [SerializeField] Mission finditems;

    [SerializeField] AudioClip[] clip;

    DialogueRunner DialogueRunner;

    private void Awake()
    {
        GameManager.setCursorToArrow();
        DialogueRunner=FindObjectOfType<DialogueRunner>();
    }
    private void Start()
    {
        UIEffect.Instance.enableCanvas(999);
        UIEffect.Instance.setColor(0, 0, 0, 1);
        
        RectTransform getoutRectTransform = getOutBtn.GetComponent<RectTransform>();
        getoutRectTransform.anchoredPosition = new Vector2(0, 300);
        getOutBtn.onClick.AddListener(() =>
        {
            UIEffect.Instance.enableCanvas(999);
            UIEffect.Instance.setColor(0, 0, 0, 0);
            UIEffect.Instance.Fade(1, 2, "Prologue2");
        });

        float sceneChangeTime = 2f;
        SoundManager.Instance.playEffectSound(clip[0]);
        StartCoroutine(startProlouge(clip[0].length, sceneChangeTime));
    }

    IEnumerator startProlouge(float startDelay, float sceneChangeTime)
    {
        yield return new WaitForSeconds(startDelay);
        UIEffect.Instance.enableCanvas(0);
        UIEffect.Instance.ChangeColor(new Color(1, 1, 1, 0.5f), 1.5f);
        yield return new WaitForSeconds(1.5f);
        DialogueRunner.StartDialogue("Start");
        MissionAlarm.afterAlarmGone = delegate { ItemHandler.Instance.setCanClickItem(true); };
        DialogueRunner.onDialogueComplete.AddListener(() =>
        {
            MissionAlarm.Instance.show_mission(finditems);            
        });
    }
}
