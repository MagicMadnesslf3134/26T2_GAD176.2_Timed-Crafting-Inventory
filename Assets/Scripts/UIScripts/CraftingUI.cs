using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class CraftingUI : MonoBehaviour
{
    [SerializeField] private CraftingSystem craftingSystem;
    [SerializeField] public ItemDatabase itemDatabase;
    [SerializeField] public InventoryUI inventoryUI;

    [SerializeField] public Transform recipeListParent;
    [SerializeField] public GameObject recipeButtonPrefab;

    [SerializeField] public TextMeshProUGUI recipeDetailsText;
    [SerializeField] public Button craftButton;
    [SerializeField] public TextMeshProUGUI messageText;

    private Recipe selectedRecipe;

    void Start()
    {
        PopulateRecipeList();
        if (craftButton != null)
            craftButton.onClick.AddListener(CraftSelectedRecipe);

        if (craftButton != null)
            craftButton.interactable = false;
    }

    void PopulateRecipeList()
    {
        if (itemDatabase == null || recipeButtonPrefab == null || recipeListParent == null)
        {
            Debug.LogWarning("CraftingUI: missing references for PopulateRecipeList.");
            return;
        }

        foreach (Recipe recipe in itemDatabase.allRecipes)
        {
            GameObject buttonObj = Instantiate(recipeButtonPrefab, recipeListParent);
            RecipeButton rb = buttonObj.GetComponent<RecipeButton>();
            if (rb != null)
                rb.Setup(recipe, this);
        }
    }

    public void SelectRecipe(Recipe recipe)
    {
        Debug.Log($"CraftingUI.SelectRecipe: {recipe?.outputItem?.itemName}");
        selectedRecipe = recipe;

        if (recipeDetailsText != null)
        {
            recipeDetailsText.text = "Ingredients:\n";
            foreach (var ingredient in recipe.ingredients)
                recipeDetailsText.text += ingredient.Key.itemName + " x" + ingredient.Value + "\n";
        }

        if (messageText != null)
            messageText.text = "You selected " + recipe.outputItem.itemName;

        if (craftButton != null)
            craftButton.interactable = (craftingSystem != null && craftingSystem.inventory != null && selectedRecipe != null && craftingSystem.inventory.HasIngredients(selectedRecipe));

        RefreshCraftButton();
    }

    public void Initialize(CraftingSystem system)
    {
        craftingSystem = system;
        Debug.Log("CraftingUI initialized with CraftingSystem.");
        PopulateRecipeList();
    }

    public void CraftSelectedRecipe()
    {
        if (selectedRecipe == null)
        {
            Debug.Log("CraftSelectedRecipe: no recipe selected.");
            return;
        }
        if (craftingSystem == null)
        {
            Debug.LogWarning("CraftSelectedRecipe: craftingSystem is null.");
            return;
        }
        craftingSystem.Craft(selectedRecipe);
        if (inventoryUI != null)
            inventoryUI.Refresh();

        RefreshCraftButton();

        if (messageText != null)
            messageText.text = "You crafted " + selectedRecipe.outputItem.itemName + ".";
    }

    public void RefreshCraftButton()
    {
        if (craftButton != null)
            craftButton.interactable = craftingSystem != null && craftingSystem.inventory != null && selectedRecipe != null && craftingSystem.inventory.HasIngredients(selectedRecipe);
    }
}