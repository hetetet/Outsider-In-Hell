using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovingPlatformBehavior : MonoBehaviour
{
    [SerializeField] private float StartXpos;
    [SerializeField] private float StartYpos;
    [SerializeField] private float EndXpos;
    [SerializeField] private float EndYpos;
    [SerializeField] private float speed;
    float currentTime = 0;
    bool reverse = false;
    Vector3 startpos;
    Vector3 endpos;

    void Start()
    {
        startpos = new Vector3(StartXpos, StartYpos, transform.position.z);
        endpos = new Vector3(EndXpos, EndYpos, transform.position.z);
    }
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= speed)
        {
            currentTime = 0;
            reverse = !reverse;
        }
            
        if(!reverse)
            this.transform.position = Vector3.Lerp(startpos, endpos, currentTime/speed);
        else
            this.transform.position = Vector3.Lerp(endpos, startpos, currentTime / speed);

    }
}
