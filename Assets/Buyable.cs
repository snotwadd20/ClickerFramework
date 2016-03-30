using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class Buyable : MonoBehaviour
{
    public float baseCost = 1.0f;
    public float costMult = 1.15f; //1.07-1.15 is a good range
    public int numberOwned = 0;
    public int maxOwned = int.MaxValue;

    public Button theButton = null;
    public Text priceText = null;
    public Text numberOwnedText = null;
    public Text maxOwnedText = null;

    public MonoBehaviour[] disableBehaviorsOnMax = null;
    public GameObject[] disableObjectsOnMax = null;

    public float _debugCurrentCost = 0;

    // Update is called once per frame
    void Update()
    {
        
        UpdateButtonInfo();
        _debugCurrentCost = Cost;
        
    }//Update

    void LateUpdate()
    {
        if(numberOwned >= maxOwned)
        {
            ShutDown();
        }//if
    }//lateUpdate

    private void ShutDown()
    {
        for (int i = 0; i < disableObjectsOnMax.Length; i++)
            disableObjectsOnMax[i].SetActive(false);

        for (int i = 0; i < disableBehaviorsOnMax.Length; i++)
            disableBehaviorsOnMax[i].enabled = false;

    }//ShutDown
    public void UpdateButtonInfo()
    {
        if (maxOwnedText != null)
            maxOwnedText.text = string.Format("{0}", maxOwned);

        if (numberOwnedText != null)
            numberOwnedText.text = string.Format("{0}", numberOwned);

        if (priceText != null && numberOwned < maxOwned)
        {
            priceText.text = string.Format("${0:n2}", Cost);
            //priceText.enabled = numberOwned < maxOwned;
        }//if

        if (theButton != null)
            theButton.interactable = ((GameManager.self.Resource >= Cost) && (numberOwned < maxOwned));
    }

    public float Cost
    {
        get
        {
            return Mathf.Round((baseCost * Mathf.Pow(costMult, numberOwned)));
        }
    }

    public void Buy() { DoPurchase(); } 
    public void DoPurchase()
    {
        GameManager.self.SubtractResource(Cost);
        numberOwned++;
        UpdateButtonInfo();
        gameObject.SendMessage("OnPurchased", SendMessageOptions.DontRequireReceiver);
    }//DoPurchase
}//Buyable
