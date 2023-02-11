using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonBehavior : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler //IPointerUpHandler,
{
    private bool onbtn = false;
    private void Start()
    {

    }

    private void Update()
    {
        if(Input.GetMouseButtonUp(0) && GameManager.getCursorState()==2)
            GameManager.setCursorToPointer();
    }

    

    private void OnEnable()
    {
        if (IsMouseUiWithBtn())// && state == 0
        {
            GameManager.setCursorToPointer();
            onbtn = true;
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GameManager.setCursorToPointer();
        onbtn = true;
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        GameManager.setCursorToArrow();
        onbtn = false;
    }

    public void OnPointerDown(PointerEventData data)
    {
        if (onbtn)
        {
            GameManager.setCursorToSelect();
        }

    }

    private bool IsMouseUiWithBtn()
    {
        PointerEventData peData=new PointerEventData(EventSystem.current);
        peData.position = Input.mousePosition;

        List<RaycastResult> raycastResultList=new List<RaycastResult>();
        EventSystem.current.RaycastAll(peData,raycastResultList);
        for(int i=0; i<raycastResultList.Count; i++)
        {
            if (raycastResultList[i].gameObject.GetComponent<ButtonBehavior>() == null)
            {
                raycastResultList.RemoveAt(i);
                i--;
            }
        }
        return raycastResultList.Count > 0;
    }
}
