using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;
using TMPro;
using UnityEngine.Localization.Settings;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class SettingManager : MonoBehaviour
{
    public static SettingManager Instance = null;

    Image img;

    public Canvas SettingCanvas;
    [SerializeField] Button Exit;
    public static bool isSettingDisabled = false;

    //quit game
    Canvas Quitcanvas;
    [SerializeField] Button Yesbtn;
    [SerializeField] Button Nobtn;
    [SerializeField] TextMeshProUGUI Explain;

    //volumn control
    [SerializeField] TextMeshProUGUI BgmValue;
    [SerializeField] TextMeshProUGUI EffectSoundValue;
    [SerializeField] Slider BgmSlider;
    [SerializeField] Slider EffectValueSlider;

    //cussfilter
    public static short cussFilterType = 1;
    public static string[] cusslist = { "Á¿", "¾¾¹ß", "»õ³¢", "º´½Å", "Á¸³ª", "Áö¶ö", "fuck", "shit", "asshole" };
    [SerializeField] ToggleGroup CussToggleGroup;
    CustomLineView customLineView;
    private Toggle Reverse;
    private Toggle Normal;
    private Toggle None;
    
    //language
    [SerializeField] TMP_Dropdown langDropDown_;

    [SerializeField] GameObject TopUI;
    [SerializeField] GameObject HpBar;
    private void Awake()
    {           
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        SettingCanvas = transform.Find("SettingCanvas").GetComponent<Canvas>();
        Quitcanvas = transform.Find("QuitCanvas").GetComponent<Canvas>();
        
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
        Nobtn.onClick.AddListener(CloseQuitWindow);

        customLineView = FindObjectOfType<CustomLineView>();

        langDropDown_.onValueChanged.AddListener(delegate { changeLanguage(); });

        Reverse = CussToggleGroup.transform.Find("Reverse").GetComponent<Toggle>();
        Reverse.onValueChanged.AddListener(delegate { changeFilterMode(); });

        Normal = CussToggleGroup.transform.Find("Normal").GetComponent<Toggle>();
        Normal.onValueChanged.AddListener(delegate { changeFilterMode(); });

        None = CussToggleGroup.transform.Find("None").GetComponent<Toggle>();
        None.onValueChanged.AddListener(delegate { changeFilterMode(); });

        //set sound value as default
        Debug.Log("BgmSlider.value: " + BgmSlider.value + ", EffectValueSlider.value: " + EffectValueSlider.value);
        BgmSlider.value = 44f;

        EffectValueSlider.value = 44f;


        if (GameManager.LanguageCode == "ko")
        {
            langDropDown_.value = 0;
        }
        else if (GameManager.LanguageCode == "en")
        {
            langDropDown_.value = 1;
        }
        else
            Debug.Log("cant find language");


    }

// Update is called once per frame
private void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            if (!isSettingActive())
                ShowSettings();
            else
                HideSettings();
        }
        BgmValue.text = ((int)BgmSlider.value).ToString();
        EffectSoundValue.text = ((int)EffectValueSlider.value).ToString();
    }

    public void ShowSettings() //esc ´­·¶À» ¶§µµ ÄÑÁ®¾ßµÊ
    {
        Debug.Log("show settings");
        if (isSettingDisabled)
            return;

        Time.timeScale = 0;
        SettingCanvas.gameObject.SetActive(true);
        try
        {
            Icon_Mission.Instance.DisableIcon();
            Icon_Backpack.Instance.DisableIcon();
            Icon_Help.Instance.DisableIcon();
        }
        catch
        {

        }

    }

    public void HideSettings()
    {
        Debug.Log("hide settings");
        Time.timeScale = 1;

        SettingCanvas.gameObject.SetActive(false);
        try
        {
            Icon_Mission.Instance.EnableIcon();
            Icon_Backpack.Instance.EnableIcon();
            Icon_Help.Instance.EnableIcon();
        }
        catch
        { }
    }


    public bool isSettingActive()
    {
        return SettingCanvas.gameObject.activeSelf;
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
        try
        {
            customLineView = FindObjectOfType<CustomLineView>();
            customLineView.showLineAgain();
        }
        catch
        {
            Debug.Log("No customLineView in this scene");
        }
    }

    public void changeLanguage()
    {
        //0: korean, 1:english
        if (langDropDown_.value == 0)
        {
            GameManager.LanguageCode = "ko";
        }
        else if (langDropDown_.value == 1)
        {
            GameManager.LanguageCode = "en";
        }

        StartCoroutine(setLocale(langDropDown_.value));
        try
        {
            customLineView = FindObjectOfType<CustomLineView>();
            customLineView.showLineAgain();
        }
        catch { }
    }

    IEnumerator setLocale(int localeID)
    {
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeID];
        Debug.Log("SelectedLocale: " + LocalizationSettings.SelectedLocale);
    }
    public void AskGoMain()
    {
        if (SceneManager.GetActiveScene().name == "Title")
            return;
        Explain.text = LocalizationSettings.StringDatabase.GetLocalizedString("UI", "gomain");
        Quitcanvas.gameObject.SetActive(true);
        Yesbtn.onClick.RemoveAllListeners();
        Yesbtn.onClick.AddListener(() =>
        {
            GameManager.Instance.SaveData();
            SoundManager.Instance.stopBGM();
            Quitcanvas.gameObject.SetActive(false);
            SettingCanvas.gameObject.SetActive(false);
            Time.timeScale = 1;
            isSettingDisabled = true;
            UIEffect.Instance.enableCanvas(1972);
            UIEffect.Instance.setColor(0, 0, 0, 0);
            UIEffect.Instance.Fade(1, 1, "Title");
        });
    }
    public void AskQuit()
    {
        Explain.text = LocalizationSettings.StringDatabase.GetLocalizedString("UI", "goexit");
        Quitcanvas.gameObject.SetActive(true);
        Yesbtn.onClick.RemoveAllListeners();
        Yesbtn.onClick.AddListener(() =>
        {
            GameManager.Instance.SaveData();
            Time.timeScale = 1;
            Application.Quit();
        });
    }

    public void CloseQuitWindow()
    {
        Time.timeScale = 1;
        GameManager.setCursorToArrow();
        Quitcanvas.gameObject.SetActive(false);
    }
    public bool isSetAreaActive()
    {
        return SettingCanvas.gameObject.activeSelf;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isSettingDisabled)
            return;
        img.transform.DOScale(new Vector3(1.2f, 1.2f, 1), 0.3f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isSettingDisabled)
            return;
        img.transform.DOScale(new Vector3(1f, 1f, 1), 0.3f);
    }
}
