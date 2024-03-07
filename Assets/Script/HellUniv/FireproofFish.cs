using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FireproofFish : MonoBehaviour
{
    [SerializeField] float startTime=0;
    Sequence hop;
    float xpos;
    float ypos;
    // Start is called before the first frame update

    private void Awake()
    {
        xpos = this.transform.position.x; //À½¼ö
        ypos = this.transform.position.y;
        Debug.Log(this.gameObject.name+" - xpos:"+xpos+", ypos:"+ypos);
    }
    void Start()
    {
        Debug.Log(this.gameObject.name + " - xpos + 2:" + xpos + 2 + ", ypos + 3:" + ypos + 3);
        this.transform.Rotate(0,0,30f,Space.Self);
        //DOTween.Sequence().Append(this.transform.DOLocalMove(new Vector3(xpos+2, ypos+2), 1f)).Append(this.transform.DOLocalMove(new Vector3(xpos + 4, ypos), 1f)).SetLoops(-1);
        Invoke("FishJump", startTime);
    }

    void FishJump()
    {
        DOTween.Sequence()
            .Insert(0, this.transform.DOMove(new Vector3(xpos + 2, ypos + 3), 0.7f).SetEase(Ease.OutCubic))
            .Insert(0, this.transform.DORotate(new Vector3(0, 0, 0), 1f).SetEase(Ease.OutCubic))
            .Insert(1f, this.transform.DOMove(new Vector3(xpos + 4, ypos), 1f).SetEase(Ease.InCubic))
            .Insert(1f, this.transform.DORotate(new Vector3(0, 0, -30), 1f).SetEase(Ease.InCubic))
            .SetLoops(-1);
    }
}
