using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ToastAlarm : MonoBehaviour
{
    [SerializeField] private Sprite[] Icons=new Sprite[4];
    [SerializeField] private Color[] BgColors = new Color[4];
    Image Img;
    Image Bg;
    TextMeshProUGUI Message;
    void Start()
    {
        Bg = GetComponentsInChildren<Image>()[0];
        Img = GetComponentsInChildren<Image>()[1];
        Message = GetComponentInChildren<TextMeshProUGUI>();        
    }

    public void SetMessageLayout(int num, string message)
    {
        Img.sprite = Icons[num];
        Bg.color = BgColors[num];
        Message.text = message;
    }
    void Update()
    {
        
    }
}
