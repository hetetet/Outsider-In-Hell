using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Yarn.Unity;

public class accessToLineText : MonoBehaviour
{
    TextMeshProUGUI line;
    public DialogueRunner.StringUnityEvent onLineUpdate;
    void Start()
    {
        line = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public void onLineUpdateFunc()
    {
        Debug.Log("line updated");
    }
}
