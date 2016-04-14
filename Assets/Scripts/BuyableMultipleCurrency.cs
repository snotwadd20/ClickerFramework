using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Collections.Generic;

public class MCPrice
{
    public Currency currency = null;
    public float basePrice = 0;
    public float perLevelPriceMult = 1;

    public MCPrice(Currency c, float p, float m) { currency = c; basePrice = p; perLevelPriceMult = m; } //constructor
    public float adjustedPrice(int numOwned) { return basePrice * Mathf.Pow(perLevelPriceMult, numOwned); }
    public override string ToString()
    {
        return ("BP: " + basePrice + " C: " + currency.name + " MLT: " + perLevelPriceMult);
    }
}//MCPrice

public class BuyableMultipleCurrency : MonoBehaviour, IQuantity, IMaximum
{
    public Currency[] currencies = null;
    public float[] basePrices = null;
    public float[] pricePerLevelMults = null;

    private List<MCPrice> multCurrencyPrices = null;

    public int numberOwned = 0;
    public int ownedMaximum = int.MaxValue;

    public Button theButton = null;

    public MonoBehaviour[] disableBehaviorsOnMax = null;
    public GameObject[] disableObjectsOnMax = null;

    public bool _debugPrintCost = false;

    void Start()
    {
        if( currencies == null || currencies.Length < 1 )
        { 
            Debug.LogWarning(this.GetType() + ": No Currencies specified. Disabling.");
            enabled = false;
            return;
        }//if

        if (theButton == null)
        {
            theButton = gameObject.GetComponentInParent<Button>();
        }//if

        //Fill out the Multi-Currency Price List
        multCurrencyPrices = new List<MCPrice>();
        for (int i = 0; i < currencies.Length; i++)
        {
            //If a corresponding currency slot is not filled, reject the whole row
            if (currencies[i] == null)
                continue;

            //Every currency specified gets one price. Default is zero
            float p = 0;
            if (basePrices == null || i < basePrices.Length)
                p = basePrices[i];
            
            //Every currency gets one multiplier. Default is one
            float m = 1;
            if (pricePerLevelMults == null || i < pricePerLevelMults.Length)
                m = pricePerLevelMults[i];

            multCurrencyPrices.Add(new MCPrice(currencies[i], p, m));
        }//for
    }//Start

    // Update is called once per frame
    void Update()
    {
        UpdateButtonInfo();
        if( _debugPrintCost )
        {
            
            string str = "Prices: ";
            foreach(MCPrice price in Prices)
            {
                str += price.currency.name + ": " + price.adjustedPrice(numberOwned) + ", ";
            }//foreach
            print(str);
            _debugPrintCost = false;
        }//if
    }//Update

    void LateUpdate()
    {
        if (numberOwned >= ownedMaximum)
        {
            ShutDown();
        }//if
    }//lateUpdate

    public int Maximum
    {
        get { return ownedMaximum; }
    }
    public int Quantity
    {
        get { return numberOwned; }
    }

    private void ShutDown()
    {
        for (int i = 0; i < disableObjectsOnMax.Length; i++)
            disableObjectsOnMax[i].SetActive(false);

        for (int i = 0; i < disableBehaviorsOnMax.Length; i++)
            disableBehaviorsOnMax[i].enabled = false;

    }//ShutDown
    public void UpdateButtonInfo()
    {
        if (theButton != null)
        {
            bool _showButton = true;
            foreach (MCPrice price in Prices)
            {
                if (price.currency.Amount < price.adjustedPrice(numberOwned))
                    _showButton = false;
            }//foreach
            theButton.interactable = _showButton && (numberOwned < ownedMaximum);
        }//if
    }//UpdateButtonInfo

    public List<MCPrice> Prices
    {
        get { return multCurrencyPrices; }
    }//Prices

    public void Purchase() { DoPurchase(); }
    public void Buy() { DoPurchase(); }
    public void DoPurchase()
    {
        _debugPrintCost = true;
        foreach (MCPrice price in Prices)
        {
            price.currency.SubtractAmount(price.adjustedPrice(numberOwned));
        }//foreach
        //currency.SubtractAmount(Price);
        //GameManager.self.SubtractResource(Price);
        numberOwned++;
        UpdateButtonInfo();
        gameObject.SendMessage("OnPurchased", SendMessageOptions.DontRequireReceiver);
    }//DoPurchase
}//BuyableMultipleCurrency
