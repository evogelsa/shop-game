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
    private ManageResources manageResources;

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

    private TextMeshProUGUI servingsText;
    private TextMeshProUGUI limitedText;


    void Start()
    {
        // pause game
        Time.timeScale = 0f;

        // get manage money script
        manageMoney = money.GetComponent<ManageMoney>();
        // get manage resources script
        manageResources = gameObject.GetComponent<ManageResources>();

        servingsText = GameObject.Find("ServingsText").GetComponent<TextMeshProUGUI>();
        limitedText = GameObject.Find("LimitedByText").GetComponent<TextMeshProUGUI>();

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
        // resume game
        Time.timeScale = 1f;
    }

    void Update()
    {
        // refresh amount of possible servings given recipe
        UpdateServings();
    }

    public void Buy10(GameObject item)
    {
        TextMeshProUGUI text = item.GetComponent<TextMeshProUGUI>();
        switch (item.name)
        {
            case "CupsInventoryAmount":
                if (manageMoney.HasEnough(cupsCost10))
                {
                    manageMoney.AddMoney(-cupsCost10);
                    manageResources.AddCups(10f);
                }
                break;
            case "CoffeeInventoryAmount":
                if (manageMoney.HasEnough(coffeeCost10))
                {
                    manageMoney.AddMoney(-coffeeCost10);
                    manageResources.AddCoffee(10f);
                }
                break;
            case "MilkInventoryAmount":
                if (manageMoney.HasEnough(milkCost10))
                {
                    manageMoney.AddMoney(-milkCost10);
                    manageResources.AddMilk(10f);
                }
                break;
            case "SugarInventoryAmount":
                if (manageMoney.HasEnough(sugarCost10))
                {
                    manageMoney.AddMoney(-sugarCost10);
                    manageResources.AddSugar(10f);
                }
                break;
        }
    }

    public void Buy25(GameObject item)
    {
        TextMeshProUGUI text = item.GetComponent<TextMeshProUGUI>();
        switch (item.name)
        {
            case "CupsInventoryAmount":
                if (manageMoney.HasEnough(cupsCost25))
                {
                    manageMoney.AddMoney(-cupsCost25);
                    manageResources.AddCups(25f);
                }
                break;
            case "CoffeeInventoryAmount":
                if (manageMoney.HasEnough(coffeeCost25))
                {
                    manageMoney.AddMoney(-coffeeCost25);
                    manageResources.AddCoffee(25f);
                }
                break;
            case "MilkInventoryAmount":
                if (manageMoney.HasEnough(milkCost25))
                {
                    manageMoney.AddMoney(-milkCost25);
                    manageResources.AddMilk(25f);
                }
                break;
            case "SugarInventoryAmount":
                if (manageMoney.HasEnough(sugarCost25))
                {
                    manageMoney.AddMoney(-sugarCost25);
                    manageResources.AddSugar(25f);
                }
                break;
        }
    }

    public void Buy50(GameObject item)
    {
        TextMeshProUGUI text = item.GetComponent<TextMeshProUGUI>();
        switch (item.name)
        {
            case "CupsInventoryAmount":
                if (manageMoney.HasEnough(cupsCost50))
                {
                    manageMoney.AddMoney(-cupsCost50);
                    manageResources.AddCups(50f);
                }
                break;
            case "CoffeeInventoryAmount":
                if (manageMoney.HasEnough(coffeeCost50))
                {
                    manageMoney.AddMoney(-coffeeCost50);
                    manageResources.AddCoffee(50f);
                }
                break;
            case "MilkInventoryAmount":
                if (manageMoney.HasEnough(milkCost50))
                {
                    manageMoney.AddMoney(-milkCost50);
                    manageResources.AddMilk(50f);
                }
                break;
            case "SugarInventoryAmount":
                if (manageMoney.HasEnough(sugarCost50))
                {
                    manageMoney.AddMoney(-sugarCost50);
                    manageResources.AddSugar(50f);
                }
                break;
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

    private void UpdateServings()
    {
        (int min, string limit) = manageResources.CalculateServings();
        servingsText.text = "Servings: " + min.ToString("0");
        limitedText.text = "Limited by: " + limit;
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
