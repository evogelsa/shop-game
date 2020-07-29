using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ManagePrice : MonoBehaviour
{
    public TextMeshProUGUI priceAmount;
    public Slider priceSlider;

    private float priceMax = 10f;
    private float priceMin = 0.05f;
    private float startPrice = 2.5f;

    void OnEnable()
    {
        priceSlider.maxValue = priceMax;
        if (PlayerPrefs.HasKey("Price"))
        {
            priceSlider.value = GetPrice();
            priceAmount.text = GetPrice().ToString("$0.00");
        }
        else
        {
            PlayerPrefs.SetFloat("Price", startPrice);
            priceSlider.value = startPrice;
            priceAmount.text = startPrice.ToString("$0.00");
        }
        priceSlider.minValue = priceMin;
    }

    public void SetPrice()
    {
        PlayerPrefs.SetFloat("Price", priceSlider.value);
        priceAmount.text = priceSlider.value.ToString("$0.00");
    }

    public float GetPrice()
    {
        return PlayerPrefs.GetFloat("Price");
    }
}
