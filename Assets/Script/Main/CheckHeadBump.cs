using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckHeadBump : MonoBehaviour
{
    
    void Start()
    {
        Debug.Log("CheckHeadBump start ::"+this.gameObject.name+", "+this.gameObject.transform.position.ToString());
        Debug.DrawRay(this.gameObject.transform.position,Vector3.forward);
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
