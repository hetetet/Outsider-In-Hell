using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpManager : MonoBehaviour
{
    [SerializeField] Image Panel;
    [SerializeField] Sprite[] helps;
    [SerializeField] Button[] buttons;

    private void Awake()
    {
        Debug.Log("helps.Length" + helps.Length);
        for (int i = 0; i < helps.Length; i++)
        {
            int index = i;//�̷��� ���ϸ� i�� 4 �� ä��addlistner �����
            if(helps[index] != null && buttons[index] != null)
            {
                Debug.Log("helps[" + index.ToString() + "]");
                buttons[index].onClick.AddListener(() => {
                    Debug.Log("������ ����: helps[" + index.ToString() + "]");
                    Panel.sprite = helps[index];
                });
            }
        }
    }
}
