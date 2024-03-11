using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
public class PortalBehavior : MonoBehaviour
{
    public static string prevScenename = "";
    private bool isReadyToMove;
    [SerializeField] string dest;
    [SerializeField] bool automove;
    [SerializeField] Color fadeColor;
    [SerializeField] float end=0;
    [SerializeField] float fadetime=0;
    // Start is called before the first frame update
    private void Awake()
    {
        //automove = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (automove)
            {
                MoveScene(dest);
            }                
            isReadyToMove = true;
        }           
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            isReadyToMove =false;
    }
    private void Update()
    {
        if (isReadyToMove && (Input.GetKeyUp(KeyCode.LeftShift) || (Input.GetKeyUp(KeyCode.RightShift))))
            MoveScene(dest);
    }

    void MoveScene(string dest)
    {
        if (Application.CanStreamedLevelBeLoaded(dest))//해당 씬 이름이 있는 경우에만 이동
        {
            Debug.Log("load scene: " + dest);
            //StartCoroutine(SceneLoading());
            UIEffect.Instance.enableCanvas(998);
            UIEffect.Instance.setColor(fadeColor.r, fadeColor.g, fadeColor.b, fadeColor.a);
            UIEffect.Instance.Fade(end, fadetime, dest);
        }
        else
        {
            ToastManager.Instance.GenerateToast(2, "Cannot load the scene: " + dest);
            Debug.Log("Cannot load the scene:" + dest);
        }
    }

    //IEnumerator SceneLoading()
    //{
    //    var mAsyncOperation = SceneManager.LoadSceneAsync(dest, LoadSceneMode.Additive);
    //    yield return mAsyncOperation;
    //    mAsyncOperation = SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
    //    yield return mAsyncOperation;
    //}
}
