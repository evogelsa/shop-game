using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeShop : MonoBehaviour
{
    public GameObject reputationObject;
    private ManageReputation manageReputation;

    public GameObject moneyObject;
    private ManageMoney manageMoney;
    
    public GameObject priceObject;
    private ManagePrice managePrice;

    void OnEnable()
    {
        manageReputation = reputationObject.GetComponent<ManageReputation>();
        manageMoney = moneyObject.GetComponent<ManageMoney>();
        managePrice = priceObject.GetComponent<ManagePrice>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (manageReputation.WillBuy())
        {
            manageMoney.AddMoney(managePrice.GetPrice());
        }
    }
}
