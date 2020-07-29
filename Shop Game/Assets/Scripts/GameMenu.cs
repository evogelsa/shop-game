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
    public Slider coffeeRecipeSlider;
    public TextMeshProUGUI coffeeRecipeAmount;

    private float milkRecipeMax = 2f;
    private float milkRecipeMin = 0f;
    private float milkCost10 = 1f;
    private float milkCost25 = 2.25f;
    private float milkCost50 = 2.95f;
    public Slider milkRecipeSlider;
    public TextMeshProUGUI milkRecipeAmount;

    private float sugarRecipeMax = 4f;
    private float sugarRecipeMin = 0f;
    private float sugarCost10 = 3f;
    private float sugarCost25 = 6.25f;
    private float sugarCost50 = 8f;
    public Slider sugarRecipeSlider;
    public TextMeshProUGUI sugarRecipeAmount;

    void OnEnable()
    {
        // pause game
        Time.timeScale = 0f;

        // get manage money script
        manageMoney = money.GetComponent<ManageMoney>();

        // init coffee slider
        coffeeRecipeSlider.maxValue = coffeeRecipeMax;
        if (PlayerPrefs.HasKey("CoffeeRecipe"))
        {
            coffeeRecipeSlider.value = PlayerPrefs.GetFloat("CoffeeRecipe");
            coffeeRecipeAmount.text = PlayerPrefs.GetFloat("CoffeeRecipe").ToString("0.0");
        }
        else
        {
            PlayerPrefs.SetFloat("CoffeeRecipe", coffeeRecipeMin);
            coffeeRecipeSlider.value = coffeeRecipeMin;
            coffeeRecipeAmount.text = coffeeRecipeMin.ToString("0.0");
        }
        coffeeRecipeSlider.minValue = coffeeRecipeMin;

        // init milk slider
        milkRecipeSlider.maxValue = milkRecipeMax;
        if (PlayerPrefs.HasKey("MilkRecipe"))
        {
            milkRecipeSlider.value = PlayerPrefs.GetFloat("MilkRecipe");
            milkRecipeAmount.text = PlayerPrefs.GetFloat("MilkRecipe").ToString("0.0");
        }
        else
        {
            PlayerPrefs.SetFloat("MilkRecipe", milkRecipeMin);
            milkRecipeSlider.value = milkRecipeMin;
            milkRecipeAmount.text = milkRecipeMin.ToString("0.0");
        }
        milkRecipeSlider.minValue = milkRecipeMin;

        // init sugar slider
        sugarRecipeSlider.maxValue = sugarRecipeMax;
        if (PlayerPrefs.HasKey("SugarRecipe"))
        {
            sugarRecipeSlider.value = PlayerPrefs.GetFloat("SugarRecipe");
            sugarRecipeAmount.text = PlayerPrefs.GetFloat("SugarRecipe").ToString("0.0");
        }
        else
        {
            PlayerPrefs.SetFloat("SugarRecipe", sugarRecipeMin);
            sugarRecipeSlider.value = sugarRecipeMin;
            sugarRecipeAmount.text = sugarRecipeMin.ToString("0.0");
        }
        sugarRecipeSlider.minValue = sugarRecipeMin;

        string[] amountsToLoad = new string[]{
            "CupsInventoryAmount", "CoffeeInventoryAmount",
            "MilkInventoryAmount", "SugarInventoryAmount",
            };

        // load amounts of each item owned
        foreach (string amount in amountsToLoad)
        {
            TextMeshProUGUI amountText = GameObject.Find(amount).GetComponent<TextMeshProUGUI>();
            if (PlayerPrefs.HasKey(amount))
                amountText.text = PlayerPrefs.GetInt(amount).ToString();
            else
                PlayerPrefs.SetInt(amount, 0);
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

    public void SetCoffeeRecipe()
    {
        PlayerPrefs.SetFloat("CoffeeRecipe", coffeeRecipeSlider.value);
        coffeeRecipeAmount.text = coffeeRecipeSlider.value.ToString("0.0");
    }

    public void SetMilkRecipe()
    {
        PlayerPrefs.SetFloat("MilkRecipe", milkRecipeSlider.value);
        milkRecipeAmount.text = milkRecipeSlider.value.ToString("0.0");
    }

    public void SetSugarRecipe()
    {
        PlayerPrefs.SetFloat("SugarRecipe", sugarRecipeSlider.value);
        sugarRecipeAmount.text = sugarRecipeSlider.value.ToString("0.0");
    }

    private void CalculateServings()
    {
        int cupAmount = PlayerPrefs.GetInt("CupsInventoryAmount");
        int coffeeAmount = PlayerPrefs.GetInt("CoffeeInventoryAmount");
        int milkAmount = PlayerPrefs.GetInt("MilkInventoryAmount");
        int sugarAmount = PlayerPrefs.GetInt("SugarInventoryAmount");

        float cupsPerServing = 1f;
        float coffeePerServing = (PlayerPrefs.GetFloat("CoffeeRecipe") * 
                (coffeeRecipeMax-coffeeRecipeMin) + coffeeRecipeMin);
        float milkPerServing = (PlayerPrefs.GetFloat("MilkRecipe") *
                (milkRecipeMax-milkRecipeMin) + milkRecipeMin);
        float sugarPerServing = (PlayerPrefs.GetFloat("SugarRecipe") *
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
