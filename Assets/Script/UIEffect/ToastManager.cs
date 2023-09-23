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
        //토스트가 잘되는지 확인하는 시험용 코드
        if (Input.GetKeyUp(KeyCode.T) && Toast != null)
        {
            GenerateToast(1, "뱀목이가 어제 싼 똥을 획득했습니다.");
        }
    }

    public void GenerateToast(int num, string message)
    {

        StartCoroutine(CoGenerateToast( num, message));
    }

    public IEnumerator CoGenerateToast(int num, string message)
    {
        //번호 - 0:미션 1:가방 2:설정 3:도움말
        GameObject toast = Instantiate(Toast, transform); //토스트레이아웃의 자식으로 Toast
        toast.GetComponent<ToastAlarm>().SetMessageLayout(num,message);
        //1초후에 서서히 사라지게
        yield return new WaitForSeconds(3f);
        toast.GetComponent<ToastAlarm>().FadeOutMessage(1f);
        yield return new WaitForSeconds(2f);
        Destroy(toast);
    }
}
