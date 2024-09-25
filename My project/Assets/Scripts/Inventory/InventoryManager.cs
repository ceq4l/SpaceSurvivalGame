using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    List<InventorySlot> Inventory = new List<InventorySlot>();
    public List<InventorySlot> Hotbar = new List<InventorySlot>();

    public List<StarterItem> StarterItems = new List<StarterItem>();

    int SelectionIndex = 0;

    [Header("Slot Holders UI")]
    public Transform InventorySlotHolder;
    public Transform HotbarSlotHolder;
    public Transform DisplayHotbar;

    [Header("UI References")]
    public GameObject ItemUIPrefab;
    public Transform MovingItemUI;
    public Transform HotbarSelector;

    [Header("Item Values")]
    public ItemClass SelectedItem;

    [HideInInspector]
    public MovingItem ItemBeingMoved;

    private void Awake() { instance = this; }

    private void Start()
    {
        for (int i = 0; i < InventorySlotHolder.childCount; i++)
        {
            InventorySlot newSlot = new InventorySlot();
            newSlot.SlotObject = InventorySlotHolder.GetChild(i).gameObject;

            Inventory.Add(newSlot);
        }

        for (int i = 0; i < HotbarSlotHolder.childCount; i++)
        {
            InventorySlot newSlot = new InventorySlot();
            newSlot.SlotObject = HotbarSlotHolder.GetChild(i).gameObject;

            Hotbar.Add(newSlot);
        }

        for (int i = 0; i < StarterItems.Count; i++)
        {
            AddItem(StarterItems[i].Item, StarterItems[i].Amount, Inventory);
        }

        UpdateInventoryUI();
    }

    private void Update()
    {
        MovingItemUI.position = Input.mousePosition;

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
            SelectionIndex += 1;
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
            SelectionIndex -= 1;

        SelectionIndex = Mathf.Clamp(SelectionIndex, -1, Hotbar.Count);

        if (SelectionIndex < 0)
            SelectionIndex = Hotbar.Count - 1;

        if (SelectionIndex == Hotbar.Count)
            SelectionIndex = 0;

        HotbarSelector.position = Hotbar[SelectionIndex].SlotObject.transform.position;
        SelectedItem = Hotbar[SelectionIndex].Item;
    }

    InventorySlot FindSlot(GameObject slot)
    {
        for (int i = 0; i < Inventory.Count; i++)
        {
            if (Inventory[i].SlotObject == slot)
                return Inventory[i];
        }

        for (int i = 0; i < Hotbar.Count; i++)
        {
            if (Hotbar[i].SlotObject == slot)
                return Hotbar[i];
        }

        return null;
    }

    #region UI Events
    void UpdateInventoryUI()
    {
        UIUpdate(Inventory);
        UIUpdate(Hotbar);

        for (int i = 0; i < Hotbar.Count; i++)
        {
            InventorySlot Slot = Hotbar[i];
            Transform SlotObject = DisplayHotbar.GetChild(i);

            if (SlotObject.childCount > 0)
                Destroy(SlotObject.GetChild(0).gameObject);

            if (Slot.Item)
            {
                Transform NewItemUI = Instantiate(ItemUIPrefab, SlotObject).transform;

                NewItemUI.GetComponent<Image>().sprite = Slot.Item.ItemIcon;

                if (Slot.Item.ItemIcon)
                {
                    NewItemUI.GetChild(0).GetComponent<TMP_Text>().text = "";
                    NewItemUI.GetChild(1).GetComponent<TMP_Text>().color = Color.white;
                }
                else
                {
                    NewItemUI.GetChild(0).GetComponent<TMP_Text>().text = Slot.Item.name;
                    NewItemUI.GetChild(1).GetComponent<TMP_Text>().color = Color.black;
                }

                NewItemUI.GetChild(1).GetComponent<TMP_Text>().text = Slot.Amount.ToString();
            }
        }

        if (ItemBeingMoved.Item)
        {
            if (ItemBeingMoved.Item.ItemIcon)
            {
                MovingItemUI.GetChild(0).GetComponent<TMP_Text>().text = "";
                MovingItemUI.GetChild(1).GetComponent<TMP_Text>().color = Color.white;
            }
            else
            {
                MovingItemUI.GetChild(0).GetComponent<TMP_Text>().text = ItemBeingMoved.Item.name;
                MovingItemUI.GetChild(1).GetComponent<TMP_Text>().color = Color.black;
            }

            MovingItemUI.GetComponent<Image>().sprite = ItemBeingMoved.Item.ItemIcon;
            MovingItemUI.GetChild(1).GetComponent<TMP_Text>().text = ItemBeingMoved.Amount.ToString();

            MovingItemUI.gameObject.SetActive(true);
        }
        else
            MovingItemUI.gameObject.SetActive(false);
    }
    void UIUpdate(List<InventorySlot> ItemList)
    {
        for (int i = 0; i < ItemList.Count; i++)
        {
            InventorySlot Slot = ItemList[i];
            Transform SlotObject = ItemList[i].SlotObject.transform;

            if (SlotObject.childCount > 0)
                Destroy(SlotObject.GetChild(0).gameObject);

            if (Slot.Item)
            {
                Transform NewItemUI = Instantiate(ItemUIPrefab, SlotObject).transform;

                NewItemUI.GetComponent<Image>().sprite = Slot.Item.ItemIcon;

                if (Slot.Item.ItemIcon)
                {
                    NewItemUI.GetChild(0).GetComponent<TMP_Text>().text = "";
                    NewItemUI.GetChild(1).GetComponent<TMP_Text>().color = Color.white;
                }
                else
                {
                    NewItemUI.GetChild(0).GetComponent<TMP_Text>().text = Slot.Item.name;
                    NewItemUI.GetChild(1).GetComponent<TMP_Text>().color = Color.black;
                }

                NewItemUI.GetChild(1).GetComponent<TMP_Text>().text = Slot.Amount.ToString();
            }
        }
    }
    #endregion

    #region Inventory Management Events
    public void AddItem(ItemClass item, float amount, List<InventorySlot> List)
    {
        float AmountToAdd = amount;

        for (int i = 0; i < List.Count; i++)
        {
            InventorySlot slot = List[i];

            if (AmountToAdd > 0 && slot.Item == item)
            {
                if (AmountToAdd <= slot.Item.MaxStackSize - slot.Amount)
                {
                    slot.Amount += AmountToAdd;
                    AmountToAdd = 0;
                }
                else
                {
                    AmountToAdd -= slot.Item.MaxStackSize - slot.Amount;
                    slot.Amount = item.MaxStackSize;
                }
            }
        }

        for (int i = 0; i < List.Count; i++)
        {
            InventorySlot slot = List[i];

            if (AmountToAdd > 0 && !slot.Item)
            {
                if (AmountToAdd <= item.MaxStackSize)
                {
                    slot.Item = item;
                    slot.Amount += AmountToAdd;

                    AmountToAdd = 0;
                }
                else
                {
                    slot.Item = item;
                    slot.Amount = item.MaxStackSize;

                    AmountToAdd -= item.MaxStackSize;
                }
            }
        }

        UpdateInventoryUI();

        if (AmountToAdd > 0)
            Debug.Log("Drop " + item.name + " x" + AmountToAdd);
    }
    void ClearSlot(InventorySlot slot)
    {
        slot.Item = null;
        slot.Amount = 0;
        slot.Durability = 0;
    }
    void ClearMovingItem()
    {
        ItemBeingMoved.Item = null;
        ItemBeingMoved.Amount = 0;
        ItemBeingMoved.Durability = 0;
    }
    #endregion

    #region Move Events
    public void MoveItem(GameObject SlotUI)
    {
        InventorySlot Slot = FindSlot(SlotUI);

        if (!ItemBeingMoved.Item)
        {
            ItemBeingMoved.Item = Slot.Item;
            ItemBeingMoved.Amount = Slot.Amount;
            ItemBeingMoved.Durability = Slot.Durability;

            ClearSlot(Slot);
        }
        else if (ItemBeingMoved.Item)
        {
            if (!Slot.Item)
            {
                Slot.Item = ItemBeingMoved.Item;
                Slot.Amount = ItemBeingMoved.Amount;
                Slot.Durability = ItemBeingMoved.Durability;

                ClearMovingItem();
            }
            else if (Slot.Item != ItemBeingMoved.Item)
            {
                MovingItem ItemToSwap = new MovingItem();
                ItemToSwap.Item = ItemBeingMoved.Item;
                ItemToSwap.Amount = ItemBeingMoved.Amount;
                ItemToSwap.Durability = ItemBeingMoved.Durability;

                ItemBeingMoved.Item = Slot.Item;
                ItemBeingMoved.Amount = Slot.Amount;
                ItemBeingMoved.Durability = Slot.Durability;

                Slot.Item = ItemToSwap.Item;
                Slot.Amount = ItemToSwap.Amount;
                Slot.Durability = ItemToSwap.Durability;
            }
            else if (Slot.Item == ItemBeingMoved.Item)
            {
                if (ItemBeingMoved.Amount <= Slot.Item.MaxStackSize - Slot.Amount)
                {
                    Slot.Amount += ItemBeingMoved.Amount;
                    ItemBeingMoved.Amount = 0;

                    ClearMovingItem();
                }
                else if (ItemBeingMoved.Amount > Slot.Item.MaxStackSize - Slot.Amount)
                {
                    float Amount = Slot.Item.MaxStackSize - Slot.Amount;

                    Slot.Amount = Slot.Item.MaxStackSize;
                    ItemBeingMoved.Amount -= Amount;
                }
            }
        }

        UpdateInventoryUI();
    }
    public void SplitStack(GameObject SlotUI)
    {
        InventorySlot Slot = FindSlot(SlotUI);

        if (Slot.Amount > 1)
        {
            ItemBeingMoved.Item = Slot.Item;
            ItemBeingMoved.Amount = Mathf.CeilToInt(Slot.Amount / 2f);

            Slot.Amount = Mathf.FloorToInt(Slot.Amount / 2f);
        }

        UpdateInventoryUI();
    }
    public void DropOne(GameObject SlotUI)
    {
        InventorySlot Slot = FindSlot(SlotUI);

        if (Slot.Item && Slot.Amount != Slot.Item.MaxStackSize && Slot.Item == ItemBeingMoved.Item)
        {
            Slot.Amount += 1;
            ItemBeingMoved.Amount -= 1;
        }
        else if (!Slot.Item)
        {
            Slot.Item = ItemBeingMoved.Item;
            Slot.Amount += 1;
            ItemBeingMoved.Amount -= 1;
        }

        UpdateInventoryUI();
    }
    public void QuickMove(GameObject SlotUI)
    {
        InventorySlot Slot = FindSlot(SlotUI);

        if (SlotUI.GetComponent<Slot>().slotType == global::Slot.SlotType.Inventory)
        {
            AddItem(Slot.Item, Slot.Amount, Hotbar);
            ClearSlot(Slot);
        }
        else if (SlotUI.GetComponent<Slot>().slotType == global::Slot.SlotType.Hotbar)
        {
            AddItem(Slot.Item, Slot.Amount, Inventory);
            ClearSlot(Slot);
        }

        UpdateInventoryUI();
    }
    #endregion
}

[System.Serializable]
public class InventorySlot
{
    public GameObject SlotObject;

    public ItemClass Item;

    public float Amount;
    public float Durability;
}

[System.Serializable]
public class MovingItem
{
    public ItemClass Item;
    public float Amount;
    public float Durability;
}

[System.Serializable]
public class StarterItem
{
    public ItemClass Item;
    public float Amount;
}