using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ManageResources : MonoBehaviour
{
    public GameObject cupsTextObject;
    private TextMeshProUGUI cupsText;

    public GameObject coffeeTextObject;
    private TextMeshProUGUI coffeeText;

    public GameObject milkTextObject;
    private TextMeshProUGUI milkText;

    public GameObject sugarTextObject;
    private TextMeshProUGUI sugarText;

    void Start()
    {
        cupsText   = cupsTextObject.GetComponent<TextMeshProUGUI>();
        coffeeText = coffeeTextObject.GetComponent<TextMeshProUGUI>();
        milkText   = milkTextObject.GetComponent<TextMeshProUGUI>();
        sugarText  = sugarTextObject.GetComponent<TextMeshProUGUI>();

        string[] keys = new string[]{
            "CupsInventoryAmount", "CoffeeInventoryAmount",
            "MilkInventoryAmount", "SugarInventoryAmount",
        };

        foreach (string key in keys)
        {
            if (!PlayerPrefs.HasKey(key))
            {
                PlayerPrefs.SetFloat(key, 0);
            }
        }

        cupsText.text = GetCups().ToString("0");
        coffeeText.text = GetCoffee().ToString("0.0");
        milkText.text = GetMilk().ToString("0.0");
        sugarText.text = GetSugar().ToString("0.0");
    }

    public float GetCups()
    {
        return PlayerPrefs.GetFloat("CupsInventoryAmount");
    }

    public void SetCups(float amount)
    {
        PlayerPrefs.SetFloat("CupsInventoryAmount", amount);
        cupsText.text = GetCups().ToString("0");
    }

    public void AddCups(float amount)
    {
        SetCups(GetCups() + amount);
    }

    public float GetCoffee()
    {
        return PlayerPrefs.GetFloat("CoffeeInventoryAmount");
    }

    public void SetCoffee(float amount)
    {
        PlayerPrefs.SetFloat("CoffeeInventoryAmount", amount);
        coffeeText.text = GetCoffee().ToString("0.0");
    }

    public void AddCoffee(float amount)
    {
        SetCoffee(GetCoffee() + amount);
    }

    public float GetMilk()
    {
        return PlayerPrefs.GetFloat("MilkInventoryAmount");
    }

    public void SetMilk(float amount)
    {
        PlayerPrefs.SetFloat("MilkInventoryAmount", amount);
        milkText.text = GetMilk().ToString("0.0");
    }

    public void AddMilk(float amount)
    {
        SetMilk(GetMilk() + amount);
    }

    public float GetSugar()
    {
        return PlayerPrefs.GetFloat("SugarInventoryAmount");
    }

    public void SetSugar(float amount)
    {
        PlayerPrefs.SetFloat("SugarInventoryAmount", amount);
        sugarText.text = GetSugar().ToString("0.0");
    }

    public void AddSugar(float amount)
    {
        SetSugar(GetSugar() + amount);
    }

    public (int, string) CalculateServings()
    {
        float cupAmount = GetCups();
        float coffeeAmount =GetCoffee();
        float milkAmount = GetMilk();
        float sugarAmount =GetSugar();

        float cupsPerServing = 1f;
        float coffeePerServing = PlayerPrefs.GetFloat("CoffeeRecipe");
        float milkPerServing = PlayerPrefs.GetFloat("MilkRecipe");
        float sugarPerServing = PlayerPrefs.GetFloat("SugarRecipe");

        float cupServings = (cupAmount / cupsPerServing);
        float coffeeServings = (coffeeAmount / coffeePerServing);
        float milkServings = (milkAmount / milkPerServing);
        float sugarServings = (sugarAmount / sugarPerServing);

        float[] servings = new float[]{
            cupServings, coffeeServings,
            milkServings, sugarServings
        };

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

        return ((int) min, ingredients[idx]);
    }

    public void ConsumeServing()
    {
        float cupsPerServing = 1f;
        float coffeePerServing = PlayerPrefs.GetFloat("CoffeeRecipe");
        float milkPerServing = PlayerPrefs.GetFloat("MilkRecipe");
        float sugarPerServing = PlayerPrefs.GetFloat("SugarRecipe");

        AddCups(-cupsPerServing);
        AddCoffee(-coffeePerServing);
        AddMilk(-milkPerServing);
        AddSugar(-sugarPerServing);
    }
}
