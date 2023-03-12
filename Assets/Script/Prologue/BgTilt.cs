using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class BgTilt : MonoBehaviour
{
    bool prevent_motion_sickness = false;
    bool isTilting = false;
    bool isfainted = false;
    float faintElapse = 0;
    int elapsedTime=0;
    float bgAngle = 0;

    [SerializeField] GameObject Guide;
    [SerializeField] TextMeshProUGUI TimeTMP;
    Image LeftArrow;
    Image RightArrow;
    Rigidbody2D bgRigid;
    Material blurMat;

    [SerializeField] Mission fainthold;
    // Start is called before the first frame update
    void Start()
    {
        TimeTMP.text = "00:00";
        bgRigid = GetComponent<Rigidbody2D>();
        
        LeftArrow =Guide.transform.Find("LeftArrow").GetComponent<Image>();
        RightArrow=Guide.transform.Find("RightArrow").GetComponent <Image>();

        blurMat = GetComponentInChildren<Renderer>().material; //.GetFloat("BlurAmount");
        blurMat.SetFloat("_isBlurring", 0);
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftArrow) && !isfainted && TimeTMP.gameObject.activeSelf)
            bgRigid.AddTorque(-10 * Time.fixedDeltaTime, ForceMode2D.Impulse);
        else if (Input.GetKey(KeyCode.RightArrow) && !isfainted && TimeTMP.gameObject.activeSelf)
            bgRigid.AddTorque(10 * Time.fixedDeltaTime, ForceMode2D.Impulse);

        //if (isTilting && elapsedTime % 60 == 0)
        //{
     
        //}
    }

    // Update is called once per frame
    void Update()
    {
        bgAngle = this.transform.eulerAngles.z;
        if (Input.GetKey(KeyCode.LeftArrow) && !isfainted)
            LeftArrow.transform.DOScale(new Vector2(1.1f, 1.1f), 0.1f);
        else if (Input.GetKey(KeyCode.RightArrow) && !isfainted)
            RightArrow.transform.DOScale(new Vector2(1.1f, 1.1f), 0.1f);

        if (Input.GetKeyUp(KeyCode.LeftArrow) && !isfainted)
            LeftArrow.transform.DOScale(new Vector2(1, 1), 0.1f);
        else if (Input.GetKeyUp(KeyCode.RightArrow) && !isfainted)
            RightArrow.transform.DOScale(new Vector2(1, 1), 0.1f);

        if (bgAngle >= 60 && bgAngle <= 300)
        {
            isfainted = true;
        }

        if (bgAngle >= 90 && bgAngle <= 270 && bgRigid.angularVelocity != 0)
        {
            bgRigid.angularVelocity = 0;
            StopAllCoroutines();
            StartCoroutine(visionScatter());
            Debug.Log("completely fell down, elapsed time: " + (elapsedTime / 100).ToString() + " second");

            PlayerBehavior.maxHP += (int)(elapsedTime / 100);
            PlayerBehavior.currentHP=PlayerBehavior.maxHP;

            Icon_Mission.Instance.deleteMission(fainthold);

            UIEffect.Instance.enableCanvas(999);
            UIEffect.Instance.setColor(0, 0, 0, 0);
            UIEffect.Instance.Fade(1, 2, "Main_starter");
        }
    }
    public void startTilt()
    {
        blurMat.SetFloat("_isBlurring", 1);
        TimeTMP.gameObject.SetActive(true);
        isTilting=true;
        StartCoroutine(tilt());
        StartCoroutine(countTime());
    }
    IEnumerator countTime()
    {
        yield return new WaitForSeconds(0.01f);
        elapsedTime += 1;
        TimeTMP.text = (elapsedTime / 100).ToString("D2") + ":" + (elapsedTime % 100).ToString("D2");
        StartCoroutine(countTime());
    }
    IEnumerator tilt()
    {
        int[] dir = { -1, 1 };
        int dirindex = Random.Range(0, 2);
        bgRigid.AddTorque(100 * dir[dirindex] * Time.fixedDeltaTime, ForceMode2D.Impulse); 
        yield return new WaitForSeconds(1f);
        StartCoroutine(tilt());
    }

    IEnumerator visionScatter()
    {
        for(int i = 0; i < 50; i++)
        {
            faintElapse += 0.001f;
            blurMat.SetFloat("_IncreaseBlur", faintElapse);
            yield return new WaitForSeconds(0.02f);
        }
    }
}
