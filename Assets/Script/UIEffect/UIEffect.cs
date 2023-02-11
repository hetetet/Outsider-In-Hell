using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Yarn.Unity;
using DG.Tweening;
public class UIEffect : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Canvas EffectCanvas;
    [SerializeField] Image image;
    public static UIEffect Instance=null;
    private bool onColCng=false;
    public delegate void Del();
    public static Del afterEffect;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(this);

        afterEffect = ()=>{ };
    }

    [YarnCommand("setActive")]
    public void enableCanvas(int sortOrder)
    {
        EffectCanvas.enabled = true;
        EffectCanvas.sortingOrder = sortOrder;
    }

    [YarnCommand("setColor")]
    public void setColor(float r,float g,float b, float a=1)
    {
        image.color =  new Color(r,g,b,a);
    }
    public Color getColor()
    {
        return image.color;
    }

    public bool isEnabled()
    {
        return EffectCanvas.enabled;
    }

    public bool onColorChange()
    {
        return onColCng;
    }

    [YarnCommand("Fade")]
    public void Fade(float end, float fadeTime, string scene = "")
    {
        EffectCanvas.enabled = true;
        onColCng = true;
        Color endcolor = image.color;
        endcolor.a = end;
        image.DOColor(endcolor, fadeTime).OnComplete(() =>
        {
            if (image.color.a == 0)
            {
                Debug.Log("color alpha is 0");
                EffectCanvas.sortingOrder = 0;
                EffectCanvas.enabled = false;
            }

            if (scene.Length > 0)
            {
                SceneManager.LoadScene(scene);
            }
            onColCng = false;
            afterEffect();
            afterEffect = () => { };
        });
    }

    public void ChangeColor(Color color, float fadeTime)
    {
        onColCng = true;
        image.DOColor(color, fadeTime).OnComplete(() =>
        {
            onColCng = false;
            afterEffect();
            afterEffect = () => { };
        });
    }
}
