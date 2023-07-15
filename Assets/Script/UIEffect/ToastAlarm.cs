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
    Image Img; //��� ī�װ��� �˶�����
    Image Bg; //ī�װ��� �´� ����
    TextMeshProUGUI Message; //�佺Ʈ ����
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

    public void SetMessageLayout(int num, string message) //num�� ���� �˶� ���� ����, 
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
