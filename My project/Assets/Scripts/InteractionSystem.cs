using TMPro;
using UnityEngine;

public class InteractionSystem : MonoBehaviour
{
    public float InteractionDistance;
    public Camera PlayerCamera;
    public TMP_Text InteractionUI;

    GameObject InteractionObject;

    private void Update()
    {
        GetInteractionObject();

        //When Left Mouse Button Pressed
        if (Input.GetMouseButtonDown(0) && !PlayerMovement.Instance.PauseInput)
            Interact();
    }

    void Interact()
    {
        //Interact With Interaction Object
        if (InteractionObject)
        {
            if (InteractionObject.GetComponent<SupplyObject>())
                InteractionObject.GetComponent<SupplyObject>().Interact();

            if (InteractionObject.GetComponent<ItemPickup>())
            {
                InventoryManager.instance.AddItem(InteractionObject.GetComponent<ItemPickup>().Item, 1f, InventoryManager.instance.Hotbar);
                Destroy(InteractionObject);
            }
                
        }
    }

    void GetInteractionObject()
    {
        InteractionObject = null;

        //Shoot ray from player camera to see if can Interact
        RaycastHit hit;
        if (Physics.Raycast(PlayerCamera.transform.position, PlayerCamera.transform.forward, out hit, InteractionDistance))
        {
            if (hit.transform.GetComponent<InteractionObject>())
            {
                InteractionObject = hit.transform.gameObject;
                InteractionUI.text = InteractionObject.GetComponent<InteractionObject>().InteractionText;

                return;
            }
        }

        InteractionUI.text = "";
    }
}