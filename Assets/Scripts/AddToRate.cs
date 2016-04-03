using UnityEngine;
using System.Collections;

public class AddToRate : MonoBehaviour
{
    public enum RateKind { Click, Tick };
    public RateKind rateKind = RateKind.Click;
    public Currency currency = null;

    public string bonusKeyword = "default";

    public float amount = 1;
    public bool addOnStart = false;

    // Use this for initialization
    void Start ()
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

        if (addOnStart)
            DoRateAdd();
	}//Start
	
    private void DoRateAdd()
    {
        if(rateKind == RateKind.Click)
        {
            currency.AddToClickRate(bonusKeyword, amount);
        }//if
        else if(rateKind == RateKind.Tick)
        {
            currency.AddToTickRate(bonusKeyword, amount);
        }//else
    }//AddCurrency

	void OnClick()
    {
        DoRateAdd();
        print("T:" + currency.TickRate + " C : " + currency.ClickRate );
    }//OnClick
}//AddToRate
