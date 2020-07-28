using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ManageMoney : MonoBehaviour
{
    public GameObject moneyTextObject;

    private TextMeshProUGUI moneyText;
    private float startMoney = 30f;

    void OnEnable()
    {
        moneyText = moneyTextObject.GetComponent<TextMeshProUGUI>();
        if (!PlayerPrefs.HasKey("Money"))
        {
            PlayerPrefs.SetFloat("Money", startMoney);
            moneyText.text = startMoney.ToString("$0.00");
        }
        else
        {
            moneyText.text = GetMoney().ToString("$0.00");
        }
    }

    public void SetMoney(float money)
    {
        PlayerPrefs.SetFloat("Money", money);
        moneyText.text = GetMoney().ToString("$0.00");
    }

    public void AddMoney(float money)
    {
        SetMoney(GetMoney() + money);
    }

    public float GetMoney()
    {
        return PlayerPrefs.GetFloat("Money");
    }

    public bool HasEnough(float money)
    {
        if (money <= PlayerPrefs.GetFloat("Money"))
            return true;
        else
            return false;
    }
}
