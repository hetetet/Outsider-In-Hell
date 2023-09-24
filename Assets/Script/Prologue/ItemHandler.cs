using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Yarn.Unity;

public class ItemHandler : MonoBehaviour
{
    public DialogueRunner myDialogueRunner;
    [SerializeField] GameObject itemList;
    [SerializeField] Button getOutBtn;
    [SerializeField] Icon_Mission iconMission;
    [SerializeField] Mission deleteMission;
    [SerializeField] InMemoryVariableStorage variableStorage;
    UIEffect uiEffect;
    public static bool canClickItem = false;
    private bool isFindCompleted;

    public static ItemHandler Instance;

    public void setCanClickItem(bool canclick)
    {
        Debug.Log("set canclick to " + canclick.ToString());
        canClickItem = canclick;
        if (canclick)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject.GetComponent<FindItem>()!=null)
            {
                GameManager.setCursorToPointer();
            }
        }
        return;
    }
    private void Awake()
    {
        Instance = this;
        uiEffect = FindObjectOfType<UIEffect>();
    }

    private void Start()
    {
        getOutBtn.onClick.AddListener(() =>
        {
            UIEffect.Instance.enableCanvas(999);
            UIEffect.Instance.setColor(0, 0, 0, 0);
            UIEffect.Instance.Fade(1, 2, "Prologue2");
        });
    }
    public IEnumerator showFoundItem(Item item, Image itemImg)
    {
        BackpackManager.add(item);
        itemImg.sprite = item.picture;
        variableStorage.SetValue("$found_obj", item.name);
        myDialogueRunner.StartDialogue("FindObject");
        itemImg.transform.LeanScale(Vector2.one, 0.5f);
        yield return new WaitForSeconds(1.8f);
        itemImg.transform.LeanScale(Vector2.zero, 0.5f).setEaseInBack().setOnComplete(() =>
        {
            Instance.setCanClickItem(true);
            if (BackpackManager.Items.Count==4) //아이템 4개 모두 찾았을 경우
            {
                getOutBtn.transform.LeanMoveLocal(new Vector2(0, -10), 0.3f).setEaseOutQuad().setOnComplete(() =>
                {
                    Icon_Mission.Instance.deleteMission(deleteMission);
                    getOutBtn.transform.LeanMoveLocal(new Vector2(0, 0), 0.3f).setEaseInQuad();
                    Instance.setCanClickItem(false);
                });
            }
        });
    }

    private void Update()
    {

    }
}
