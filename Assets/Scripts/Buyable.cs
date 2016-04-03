using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class Buyable : MonoBehaviour, IPrice, IQuantity, IMaximum
{
    public Currency currency = null;

    public float baseCost = 1.0f;
    public float costMult = 1.15f; //1.07-1.15 is a good range
    public int numberOwned = 0;
    public int maxOwned = int.MaxValue;

    public Button theButton = null;
    
    public MonoBehaviour[] disableBehaviorsOnMax = null;
    public GameObject[] disableObjectsOnMax = null;

    public float _debugCurrentCost = 0;

    void Start()
    {
        if (currency == null && Currency.Default == null)
        {
            Debug.LogWarning(this.GetType() + ": No Currency specified. Disabling.");
            enabled = false;
            return;
        }//if
        else if (currency == null && Currency.Default != null)
        {
            currency = Currency.Default;
        }//else if
    }//Start

    // Update is called once per frame
    void Update()
    {
        UpdateButtonInfo();
        _debugCurrentCost = Price;   
    }//Update

    void LateUpdate()
    {
        if(numberOwned >= maxOwned)
        {
            ShutDown();
        }//if
    }//lateUpdate

    public int Maximum
    {
        get { return maxOwned; }
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
            theButton.interactable = ((currency.Amount >= Price) && (numberOwned < maxOwned));
    }

    public float Price
    {
        get
        {
            return baseCost * Mathf.Pow(costMult, numberOwned);
        }
    }

    //Why not?
    public void Purchase() { DoPurchase(); }
    public void Buy() { DoPurchase(); } 
    public void DoPurchase()
    {
        currency.SubtractAmount(Price);
        //GameManager.self.SubtractResource(Price);
        numberOwned++;
        UpdateButtonInfo();
        gameObject.SendMessage("OnPurchased", SendMessageOptions.DontRequireReceiver);
    }//DoPurchase
}//Buyable
