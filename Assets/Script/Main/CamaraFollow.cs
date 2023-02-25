using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float speed;

    [SerializeField] Vector2 center;
    [SerializeField] Vector2 size;
    float height;
    float width;
    private void Awake()
    {

    }
    void Start()
    {
        height=Camera.main.orthographicSize;
        width=height*Screen.width/Screen.height;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, size);
    }
    void LateUpdate()
    {
        try
        {
            if (target == null)
            target = GameObject.Find("Soyeon").transform;
            transform.position = Vector3.Lerp(new Vector3(transform.position.x, transform.position.y, -10f), target.position, Time.deltaTime * speed);
            float lx = size.x * 0.5f - width;
            float clampX = Mathf.Clamp(transform.position.x, -lx + center.x, lx + center.x);

            float ly = size.y * 0.5f - height;
            float clampY = Mathf.Clamp(transform.position.y, -ly + center.y, ly + center.y);

            transform.position = new Vector3(clampX, clampY, -10f);
        }
        catch { }
    }
}
