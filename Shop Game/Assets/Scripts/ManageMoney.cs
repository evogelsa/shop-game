using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ManageMoney : MonoBehaviour
{
    public GameObject moneyObject;

    private void Update()
    {
        TextMeshProUGUI moneyText = moneyObject.GetComponent<TextMeshProUGUI>();
        if (PlayerPrefs.HasKey("Money"))
        {
            moneyText.text = PlayerPrefs.GetFloat("Money").ToString("$0.00");
        }
        else
        {
            PlayerPrefs.SetFloat("Money", 30.00f);
            moneyText.text = 30.00f.ToString("$0.00");
        }
    }

    public void SetMoney(float money)
    {
        PlayerPrefs.SetFloat("Money", money);
    }

    public void AddMoney(float money)
    {
        PlayerPrefs.SetFloat("Money", PlayerPrefs.GetFloat("Money") + money);
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
