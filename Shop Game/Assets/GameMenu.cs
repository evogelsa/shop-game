using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameMenu : MonoBehaviour
{
    private float CoffeeMax = 10;
    private float CoffeeMin = 0;

    private float MilkMax = 2;
    private float MilkMin = 0;

    private float SugarMax = 10;
    private float SugarMin = 0;

    public void Increase10(GameObject Item)
    {
        TextMeshProUGUI Text = Item.GetComponent<TextMeshProUGUI>();
        int amount = int.Parse(Text.text) + 10;
        Text.text = amount.ToString();
    }

    public void Increase25(GameObject Item)
    {
        TextMeshProUGUI Text = Item.GetComponent<TextMeshProUGUI>();
        int amount = int.Parse(Text.text) + 25;
        Text.text = amount.ToString();
    }

    public void Increase50(GameObject Item)
    {
        TextMeshProUGUI Text = Item.GetComponent<TextMeshProUGUI>();
        int amount = int.Parse(Text.text) + 50;
        Text.text = amount.ToString();
    }

    public void SetCoffeeRecipe(GameObject Item)
    {
        Slider Recipe = GameObject.Find("CoffeeSlider").GetComponent<Slider>();
        float amount = (CoffeeMax * Recipe.value) + CoffeeMin;
        TextMeshProUGUI Text = Item.GetComponent<TextMeshProUGUI>();
        Text.text = amount.ToString("0.0");
    }

    public void SetMilkRecipe(GameObject Item)
    {
        Slider Recipe = GameObject.Find("MilkSlider").GetComponent<Slider>();
        float amount = (MilkMax * Recipe.value) + MilkMin;
        TextMeshProUGUI Text = Item.GetComponent<TextMeshProUGUI>();
        Text.text = amount.ToString("0.0");
    }

    public void SetSugarRecipe(GameObject Item)
    {
        Slider Recipe = GameObject.Find("SugarSlider").GetComponent<Slider>();
        float amount = (SugarMax * Recipe.value) + SugarMin;
        TextMeshProUGUI Text = Item.GetComponent<TextMeshProUGUI>();
        Text.text = amount.ToString("0.0");
    }
}
