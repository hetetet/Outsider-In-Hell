using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalBehavior : MonoBehaviour
{
    public static string prevScenename = "";
    private bool isReadyToMove;
    [SerializeField] string dest;
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            isReadyToMove = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            isReadyToMove =false;
    }
    private void Update()
    {
        if (isReadyToMove && (Input.GetKeyUp(KeyCode.LeftShift) || (Input.GetKeyUp(KeyCode.RightShift))))
        {
            Debug.Log("load scene: " + dest);
            SceneManager.LoadScene(dest);
        }
    }
}