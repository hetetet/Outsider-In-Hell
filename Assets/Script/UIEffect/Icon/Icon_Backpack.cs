using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using TMPro;
using UnityEngine.Localization.Settings;

public class Icon_Backpack : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler
{
    Image img;
    [SerializeField] Canvas TopUiCanvas;
    [SerializeField] public GameObject ItemArea;
    [SerializeField] Button Exit;
    public static Icon_Backpack Instance;
    //public static List<Item> Items = new List<Item>();
    [SerializeField] Transform Elements;
    [SerializeField] GameObject InventoryItem; //prefab
    [SerializeField] Image NewMark;
    [SerializeField] GameObject UseBtn;
    [SerializeField] Sprite[] UsingMark; //0: mask 1: necklace 2: weapon
    bool isDisabled=false;
    bool isAskToUse = false;

    public static Item Mask=null;
    public static Item Necklace =null;
    public static Item Weapon = null;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
        NewMark.gameObject.SetActive(false);
        img = GetComponent<Image>();
        Exit.onClick.AddListener(hideBackPack);
    }
    private void Start()
    {
        ItemArea.transform.DOScale(new Vector2(0, 0), 0.01f);
    }

    void hideBackPack()
    {
        UIEffect.Instance.Fade(0, 0.4f);
        Time.timeScale = 1;
        ItemArea.transform.DOScale(new Vector2(1.1f, 1.1f), 0.1f).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            ItemArea.transform.DOScale(new Vector2(0, 0), 0.25f).SetEase(Ease.InQuad).OnComplete(() =>
            {
                ItemArea.SetActive(false);
                Icon_Settings.Instance.EnableSettingIcon();
                Icon_Mission.Instance.EnableIcon();
                Icon_Help.Instance.EnableIcon();
            });
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isDisabled)
            return;
        img.transform.DOScale(new Vector3(1.2f, 1.2f, 1), 0.3f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isDisabled)
            return;
        img.transform.DOScale(new Vector3(1f, 1f, 1), 0.3f);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (isDisabled)
            return;
        else if (ItemArea.activeSelf)
        {
            hideBackPack();
            return;
        }
        ItemArea.SetActive(true);
        Icon_Settings.Instance.DisableSettingIcon();
        Icon_Mission.Instance.DisableIcon();
        Icon_Help.Instance.DisableIcon();

        NewMark.gameObject.SetActive(false);
        ListItems();
        UIEffect.afterEffect = () =>
        {
            Time.timeScale = 0;
        };
        UIEffect.Instance.enableCanvas(20);
        UIEffect.Instance.setColor(1, 1, 1, 0);
        UIEffect.Instance.Fade(0.5f, 0.4f);
        ItemArea.transform.DOScale(new Vector2(1.1f, 1.1f), 0.25f).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            ItemArea.transform.DOScale(new Vector2(1, 1), 0.1f).SetEase(Ease.InQuad);
        });       
    }
    public void setNewMark()
    {
        NewMark.gameObject.SetActive(true);
    }

    public void ListItems()
    {
        foreach(Transform item in Elements)
        {
            Destroy(item.gameObject);
        }
        foreach(var item in BackpackManager.Items)
        {
            GameObject obj = Instantiate(InventoryItem, Elements);
            var itemIcon=obj.transform.Find("ItemIcon").GetComponent<Image>();
            var itemName=obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
            var itemNum = obj.transform.Find("ItemNum").GetComponent<TextMeshProUGUI>();
            var isUsing=obj.transform.Find("IsUsing").GetComponent<Image>();
            Button itemBtn = obj.transform.GetComponent<Button>();

            string localizedItemName= LocalizationSettings.StringDatabase.GetLocalizedString("Item", item.key);
            itemName.text = localizedItemName;
            itemNum.text = item.number.ToString();
            itemIcon.sprite = item.picture;

            //현재 착용중인 가면, 목장식, 아이템 표시
            if (Mask!=null && Mask.name == item.name) 
            {
                isUsing.gameObject.SetActive(true);
                isUsing.sprite = UsingMark[0];
            }else if (Necklace != null && Necklace.name == item.name)
            {
                isUsing.gameObject.SetActive(true);
                isUsing.sprite = UsingMark[1];
            }
            else if (Weapon != null && Weapon.name == item.name)
            {
                isUsing.gameObject.SetActive(true);
                isUsing.sprite = UsingMark[2];
            }
            else
                isUsing.gameObject.SetActive(false);

            itemBtn.onClick.AddListener(()=> {
                Debug.Log("itemName: " + item.name + ", itemType:" + item.type + ", itemCount" + item.number);

                if (item.type == 0)
                    return;
                else if (10 <= item.type && item.type <= 19)
                {
                    if (item.type == 11)//마스크
                    {
                        //아무것도 안 쓴 경우
                        //이미 해당 가면을 쓴 경우                     
                        if (Mask!=null)
                        {
                            Transform PrevMask = PlayerBehavior.Instance.MaskArea.transform.GetChild(0);
                            Destroy(PrevMask.gameObject);
                            if (PrevMask.name == item.name)
                            {
                                Mask = null;
                                ListItems();
                                return;
                            }
                        }                       
                        Mask = item;
                        var NewMask=Instantiate(item.itemobj, PlayerBehavior.Instance.MaskArea);
                        NewMask.name = item.name;
                        ListItems();
                    }                      
                }
                else if (20 <= item.type && item.type <= 29)
                {

                }
            });
        }
    }

    public void OnClickItem()
    {

    }

    public void DisableIcon()
    {
        isDisabled = true;
        ItemArea.SetActive(false);
        gameObject.GetComponent<ButtonBehavior>().enabled = false;
        img.color = new Color(0.5f, 0.5f, 0.5f);
    }
    public void EnableIcon()
    {
        isDisabled =false;
        gameObject.GetComponent<ButtonBehavior>().enabled = true;
        img.color = Color.white;
    }
}
