using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Yarn.Unity;
public class FindItem : MonoBehaviour
{
    DialogueRunner myDialogueRunner;
    [SerializeField] GameObject[] VisibleItems; //�������� �������� ���� ��� ���� �ִ´�
    [SerializeField] Item[] Items; //ã�ƾ� �ϴ� ��� �����۵�
    [SerializeField] AudioClip open;
    [SerializeField] AudioClip close;

    [SerializeField] Image itemImg;
    [SerializeField] ItemHandler itemHandler;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        if(VisibleItems.Length > 0)
            VisibleItems[0].SetActive(true);

        anim = GetComponent<Animator>();
        itemImg.transform.localScale = Vector2.zero;
        myDialogueRunner = itemHandler.myDialogueRunner;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (SettingManager.Instance.isSetAreaActive())
            return;

        if (ItemHandler.canClickItem && !myDialogueRunner.IsDialogueRunning && !UIEffect.Instance.isEnabled())
            GameManager.setCursorToSelect();
    }
    private void OnMouseUp()
    {
        if (SettingManager.Instance.isSetAreaActive())
            return;

        if (ItemHandler.canClickItem && !myDialogueRunner.IsDialogueRunning && !UIEffect.Instance.isEnabled()) //������ Ŭ�� �����ϰ�, ��ȭ�� ���������� �ʰ�, uiEffect�� disabled �Ǿ��� ��
        {
            GameManager.setCursorToPointer();
            anim.SetBool("isOpened", true);
            SoundManager.Instance.playEffectSound(open);
            myDialogueRunner.onDialogueComplete.RemoveAllListeners();
            myDialogueRunner.onDialogueComplete.AddListener(close_object); //dialog�� ������ close_object �Լ� ����
            if (Items.Length == 0)
            {
                myDialogueRunner.StartDialogue("NoObject");                
            }
            else
            {
                Debug.Log("itemsprite length: " + Items.Length.ToString() + "itemsprite 0th index: " + Items[0]);             
                ItemHandler.Instance.setCanClickItem(false);
                StartCoroutine(itemHandler.showFoundItem(Items[0], itemImg));
            }
            return;
        }
    }
    private void OnMouseEnter()
    {
        if(ItemHandler.canClickItem && !WholeIconHandler.Instance.isIconActive())
            GameManager.setCursorToPointer();
    }
    private void OnMouseExit()
    {
        if (ItemHandler.canClickItem && !WholeIconHandler.Instance.isIconActive())
            GameManager.setCursorToArrow();
    }



    [YarnCommand("get_objname")]
    string get_objname()
    {
        return this.gameObject.name;
    }

    void close_object()
    {
        Debug.Log("close object");
        anim.SetBool("isOpened", false);
        SoundManager.Instance.playEffectSound(close);
        if (VisibleItems.Length > 0)
            VisibleItems[0].SetActive(false);
        if(Items.Length > 0)
        {
            Items = new Item[0];
        }
    } 
}
