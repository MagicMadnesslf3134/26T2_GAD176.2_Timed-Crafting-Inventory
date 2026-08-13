using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RecipeButton : MonoBehaviour
{
    public TextMeshProUGUI buttonText;
    private Recipe recipe;
    private CraftingUI craftingUI;

    public void Setup(Recipe recipe, CraftingUI ui)
    {
        this.recipe = recipe;
        this.craftingUI = ui;
        if (buttonText != null)
            buttonText.text = recipe.outputItem.itemName;
    }

    public void OnClick()
    {
        if (craftingUI != null && recipe != null)
            craftingUI.SelectRecipe(recipe);
        Debug.Log("RecipeButton clicked: " + recipe.outputItem.itemName);
    }
}