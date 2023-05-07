using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
public class HPmanager : MonoBehaviour
{
    Image HPfill;
    TextMeshProUGUI hpValue;
    TextMeshProUGUI ExtraHP;
    private Color col= new Color((float)241/255, (float)220 /255, (float)33 /255, 1);
    public static HPmanager Instance;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        HPfill=transform.Find("IconLayout/Fill").GetComponent<Image>();
        hpValue=transform.Find("IconLayout/Value").GetComponent<TextMeshProUGUI>();
        hpValue.text=PlayerBehavior.currentHP.ToString()+"/"+PlayerBehavior.maxHP.ToString();
        ExtraHP= transform.Find("IconLayout/ExtraHP").GetComponent<TextMeshProUGUI>();
        ExtraHP.color = new Color(col.r, col.g, col.b, 0);
    }

    public void showExtraHP()
    {   
        Sequence show = DOTween.Sequence();
        ExtraHP.text="+"+(PlayerBehavior.maxHP -100).ToString();

        float y = ExtraHP.rectTransform.anchoredPosition.y;
        show.Insert(0, ExtraHP.rectTransform.DOAnchorPosY(y + 50, 3));
        show.Insert(0, ExtraHP.DOColor(col, 1));
        show.Insert(2, ExtraHP.DOColor(new Color(col.r, col.g, col.b, 0), 1));
    }

    public void showHpBar(int hp)
    {
        HPfill.transform.DOScaleX(((float)hp / (float)PlayerBehavior.maxHP) * HPfill.transform.localScale.x, 0.5f);
        Debug.Log("hpfill length: " + (((float)hp / (float)PlayerBehavior.maxHP) * HPfill.transform.localScale.x).ToString());
    }
}
