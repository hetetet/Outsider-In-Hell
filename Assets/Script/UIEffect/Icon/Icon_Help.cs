using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
public class Icon_Help : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler
{
    Image img;
    [SerializeField] public GameObject HelpArea;
    [SerializeField] Button Exit;
    public static Icon_Help Instance;
    bool isDisabled = false;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
        Exit.onClick.AddListener(hideHelp);
    }


    void hideHelp()
    {
        UIEffect.Instance.Fade(0, 0.4f);
        Time.timeScale = 1;
        HelpArea.transform.DOScale(new Vector2(1.1f, 1.1f), 0.1f).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            HelpArea.transform.DOScale(new Vector2(0, 0), 0.25f).SetEase(Ease.InQuad).OnComplete(() =>
            {
                HelpArea.SetActive(false);
                Icon_Mission.Instance.EnableIcon();
                Icon_Backpack.Instance.EnableIcon();
                Icon_Settings.Instance.EnableSettingIcon();
            });
        });
    }

    void Start()
    {
        img = GetComponent<Image>();
        HelpArea.transform.DOScale(new Vector2(0, 0), 0.01f);
    }

    void Update()
    {
        
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

    public void OnPointerUp(PointerEventData eventData)
    {
        if(isDisabled)
            return;
        else if (HelpArea.activeSelf)
        {
            hideHelp();
            return;
        }
        HelpArea.SetActive(true);
        Icon_Mission.Instance.DisableIcon();
        Icon_Backpack.Instance.DisableIcon();
        Icon_Settings.Instance.DisableSettingIcon();

        UIEffect.afterEffect = () =>
        {
            Time.timeScale = 0;
        };
        UIEffect.Instance.enableCanvas(10);
        UIEffect.Instance.setColor(1, 1, 1, 0);
        UIEffect.Instance.Fade(0.5f, 0.4f);
        HelpArea.transform.DOScale(new Vector2(1.1f, 1.1f), 0.25f).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            HelpArea.transform.DOScale(new Vector2(1, 1), 0.1f).SetEase(Ease.InQuad);
        });
    }
    public void DisableIcon()
    {
        Debug.Log("disable icon_help");
        isDisabled = true;
        gameObject.GetComponent<ButtonBehavior>().enabled = false;
        HelpArea.SetActive(false);
        img.color = new Color(0.5f, 0.5f, 0.5f);
    }
    public void EnableIcon()
    {
        Debug.Log("enable icon_help");
        isDisabled = false;
        gameObject.GetComponent<ButtonBehavior>().enabled = true;
        img.color = Color.white;
    }
}
