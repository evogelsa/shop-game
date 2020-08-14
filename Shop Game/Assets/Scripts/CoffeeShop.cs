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

    public GameObject resourceObject;
    private ManageResources manageResources;

    void Start()
    {
        manageReputation = reputationObject.GetComponent<ManageReputation>();
        manageMoney = moneyObject.GetComponent<ManageMoney>();
        managePrice = priceObject.GetComponent<ManagePrice>();
        manageResources = resourceObject.GetComponent<ManageResources>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        (int servings, string _) = manageResources.CalculateServings();
        if (manageReputation.WillBuy() && servings > 0)
        {
            manageMoney.AddMoney(managePrice.GetPrice());
            manageResources.ConsumeServing();
        }
    }
}
