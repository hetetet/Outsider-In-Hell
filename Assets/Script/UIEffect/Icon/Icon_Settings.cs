using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Icon_Settings : MonoBehaviour ,IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler
{
    Image img;
    public static Icon_Settings Instance;
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {

        img = GetComponent<Image>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (GameManager.isSettingDisabled)
            return;
        img.transform.DOScale(new Vector3(1.2f, 1.2f, 1), 0.3f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (GameManager.isSettingDisabled)
            return;
        img.transform.DOScale(new Vector3(1f, 1f, 1), 0.3f);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if(!GameManager.Instance.isSettingActive())
            GameManager.Instance.ShowSettings();
        else
            GameManager.Instance.HideSettings();
    }

    public void DisableSettingIcon()
    {
        Debug.Log("disable icon_settings");
        GameManager.isSettingDisabled = true;
        GameManager.Instance.SettingCanvas.gameObject.SetActive(false);
        gameObject.GetComponent<ButtonBehavior>().enabled = false;
        img.color = new Color(0.5f, 0.5f, 0.5f);
    }

    public void EnableSettingIcon()
    {
        Debug.Log("enable icon_settings");
        GameManager.isSettingDisabled = false;
        gameObject.GetComponent<ButtonBehavior>().enabled = true;
        img.color = Color.white;
    }
}
