using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManageReputation : MonoBehaviour
{
    public GameObject reputationSliderObject;
    public Slider reputationSlider;

    private float startReputation = .2f;
    private float maxReputation = 1f;

    void OnEnable()
    {
        if (!PlayerPrefs.HasKey("Reputation"))
        {
            PlayerPrefs.SetFloat("Reputation", startReputation);
            reputationSlider.value = startReputation/maxReputation;
        }
        else
        {
            reputationSlider.value = GetReputation();
        }
    }

    public void SetReputation(float rep)
    {
        PlayerPrefs.SetFloat("Reputation", rep);
        reputationSlider.value = GetReputation();
    }

    public void AddReputation(float rep)
    {
        SetReputation(GetReputation() + rep);
    }

    public float GetReputation()
    {
        return PlayerPrefs.GetFloat("Reputation");
    }

    public bool WillBuy()
    {
        if (Random.value < GetReputation())
            return true;
        else
            return false;
    }
}
