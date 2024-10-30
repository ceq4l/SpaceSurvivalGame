using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RecipeHandler : MonoBehaviour
{
    public RecipeClass Recipe;

    public GameObject Ingredient;
    public GameObject IngredientHolder;

    private void Start()
    {
        transform.GetChild(0).GetComponent<TMP_Text>().text = Recipe.name;

        for (int i = 0; i < Recipe.Ingredients.Count; i++)
        {
            GameObject NewIngredientText = Instantiate(Ingredient, IngredientHolder.transform);
            NewIngredientText.GetComponent<TMP_Text>().text = Recipe.Ingredients[i].Item.name + " x" + Recipe.Ingredients[i].Amount;
        }
    }

    private void Update()
    {
        for (int i = 0; i < Recipe.Ingredients.Count; i++)
        {
            if (InventoryManager.instance.Contains(Recipe.Ingredients[i].Item, Recipe.Ingredients[i].Amount))
                IngredientHolder.transform.GetChild(i).GetComponent<TMP_Text>().color = Color.green;
            else
                IngredientHolder.transform.GetChild(i).GetComponent<TMP_Text>().color = Color.red;
        }
    }

    public void Craft()
    {
        Recipe.Craft();
    }
}
