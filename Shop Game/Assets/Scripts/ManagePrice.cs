using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ManagePrice : MonoBehaviour
{
    public GameObject priceAmountObject;
    private TextMeshProUGUI priceAmount;

    public GameObject priceSliderObject;
    private Slider priceSlider;

    private float priceMax = 10f;
    private float priceMin = 0.05f;
    private float startPrice = 2.5f;

    void OnEnable()
    {
        priceAmount = priceAmountObject.GetComponent<TextMeshProUGUI>();
        priceSlider = priceSliderObject.GetComponent<Slider>();
        priceSlider.maxValue = priceMax;
        priceSlider.minValue = priceMin;
        if (!PlayerPrefs.HasKey("Price"))
        {
            PlayerPrefs.SetFloat("Price", startPrice);
            priceAmount.text = startPrice.ToString("$0.00");
        }
        else
        {
            priceAmount.text = GetPrice().ToString("$0.00");
        }
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
