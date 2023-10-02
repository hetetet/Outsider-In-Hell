using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Yarn.Unity;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;
using TMPro;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;
    //cursor
    [SerializeField] Texture2D cursorArrow;
    [SerializeField] Texture2D cursorPointer;
    [SerializeField] Texture2D cursorSelect;
    private static int cursorState = 0; //0: arrow, 1: pointer, 2:selected
    //localization
    public static string LanguageCode = "ko";
    TextLineProvider textLineProvider;
    YarnProject yarnProject;
    

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
        try
        {
            textLineProvider = GameObject.Find("DialogSystem").GetComponent<TextLineProvider>();
            textLineProvider.textLanguageCode = GameManager.LanguageCode;
            Debug.Log("onstart: this scene HAS textLineProvider");
        }
        catch
        {
            Debug.Log("onstart: no textLineProvider in this scene");
        }
        Debug.Log("onstart: no textLineProvider in this scene");
    }
    private void Update()
    {


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

    public void OnPointerUp(PointerEventData eventData)
    {
        //ShowSettings();
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
        PlayerBehavior.currentHP = PlayerBehavior.maxHP;
        PlayerBehavior.Instance.EnableMove();
    }
}
