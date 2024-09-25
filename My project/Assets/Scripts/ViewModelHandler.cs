using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewModelHandler : MonoBehaviour
{
    GameObject CurrentViewModel;
    ItemClass CurrentItem;

    private void Update()
    {
        if (InventoryManager.instance.SelectedItem != CurrentItem)
        {
            CurrentItem = InventoryManager.instance.SelectedItem;

            if (CurrentViewModel)
                Destroy(CurrentViewModel);

            if (InventoryManager.instance.SelectedItem && InventoryManager.instance.SelectedItem.GetTool() && InventoryManager.instance.SelectedItem.GetTool().ViewModel)
            {
                CurrentViewModel = Instantiate(InventoryManager.instance.SelectedItem.GetTool().ViewModel, transform);
                CurrentViewModel.transform.localPosition = Vector3.zero;
                CurrentViewModel.transform.localRotation = Quaternion.identity;
            }
        }
    }
}
