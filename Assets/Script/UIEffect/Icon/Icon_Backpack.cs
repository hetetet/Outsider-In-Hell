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
    [SerializeField] public GameObject ItemArea;
    [SerializeField] Button Exit;
    public static Icon_Backpack Instance;
    //public static List<Item> Items = new List<Item>();
    [SerializeField] Transform Elements;
    [SerializeField] GameObject InventoryItem; //prefab
    [SerializeField] Image NewMark;
    bool isDisabled=false;

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
        UIEffect.Instance.enableCanvas(10);
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
            Button itemBtn = obj.transform.GetComponent<Button>();

            string localizedItemName= LocalizationSettings.StringDatabase.GetLocalizedString("Item", item.key);
            itemName.text = localizedItemName;
            itemNum.text = item.number.ToString();
            itemIcon.sprite = item.picture;
            itemBtn.onClick.AddListener(()=> {
                Debug.Log("itemName: " + item.name + ", itemType:" + item.type + ", itemCount" + item.number);

            });
        }
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
