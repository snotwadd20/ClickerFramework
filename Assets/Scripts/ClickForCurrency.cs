using UnityEngine;

public class ClickForCurrency : MonoBehaviour, IValue
{
    public Currency currency = null;
    public float amountPerClick = 1.0f;
    public bool useGlobalClickRate = true;

    public float Value { get { return ( useGlobalClickRate ? (currency.ClickRate + amountPerClick) : amountPerClick); } set { amountPerClick = value; } }
    public void Start()
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

    public void OnClick()
    {
        currency.AddAmount(Value);
        //print(currency.ClickRate);
    }//OnClick
}//ClickForResource
