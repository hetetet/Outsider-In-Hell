using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckHeadBump : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("CheckHeadBump start");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        {
            if (collision.gameObject.tag == "Land")
            {
                PlayerBehaviorOnStart.Instance.doBump();
            }
        }
    }
}
