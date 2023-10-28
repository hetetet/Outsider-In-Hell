using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableGround : MonoBehaviour
{
    public ParticleSystem ps;
    // Start is called before the first frame update
    void Start()
    {
        ps.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "drill")
        {
            Debug.Log("드릴에 맞았습니다.");
            StartCoroutine("Codestroy");
        }
    }

    IEnumerator Codestroy()
    {
        Debug.Log("codestroy");
        ps.Play();
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }
}
