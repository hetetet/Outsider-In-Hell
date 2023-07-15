using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Localization.Settings;
public class TitleHandler : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button SettingButton;
    //delete data
    public Button deleteDataBtn;
    public Canvas deleteData;
    public TextMeshProUGUI deleteTitle;
    public Button deleteOk;
    public Button deleteCancel;
    public Animator Anim;

    void Awake()
    {

    }

    private void Start()
    {
        deleteDataBtn.onClick.AddListener(OpenDeleteDataPopup);
        deleteData.gameObject.SetActive(false);
        startButton.onClick.AddListener(startGame);
        SettingButton.onClick.AddListener(GameManager.Instance.ShowSettings);

        GameManager.isSettingDisabled = false;
        Icon_Mission.Missions.Clear();
        BackpackManager.Items.Clear();

        GameManager.setCursorToArrow();
        UIEffect.Instance.enableCanvas(999);
        UIEffect.Instance.setColor(0, 0, 0, 1);
        UIEffect.Instance.Fade(0, 1);
    }

    public void startGame() {
        UIEffect.Instance.enableCanvas(999);
        UIEffect.Instance.setColor(0, 0, 0, 0);
        
        //가져와야 할 정보 목록
        //플레이중이었던 씬
        string CurrentScene=PlayerPrefs.GetString("CurrentScene", "none");
        //hp
        int Hp=PlayerPrefs.GetInt("Hp", 100);
        //maxhp
        int MaxHp=PlayerPrefs.GetInt("MaxHp", 100);
        //미션 목록
        string misarr= PlayerPrefs.GetString("Missions", "none");
        //아이템 목록
        string itemarr=PlayerPrefs.GetString("Items", "none");
        //얘네둘은 타이틀 화면에서 못 불러오는데 어떡하지...? uieffect의 aftereffect?
        //npc별 대화 여부는 대화 후 따로 저장

        if (CurrentScene=="none") 
            UIEffect.Instance.Fade(1, 2, "Prologue");
        else
        {
            UIEffect.Instance.Fade(1, 2, CurrentScene);
            UIEffect.afterEffect = delegate
            {
                //add missions and items
                string[] items = itemarr.Split("#");
                foreach(string item in items)
                {
                    string[] iteminfo = item.Split('_');
                    //BackpackManager.Items.Add();
                }
            };
        }
    }

    public void DeleteGame()
    {
        Debug.Log("Delete gay");
        DeleteData();
    }
    public void DeleteData()
    {
        deleteOk.gameObject.SetActive(false);
        deleteCancel.gameObject.SetActive(false);
        Anim.SetTrigger("eat");
        Debug.Log("Delete game data");
        PlayerPrefs.DeleteAll();
        StartCoroutine("CoDeleteData");
    }

    IEnumerator CoDeleteData()
    {
        yield return new WaitForSeconds(2f); //데이터 먹는 애니메이션 시간동안
        deleteOk.gameObject.SetActive(true);
        deleteOk.onClick.RemoveAllListeners();
        deleteOk.onClick.AddListener(CloseDeleteDataPopup);
        deleteTitle.text = LocalizationSettings.StringDatabase.GetLocalizedString("UI", "datadeleted");
    }
    public void OpenDeleteDataPopup()
    {
        Anim.SetTrigger("init");

        deleteData.gameObject.SetActive(true);
        deleteOk.gameObject.SetActive(true);
        deleteCancel.gameObject.SetActive(true);

        deleteTitle.text = LocalizationSettings.StringDatabase.GetLocalizedString("UI", "askdeletedata");
       
        deleteOk.onClick.RemoveAllListeners();
        deleteCancel.onClick.RemoveAllListeners();
        deleteCancel.onClick.AddListener(CloseDeleteDataPopup);
        deleteOk.onClick.AddListener(DeleteData);
    }
    public void CloseDeleteDataPopup()
    {
        deleteData.gameObject.SetActive(false);
        deleteOk.onClick.RemoveAllListeners();
        deleteCancel.onClick.RemoveAllListeners();
    }
}
