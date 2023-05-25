using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Yarn.Unity;
using UnityEngine.Localization.Settings;

public class MissionAlarm : MonoBehaviour
{
    [SerializeField] Image Mission;
    [SerializeField] TextMeshProUGUI missionText;
    
    public delegate void Del();
    public static MissionAlarm Instance;
    public static Del whenAlarmShown;
    public static Del afterAlarmGone;

    public bool missionShown = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }
    private void Start()
    {
        whenAlarmShown = () => { };
        afterAlarmGone = () => { };
    }

    private void Update()
    {
        if(missionShown && Input.GetButtonUp("Jump"))
            hide_mission();
    }

    [YarnCommand("show_mission")]
    public void show_mission(Mission mission)
    {
        PlayerBehavior.canmove = false;
        whenAlarmShown();
        whenAlarmShown = () => { };

        string desc = LocalizationSettings.StringDatabase.GetLocalizedString("Mission", mission.key);
        missionText.text = desc;
        Icon_Mission.Instance.addMission(mission);

        if (!UIEffect.Instance.isEnabled())//만약 fadeImage가 disabled 됐다면 fade 효과를 준다.
        {
            UIEffect.Instance.enableCanvas(0);
            UIEffect.Instance.setColor(1, 1, 1, 0);
            UIEffect.Instance.Fade(0.5f, 1);
        }            

        Mission.rectTransform.LeanMoveLocal(new Vector2(0, -110), 0.3f).setEaseOutQuad()
        .setOnComplete(() => {
            //Debug.Log(Mission.rectTransform.anchoredPosition.x.ToString() + ", " + Mission.rectTransform.anchoredPosition.y.ToString());
            Mission.rectTransform.LeanMoveLocal(new Vector2(0, -100), 0.3f).setEaseInQuad();
            missionShown = true;
            return;
        });
    }
    public void hide_mission()
    {
        Debug.Log("hide mission");
        missionShown = false;
        Mission.rectTransform.LeanMoveLocal(new Vector2(0, -110), 0.3f).setEaseInQuad()
        .setOnComplete(() => {
            UIEffect.Instance.Fade(0, 0.25f);
            Mission.rectTransform.LeanMoveLocal(new Vector2(0, 100), 0.3f).setEaseOutQuad().setOnComplete(() =>
            {
                afterAlarmGone();
                afterAlarmGone = () => { };
                PlayerBehavior.canmove = true;
                return;
            });           
        });
    }

    public void clear_mission()
    {

    }
}
