using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameMenu : MonoBehaviour
{
    public GameObject money;
    private ManageMoney manageMoney;

    // ingredient settings
    private float cupsCost10 = 2f;
    private float cupsCost25 = 4f;
    private float cupsCost50 = 6f;

    private float coffeeRecipeMax = 4f;
    private float coffeeRecipeMin = 1f;
    private float coffeeCost10 = 5f;
    private float coffeeCost25 = 8.75f;
    private float coffeeCost50 = 15f;

    private float milkRecipeMax = 2f;
    private float milkRecipeMin = 0f;
    private float milkCost10 = 1f;
    private float milkCost25 = 2.25f;
    private float milkCost50 = 2.95f;

    private float sugarRecipeMax = 4f;
    private float sugarRecipeMin = 0f;
    private float sugarCost10 = 3f;
    private float sugarCost25 = 6.25f;
    private float sugarCost50 = 8f;

    private float priceMax = 10f;
    private float priceMin = 0.05f;

    void OnEnable()
    {
        Time.timeScale = 0f;

        manageMoney = money.GetComponent<ManageMoney>();

        string[] amountsToLoad = new string[]{
            "CupsInventoryAmount", "CoffeeInventoryAmount",
            "MilkInventoryAmount", "SugarInventoryAmount",
            };

        foreach (string amount in amountsToLoad)
        {
            GameObject amountObject = GameObject.Find(amount);
            TextMeshProUGUI amountText = amountObject.GetComponent<TextMeshProUGUI>();
            if (PlayerPrefs.HasKey(amount))
                amountText.text = PlayerPrefs.GetInt(amount).ToString();
            else
                PlayerPrefs.SetInt(amount, 0);
        }

        string[] recipesToLoad = new string[]{
            "CoffeeRecipeSlider", "MilkRecipeSlider",
            "SugarRecipeSlider"
            };

        foreach (string recipe in recipesToLoad)
        {
            GameObject recipeObject = GameObject.Find(recipe);
            Slider recipeSlider = recipeObject.GetComponent<Slider>();
            if (PlayerPrefs.HasKey(recipe))
            {
                recipeSlider.value = PlayerPrefs.GetFloat(recipe);
                GameObject label;
                TextMeshProUGUI labelText;
                float amount;
                switch (recipe)
                {
                    case "CoffeeRecipeSlider":
                        label = GameObject.Find("CoffeeRecipeAmount");
                        labelText = label.GetComponent<TextMeshProUGUI>();
                        amount = (coffeeRecipeMax-coffeeRecipeMin)*recipeSlider.value + coffeeRecipeMin;
                        labelText.text = amount.ToString("0.0");
                        break;
                    case "MilkRecipeSlider":
                        label = GameObject.Find("MilkRecipeAmount");
                        labelText = label.GetComponent<TextMeshProUGUI>();
                        amount = (milkRecipeMax-milkRecipeMin)*recipeSlider.value + milkRecipeMin;
                        labelText.text = amount.ToString("0.0");
                        break;
                    case "SugarRecipeSlider":
                        label = GameObject.Find("SugarRecipeAmount");
                        labelText = label.GetComponent<TextMeshProUGUI>();
                        amount = (sugarRecipeMax-sugarRecipeMin)*recipeSlider.value + sugarRecipeMin;
                        labelText.text = amount.ToString("0.0");
                        break;
                }
            }
            else 
            {
                PlayerPrefs.SetFloat(recipe, 0) ;
            }
        }

        GameObject priceObject = GameObject.Find("PriceSlider");
        Slider priceSlider = priceObject.GetComponent<Slider>();
        GameObject priceAmount = GameObject.Find("PriceAmount");
        TextMeshProUGUI priceAmountText = priceAmount.GetComponent<TextMeshProUGUI>();
        if (PlayerPrefs.HasKey("PriceSlider"))
        {
            priceSlider.value = PlayerPrefs.GetFloat("PriceSlider");
            float price = (priceMax-priceMin)*priceSlider.value + priceMin;
            priceAmountText.text = price.ToString("$0.00");
        }
        else
        {
            PlayerPrefs.SetFloat("PriceSlider", 0);
        }

        Dictionary<string, float> itemCostsToLoad = new Dictionary<string, float>()
            {
                {"CupsCost10", cupsCost10}, 
                {"CupsCost25", cupsCost25}, 
                {"CupsCost50", cupsCost50},
                {"CoffeeCost10", coffeeCost10},
                {"CoffeeCost25", coffeeCost25},
                {"CoffeeCost50", coffeeCost50},
                {"MilkCost10", milkCost10},
                {"MilkCost25", milkCost25},
                {"MilkCost50", milkCost50},
                {"SugarCost10", sugarCost10},
                {"SugarCost25", sugarCost25},
                {"SugarCost50", sugarCost50}
            };

        foreach  (KeyValuePair<string, float> item in itemCostsToLoad)
        {
            GameObject itemCost = GameObject.Find(item.Key);
            TextMeshProUGUI itemCostText = itemCost.GetComponent<TextMeshProUGUI>();
            itemCostText.text = item.Value.ToString("$0.00");
        }
    }

    void OnDisable()
    {
        Time.timeScale = 1f;
    }

    void Update()
    {
        CalculateServings();
    }

    public void Buy10(GameObject item)
    {
        TextMeshProUGUI text = item.GetComponent<TextMeshProUGUI>();
        bool success = false;
        switch (item.name)
        {
            case "CupsInventoryAmount":
                if (manageMoney.HasEnough(cupsCost10))
                {
                    manageMoney.AddMoney(-cupsCost10);
                    success = true;
                }
                break;
            case "CoffeeInventoryAmount":
                if (manageMoney.HasEnough(coffeeCost10))
                {
                    manageMoney.AddMoney(-coffeeCost10);
                    success = true;
                }
                break;
            case "MilkInventoryAmount":
                if (manageMoney.HasEnough(milkCost10))
                {
                    manageMoney.AddMoney(-milkCost10);
                    success = true;
                }
                break;
            case "SugarInventoryAmount":
                if (manageMoney.HasEnough(sugarCost10))
                {
                    manageMoney.AddMoney(-sugarCost10);
                    success = true;
                }
                break;
        }

        if (success)
        {
            PlayerPrefs.SetInt(item.name, PlayerPrefs.GetInt(item.name) + 10);
            text.text = PlayerPrefs.GetInt(item.name).ToString();
        }
    }

    public void Buy25(GameObject item)
    {
        TextMeshProUGUI text = item.GetComponent<TextMeshProUGUI>();
        bool success = false;
        switch (item.name)
        {
            case "CupsInventoryAmount":
                if (manageMoney.HasEnough(cupsCost25))
                {
                    manageMoney.AddMoney(-cupsCost25);
                    success = true;
                }
                break;
            case "CoffeeInventoryAmount":
                if (manageMoney.HasEnough(coffeeCost25))
                {
                    manageMoney.AddMoney(-coffeeCost25);
                    success = true;
                }
                break;
            case "MilkInventoryAmount":
                if (manageMoney.HasEnough(milkCost25))
                {
                    manageMoney.AddMoney(-milkCost25);
                    success = true;
                }
                break;
            case "SugarInventoryAmount":
                if (manageMoney.HasEnough(sugarCost25))
                {
                    manageMoney.AddMoney(-sugarCost25);
                    success = true;
                }
                break;
        }

        if (success)
        {
            PlayerPrefs.SetInt(item.name, PlayerPrefs.GetInt(item.name) + 25);
            text.text = PlayerPrefs.GetInt(item.name).ToString();
        }
    }

    public void Buy50(GameObject item)
    {
        TextMeshProUGUI text = item.GetComponent<TextMeshProUGUI>();
        bool success = false;
        switch (item.name)
        {
            case "CupsInventoryAmount":
                if (manageMoney.HasEnough(cupsCost50))
                {
                    manageMoney.AddMoney(-cupsCost50);
                    success = true;
                }
                break;
            case "CoffeeInventoryAmount":
                if (manageMoney.HasEnough(coffeeCost50))
                {
                    manageMoney.AddMoney(-coffeeCost50);
                    success = true;
                }
                break;
            case "MilkInventoryAmount":
                if (manageMoney.HasEnough(milkCost50))
                {
                    manageMoney.AddMoney(-milkCost50);
                    success = true;
                }
                break;
            case "SugarInventoryAmount":
                if (manageMoney.HasEnough(sugarCost50))
                {
                    manageMoney.AddMoney(-sugarCost50);
                    success = true;
                }
                break;
        }

        if (success)
        {
            PlayerPrefs.SetInt(item.name, PlayerPrefs.GetInt(item.name) + 50);
            text.text = PlayerPrefs.GetInt(item.name).ToString();
        }
    }

    public void SetCoffeeRecipe(GameObject item)
    {
        Slider recipe = GameObject.Find("CoffeeRecipeSlider").GetComponent<Slider>();
        TextMeshProUGUI recipeText = item.GetComponent<TextMeshProUGUI>();
        PlayerPrefs.SetFloat("CoffeeRecipeSlider", recipe.value);
        float amount = (coffeeRecipeMax-coffeeRecipeMin)*recipe.value + coffeeRecipeMin;
        recipeText.text = amount.ToString("0.0");
    }

    public void SetMilkRecipe(GameObject item)
    {
        Slider recipe = GameObject.Find("MilkRecipeSlider").GetComponent<Slider>();
        TextMeshProUGUI recipeText = item.GetComponent<TextMeshProUGUI>();
        PlayerPrefs.SetFloat("MilkRecipeSlider", recipe.value);
        float amount = (milkRecipeMax-milkRecipeMin)*recipe.value + milkRecipeMin;
        recipeText.text = amount.ToString("0.0");
    }

    public void SetSugarRecipe(GameObject item)
    {
        Slider recipe = GameObject.Find("SugarRecipeSlider").GetComponent<Slider>();
        TextMeshProUGUI recipeText = item.GetComponent<TextMeshProUGUI>();
        PlayerPrefs.SetFloat("SugarRecipeSlider", recipe.value);
        float amount = (sugarRecipeMax-sugarRecipeMin)*recipe.value + sugarRecipeMin;
        recipeText.text = amount.ToString("0.0");
    }

    public void SetPrice(GameObject priceText)
    {
        Slider price = GameObject.Find("PriceSlider").GetComponent<Slider>();
        TextMeshProUGUI text = priceText.GetComponent<TextMeshProUGUI>();
        PlayerPrefs.SetFloat("PriceSlider", price.value);
        float amount = (priceMax-priceMin)*price.value + priceMin;
        text.text = amount.ToString("$0.00");
    }

    private void CalculateServings()
    {
        int cupAmount = PlayerPrefs.GetInt("CupsInventoryAmount");
        int coffeeAmount = PlayerPrefs.GetInt("CoffeeInventoryAmount");
        int milkAmount = PlayerPrefs.GetInt("MilkInventoryAmount");
        int sugarAmount = PlayerPrefs.GetInt("SugarInventoryAmount");

        float cupsPerServing = 1f;
        float coffeePerServing = (PlayerPrefs.GetFloat("CoffeeRecipeSlider") * 
                (coffeeRecipeMax-coffeeRecipeMin) + coffeeRecipeMin);
        float milkPerServing = (PlayerPrefs.GetFloat("MilkRecipeSlider") *
                (milkRecipeMax-milkRecipeMin) + milkRecipeMin);
        float sugarPerServing = (PlayerPrefs.GetFloat("SugarRecipeSlider") *
                (sugarRecipeMax-sugarRecipeMin) + sugarRecipeMin);

        float cupServings = (cupAmount / cupsPerServing);
        float coffeeServings = (coffeeAmount / coffeePerServing);
        float milkServings = (milkAmount / milkPerServing);
        float sugarServings = (sugarAmount / sugarPerServing);

        float[] servings = new float[]{cupServings, coffeeServings, milkServings, sugarServings};
        string[] ingredients = new string[]{"Cups", "Coffee", "Milk", "Sugar"};
        float min = float.MaxValue;
        int idx = -1;
        for (int i = 0; i < servings.Length; i++)
        {
            if (servings[i] < min)
            {
                min = servings[i];
                idx = i;
            }
        }
        if (min < 0)
            min = 0;
        else 
            min = (float) ((int) min);

        TextMeshProUGUI servingsText = GameObject.Find("ServingsText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI limitedText = GameObject.Find("LimitedByText").GetComponent<TextMeshProUGUI>();
        servingsText.text = "Servings: " + min.ToString("0");
        limitedText.text = "Limited by: " + ingredients[idx];
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
