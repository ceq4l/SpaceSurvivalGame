using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerClickHandler
{
    public enum SlotType
    {
        Inventory,
        Hotbar
    }

    public SlotType slotType;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (Input.GetKey(KeyCode.LeftShift))
                InventoryManager.instance.QuickMove(gameObject);
            else
                InventoryManager.instance.MoveItem(gameObject);
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (!InventoryManager.instance.ItemBeingMoved.Item)
                InventoryManager.instance.SplitStack(gameObject);
            else if (InventoryManager.instance.ItemBeingMoved.Item)
                InventoryManager.instance.DropOne(gameObject);
        }
    }
}
