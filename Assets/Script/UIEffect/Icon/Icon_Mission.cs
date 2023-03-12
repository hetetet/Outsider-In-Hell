using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using TMPro;
using UnityEngine.Localization.Settings;
public class Icon_Mission : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler
{
    Image img;
    public static Icon_Mission Instance;
    public static List<Mission> Missions = new List<Mission>();
    [SerializeField] GameObject TodoElement;
    [SerializeField] public GameObject TodoArea;
    [SerializeField] ContentSizeFitter Elements; //원래 그냥 transform
    [SerializeField] Image NewMark;
    bool isDisabled=false;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        NewMark.gameObject.SetActive(false);
        img = GetComponent<Image>();

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isDisabled)
            return;
        img.transform.DOScale(new Vector3(1.2f, 1.2f, 1), 0.3f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isDisabled)
            return;
        img.transform.DOScale(new Vector3(1f, 1f, 1), 0.3f);
    }
    public void OnPointerUp(PointerEventData data)
    {
        if(isDisabled)
            return;

        if (!TodoArea.activeSelf)
        {
            Icon_Backpack.Instance.DisableIcon();
            Icon_Settings.Instance.DisableSettingIcon();
            Icon_Help.Instance.DisableIcon();

            NewMark.gameObject.SetActive(false);
            showMission();
            TodoArea.SetActive(true);
        }
        else
        {
            Icon_Backpack.Instance.EnableIcon();
            Icon_Settings.Instance.EnableSettingIcon();
            Icon_Help.Instance.EnableIcon();
            TodoArea.SetActive(false);
        }
    }
    public void addMission(Mission mission)
    {
        NewMark.gameObject.SetActive(true);
        Missions.Add(mission);
    }

    public void deleteMission(Mission mission)
    {
        Missions.Remove(mission);
        showMission();
    }

    public void showMission()
    {
        foreach (Transform mission in Elements.transform)
        {
            Destroy(mission.gameObject);
        }
        foreach (var mission in Missions)
        {
            GameObject obj = Instantiate(TodoElement, Elements.transform);

            var MissionTMP=obj.transform.Find("Text").GetComponent<TextMeshProUGUI>();
            string localizedMissonName= LocalizationSettings.StringDatabase.GetLocalizedString("Mission", mission.key);
            MissionTMP.text = localizedMissonName;

            //LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)Elements.transform);
        }       
    }


    public void DisableIcon()
    {
        isDisabled = true;
        TodoArea.SetActive(false);
        gameObject.GetComponent<ButtonBehavior>().enabled = false;
        img.color = new Color(0.5f, 0.5f, 0.5f);
    }
    public void EnableIcon()
    {
        isDisabled = false;
        gameObject.GetComponent<ButtonBehavior>().enabled = true;
        img.color = Color.white;
    }
}
