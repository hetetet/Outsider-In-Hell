using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TitleHandler : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button LoadButton;
    [SerializeField] private Button SettingButton;

    void Awake()
    {

    }

    private void Start()
    {
        startButton.onClick.AddListener(startGame);
        LoadButton.onClick.AddListener(loadGame);
        SettingButton.onClick.AddListener(GameManager.Instance.ShowSettings);

        GameManager.isSettingDisabled = false;
        Icon_Mission.Missions.Clear();
        Icon_Backpack.Items.Clear();

        GameManager.setCursorToArrow();
        UIEffect.Instance.enableCanvas(999);
        UIEffect.Instance.setColor(0, 0, 0, 1);
        UIEffect.Instance.Fade(0, 1);
    }

    public void startGame() {
        UIEffect.Instance.enableCanvas(999);
        UIEffect.Instance.setColor(0, 0, 0, 0);
        UIEffect.Instance.Fade(1, 2, "Prologue");       
    }

    public void loadGame()
    {
        Debug.Log("Load gay");
    }
}
