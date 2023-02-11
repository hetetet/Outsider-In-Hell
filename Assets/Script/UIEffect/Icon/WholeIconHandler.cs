using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WholeIconHandler : MonoBehaviour
{
    public static WholeIconHandler Instance;

    [SerializeField] GameObject ItemArea;
    [SerializeField] GameObject HelpArea;
    void Start()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        //TodoArea=transform.Find("TodoArea").GetComponent<GameObject>();
        //ItemArea = transform.Find("ItemArea").GetComponent<GameObject>();
        //HelpArea = transform.Find("HelpArea").GetComponent<GameObject>();
    }

    public bool isIconActive()
    {
        return ItemArea.activeSelf || HelpArea.activeSelf || GameManager.Instance.isSetAreaActive();
    }
}
