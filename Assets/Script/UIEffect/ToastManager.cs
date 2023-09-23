using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ToastManager : MonoBehaviour
{
    public static ToastManager Instance;
    [SerializeField] GameObject Toast;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        
    }

    void Update()
    {
        //�佺Ʈ�� �ߵǴ��� Ȯ���ϴ� ����� �ڵ�
        if (Input.GetKeyUp(KeyCode.T) && Toast != null)
        {
            GenerateToast(1, "����̰� ���� �� ���� ȹ���߽��ϴ�.");
        }
    }

    public void GenerateToast(int num, string message)
    {

        StartCoroutine(CoGenerateToast( num, message));
    }

    public IEnumerator CoGenerateToast(int num, string message)
    {
        //��ȣ - 0:�̼� 1:���� 2:���� 3:����
        GameObject toast = Instantiate(Toast, transform); //�佺Ʈ���̾ƿ��� �ڽ����� Toast
        toast.GetComponent<ToastAlarm>().SetMessageLayout(num,message);
        //1���Ŀ� ������ �������
        yield return new WaitForSeconds(3f);
        toast.GetComponent<ToastAlarm>().FadeOutMessage(1f);
        yield return new WaitForSeconds(2f);
        Destroy(toast);
    }
}
