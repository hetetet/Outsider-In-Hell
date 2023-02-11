using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;
using DG.Tweening;
public class CharImg : MonoBehaviour
{
    [SerializeField] Image charImg;
    public SerializableDictionary<string, Sprite[]> CharDict;

    [YarnCommand("set_sprite")]
    public void setSprite(string charname, int spritenum)
    {
        if(charname == "Soyeon")
        {
            charImg.rectTransform.anchoredPosition = new Vector2(910, 510);
        }
        else
        {
            charImg.rectTransform.anchoredPosition = new Vector2(-290, 510);
        }
        charImg.sprite = CharDict[charname][spritenum];
    }

    [YarnCommand("charjump")]
    public void CharJump()
    {
        float origin_x = charImg.rectTransform.anchoredPosition.x;
        float origin_y = charImg.rectTransform.anchoredPosition.y;
        transform.DOLocalMove(new Vector2(origin_x, origin_y + 30), 0.3f).SetEase(Ease.InQuad).SetLoops(2,LoopType.Yoyo);
    }

    [YarnCommand("char_stagger")]
    public void CharStagger()
    {
        float origin_x = charImg.rectTransform.anchoredPosition.x;
        float origin_y = charImg.rectTransform.anchoredPosition.y;

        Sequence stagSeq = DOTween.Sequence().SetAutoKill(false);
        stagSeq.Insert(0, transform.DOLocalRotate(new Vector3(0, 0, -20), 0.5f));
        stagSeq.Insert(0, transform.DOLocalMove(new Vector2(origin_x + 900, origin_y - 300), 0.5f));
        stagSeq.Restart();
    }
}
