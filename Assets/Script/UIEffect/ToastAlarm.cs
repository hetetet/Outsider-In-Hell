using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
public class ToastAlarm : MonoBehaviour
{
    [SerializeField] private Sprite[] Icons=new Sprite[4];
    [SerializeField] private Color[] BgColors = new Color[4];
    Image Img; //어느 카테고리의 알람인지
    Image Bg; //카테고리에 맞는 색깔
    TextMeshProUGUI Message; //토스트 내용
    private void Awake()
    {
        Debug.Log("ToastAlarm start");
        Bg = GetComponentsInChildren<Image>()[0];
        Img = GetComponentsInChildren<Image>()[1];
        Message = GetComponentInChildren<TextMeshProUGUI>();
    }
    void Start()
    {
     
    }

    public void SetMessageLayout(int num, string message) //num에 따라 알람 종류 결정, 
    {
        Debug.Log(message);
        Img.sprite = Icons[num];
        Bg.color = BgColors[num];
        Message.text = message; 
    }

    public void FadeOutMessage(float duration)
    {
        Img.DOFade(0, duration);
        Bg.DOFade(0, duration);
        Message.DOFade(0, duration);
    }
    void Update()
    {
        
    }
}
