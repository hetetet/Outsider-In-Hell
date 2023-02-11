using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorHandle : MonoBehaviour
{
    private void OnMouseEnter()
    {
        GameManager.setCursorToPointer();
        Debug.Log("mouse enter");
    }

    private void OnMouseExit()
    {
        GameManager.setCursorToArrow();
    }
}
