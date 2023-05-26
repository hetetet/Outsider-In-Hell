using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using TMPro;
using Yarn.Unity;
public class TitleSetting : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler
{
    Image img;
    [SerializeField] public GameObject SettingArea;
    [SerializeField] Button Exit;
    public static TitleSetting Instance;
    bool isDisabled = false;

    //quit game
    [SerializeField] Canvas QuitCanvas;
    [SerializeField] GameObject QuitWindow;
    [SerializeField] Button Yes;
    [SerializeField] Button No;
    [SerializeField] TextMeshProUGUI explain;

    //volumn control
    [SerializeField] TextMeshProUGUI BgmValue;
    [SerializeField] TextMeshProUGUI EffectSoundValue;
    [SerializeField] Slider BgmSlider;
    [SerializeField] Slider EffectValueSlider;

    //cussfilter
    public static short cussFilterType = 0;
    public static string[] cusslist = { "좆", "씨발", "새끼", "병신", "존나", "지랄", "fuck", "shit", "asshole" };
    [SerializeField] ToggleGroup CussToggleGroup;
    CustomLineView customLineView;
    private Toggle Reverse;
    private Toggle Normal;
    private Toggle None;

    //localization
    public static string LanguageCode = "ko";
    TextLineProvider textLineProvider;
    YarnProject yarnProject;
    [SerializeField] TMP_Dropdown langDropDown;
    private void Awake()
    {
        Instance = this;

    }
    void Start()
    {
        img = GetComponent<Image>();
        Exit.onClick.AddListener(() =>
        {
            HideSettings();
        });
        BgmSlider.onValueChanged.AddListener(delegate {
            SoundManager.Instance.setBgmVolumn(BgmSlider.value / 100);
        });
        EffectValueSlider.onValueChanged.AddListener(delegate {
            SoundManager.Instance.setEffectVolumn(EffectValueSlider.value / 100);
        });
        No.onClick.AddListener(CloseQuitWindow);

        customLineView = FindObjectOfType<CustomLineView>();

        Reverse = CussToggleGroup.transform.Find("Reverse").GetComponent<Toggle>();
        Reverse.onValueChanged.AddListener(delegate { changeFilterMode(); });

        Normal = CussToggleGroup.transform.Find("Normal").GetComponent<Toggle>();
        Normal.onValueChanged.AddListener(delegate { changeFilterMode(); });

        None = CussToggleGroup.transform.Find("None").GetComponent<Toggle>();
        None.onValueChanged.AddListener(delegate { changeFilterMode(); });

        try
        {
            yarnProject = FindObjectOfType<DialogueRunner>().yarnProject;
            textLineProvider = FindObjectOfType<TextLineProvider>();
            textLineProvider.textLanguageCode = LanguageCode;
        }
        catch
        {
            Debug.Log("no yarnproject and textLineProvider in this scene");
        }

        if (LanguageCode == "ko")
        {
            langDropDown.value = 0;
            Debug.Log("korean on start");
        }
        else if (LanguageCode == "en")
        {
            langDropDown.value = 1;
            Debug.Log("english on start");
        }
        else
            Debug.Log("cant find language");
    }

    // Update is called once per frame
    void Update()
    {
        BgmValue.text = ((int)BgmSlider.value).ToString();
        EffectSoundValue.text = ((int)EffectValueSlider.value).ToString();
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
        ShowSettings();
    }
    public void ShowSettings() //esc 눌렀을 때도 켜져야됨
    {
        if (isDisabled)
            return;
        else if (SettingArea.activeSelf)
        {
            HideSettings();
            return;
        }
        SettingArea.SetActive(true);

        UIEffect.afterEffect = () =>
        {
            Time.timeScale = 0;
        };
        UIEffect.Instance.enableCanvas(20);
        UIEffect.Instance.setColor(1, 1, 1, 0);
        UIEffect.Instance.Fade(0.5f, 0.2f);
    }

    public void HideSettings()
    {
        UIEffect.Instance.Fade(0, 0.2f);
        Time.timeScale = 1;

        SettingArea.SetActive(false);
    }

    public void DisableIcon()
    {
        Debug.Log("disable icon_settings");
        isDisabled = true;
        SettingArea.SetActive(false);
        gameObject.GetComponent<ButtonBehavior>().enabled = false;
        img.color = new Color(0.5f, 0.5f, 0.5f);
    }

    public bool isSettingActive()
    {
        return SettingArea.activeSelf;
    }
    public void EnableIcon()
    {
        Debug.Log("enable icon_settings");
        isDisabled = false;
        gameObject.GetComponent<ButtonBehavior>().enabled = true;
        img.color = Color.white;
    }

    public float getBgmValue()
    {
        return BgmSlider.value / 100;
    }
    public float getEffectSoundValue()
    {
        return EffectValueSlider.value / 100;
    }

    public void changeFilterMode()
    {
        if (Reverse.isOn)
            cussFilterType = 0;
        else if (Normal.isOn)
            cussFilterType = 1;
        else
            cussFilterType = 2;
        customLineView.showLineAgain();
    }

    public void changeLanguage()
    {
        if (langDropDown.value == 0)
        {
            LanguageCode = "ko";
        }
        else if (langDropDown.value == 1)
        {
            LanguageCode = "en";
        }
        Debug.Log("languageCode: " + LanguageCode);
        //yarnProject.baseLocalization = yarnProject.localizations[langDropDown.value];
        //LanguageCode=textLineProvider.textLanguageCode = yarnProject.baseLocalization.name;
        //Debug.Log(yarnProject.baseLocalization);
    }
    public void AskGoMain()
    {
        explain.text = "메인 화면으로 돌아가시겠습니까?";
        QuitCanvas.gameObject.SetActive(true);
        QuitWindow.SetActive(true);
        Yes.onClick.RemoveAllListeners();
        Yes.onClick.AddListener(() =>
        {
            SaveData();
            Time.timeScale = 1;
            UIEffect.Instance.enableCanvas(999);
            UIEffect.Instance.setColor(0, 0, 0, 0);
            UIEffect.Instance.Fade(1, 1, "Title");
        });
    }
    public void AskQuit()
    {
        explain.text = "종료하시겠습니까?";
        QuitCanvas.gameObject.SetActive(true);
        QuitWindow.SetActive(true);
        Yes.onClick.RemoveAllListeners();
        Yes.onClick.AddListener(() =>
        {
            SaveData();
            Time.timeScale = 1;
            Application.Quit();
        });

    }

    public void CloseQuitWindow()
    {
        Time.timeScale = 1;
        GameManager.setCursorToArrow();
        QuitWindow.SetActive(false);
        QuitCanvas.gameObject.SetActive(false);
    }

    public void SaveData()
    {
        Debug.Log("Save game data");
    }
}
