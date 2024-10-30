using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/CraftingRecipe", fileName = "CraftingRecipe")]
public class RecipeClass : ScriptableObject
{
    public List<Ingredient> Ingredients = new List<Ingredient>();
    public List<Ingredient> Output = new List<Ingredient>();

    bool CanCraft()
    {
        int Check = 0;

        for (int i = 0; i < Ingredients.Count; i++)
        {
            if (InventoryManager.instance.Contains(Ingredients[i].Item, Ingredients[i].Amount))
                Check++;
        }

        return Check == Ingredients.Count;
    }

    public void Craft()
    {
        if (CanCraft())
        {
            for (int i = 0; i < Ingredients.Count; i++)
                InventoryManager.instance.RemoveItem(Ingredients[i].Item, Ingredients[i].Amount);

            for (int i = 0; i < Output.Count; i++)
                InventoryManager.instance.AddItem(Output[i].Item, Output[i].Amount, InventoryManager.instance.Hotbar);
        }
    }
}

[System.Serializable]
public class Ingredient
{
    public ItemClass Item;
    public float Amount;
}