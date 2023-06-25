using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using TMPro;
using Yarn.Unity;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    //cursor
    [SerializeField] Texture2D cursorArrow;
    [SerializeField] Texture2D cursorPointer;
    [SerializeField] Texture2D cursorSelect;
    private static int cursorState = 0; //0: arrow, 1: pointer, 2:selected
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

    //localization
    public static string LanguageCode = "ko";
    TextLineProvider textLineProvider;
    YarnProject yarnProject;
    [SerializeField] TMP_Dropdown langDropDown_;
    [SerializeField] GameObject Player;


    private void Awake()
    {
        if(Instance==null)
            Instance = this;
        else if(Instance!=this)
            Destroy(gameObject);
        DontDestroyOnLoad(this);
    }

    private void Start()
    {

        SettingCanvas = transform.Find("SettingCanvas").GetComponent<Canvas>();
        Quitcanvas = transform.Find("QuitCanvas").GetComponent<Canvas>();
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
        Nobtn.onClick.AddListener(CloseQuitWindow);

        customLineView = FindObjectOfType<CustomLineView>();

        Reverse = CussToggleGroup.transform.Find("Reverse").GetComponent<Toggle>();
        Reverse.onValueChanged.AddListener(delegate { changeFilterMode(); });

        Normal = CussToggleGroup.transform.Find("Normal").GetComponent<Toggle>();
        Normal.onValueChanged.AddListener(delegate { changeFilterMode(); });

        None = CussToggleGroup.transform.Find("None").GetComponent<Toggle>();
        None.onValueChanged.AddListener(delegate { changeFilterMode(); });



        if (LanguageCode == "ko")
        {
            langDropDown_.value = 0;
        }
        else if (LanguageCode == "en")
        {
            langDropDown_.value = 1;
        }
        else
            Debug.Log("cant find language");

        try { 
            textLineProvider = GameObject.Find("DialogSystem").GetComponent<TextLineProvider>();
            textLineProvider.textLanguageCode = LanguageCode;
            Debug.Log("onstart: this scene HAS textLineProvider");
        }
        catch
        {
            Debug.Log("onstart: no textLineProvider in this scene");
        }
    }
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

    public static void setCursorToArrow()
    {
        Cursor.SetCursor(Instance.cursorArrow, Vector2.zero, cursorMode: CursorMode.ForceSoftware);
        cursorState = 0;
    }

    public static void setCursorToPointer()
    {
        Cursor.SetCursor(Instance.cursorPointer, Vector2.zero, cursorMode: CursorMode.ForceSoftware);
        cursorState=1;
    }

    public static void setCursorToSelect()
    {
        Cursor.SetCursor(Instance.cursorSelect, Vector2.zero, cursorMode: CursorMode.ForceSoftware);
        cursorState = 2;
    }
    public static int getCursorState()
    {
        return cursorState;
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
    public void OnPointerUp(PointerEventData eventData)
    {
        //ShowSettings();
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
        {}
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
            customLineView.showLineAgain();
        }
        catch { }
    }

    public void changeLanguage()
    {
        //0: korean, 1:english
        if (langDropDown_.value == 0)
        {
            LanguageCode = "ko";
        }
        else if (langDropDown_.value == 1)
        {
            LanguageCode = "en";
        }
        
        StartCoroutine(setLocale(langDropDown_.value));
        try
        {
            customLineView=FindObjectOfType<CustomLineView>();
            customLineView.showLineAgain();
        }
        catch{}
    }

    IEnumerator setLocale(int localeID)
    {
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeID];
        Debug.Log("SelectedLocale: " + LocalizationSettings.SelectedLocale);
    }
    public void AskGoMain()
    {
        Explain.text = LocalizationSettings.StringDatabase.GetLocalizedString("UI", "gomain");
        Quitcanvas.gameObject.SetActive(true);
        Yesbtn.onClick.RemoveAllListeners();
        Yesbtn.onClick.AddListener(() =>
        {
            SaveData();
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
            SaveData();
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

    public void SaveData()
    {
        Debug.Log("Save game data");
        PlayerPrefs.SetString("CurrentScene", SceneManager.GetActiveScene().name);
        //hp
        PlayerPrefs.SetInt("Hp", PlayerBehavior.currentHP);
        //maxhp
        PlayerPrefs.SetInt("MaxHp", PlayerBehavior.maxHP);
        //items
        string itemarr = "";
        foreach(Item item in BackpackManager.Items)
            itemarr += item.name + "_" + item.number.ToString() + "#";        
        PlayerPrefs.SetString("Items", itemarr);
        //missions
        string missionarr = "";
        foreach(Mission mission in Icon_Mission.Missions)
            missionarr += mission.name + "#";
        PlayerPrefs.SetString("Missions", missionarr);
        Debug.Log("missionarr: " + missionarr + ", itemarr: " + itemarr);
    }


    public bool isSetAreaActive()
    {
        return SettingCanvas.gameObject.activeSelf;
    }

    public void Revive()
    {
        StartCoroutine("coRevive");
    }

    IEnumerator coRevive()
    {
        yield return new WaitForSeconds(0.5f);
        Debug.Log("revive");        
        SceneManager.LoadScene("Main_map01");
        UIEffect.Instance.Fade(0, 1);
        yield return new WaitForSeconds(0.5f);
        var NewPlayer=Instantiate(Player);
        NewPlayer.gameObject.name = "Soyeon";
        NewPlayer.transform.position = GameObject.Find("FromStartToHere").transform.position;
        NewPlayer.transform.localScale = new Vector3(-NewPlayer.transform.localScale.x, NewPlayer.transform.localScale.y, NewPlayer.transform.localScale.z);
        PlayerBehavior.Instance.EnableMove();
    }
}
