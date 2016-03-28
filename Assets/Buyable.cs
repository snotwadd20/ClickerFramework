using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class Buyable : MonoBehaviour
{
    public float baseCost = 1.0f;
    public float costMult = 1.15f; //1.07-1.15 is a good range
    public float numberOwned = 0;

    public Button theButton = null;
    public Text priceText = null;

    public float _debugCurrentCost = 0;

    // Update is called once per frame
    void Update()
    {
        
        UpdateButtonInfo();
        _debugCurrentCost = Cost;

    }//Update

    public void UpdateButtonInfo()
    {
        if (priceText != null)
            priceText.text = "Cost $:" + string.Format("{0:n2}",Math.Round(Cost, 1));

        if (theButton != null)
            theButton.interactable = (GameManager.self.Resource >= Cost);
    }

    public float Cost
    {
        get
        {
            return baseCost * Mathf.Pow(costMult, numberOwned);
        }
    }

    public void DoPurchase()
    {
        GameManager.self.SubtractResource(Cost);
        numberOwned++;
        UpdateButtonInfo();
    }//DoPurchase
}//Buyable
