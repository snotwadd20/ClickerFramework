using UnityEngine;
using UnityEngine.UI;

public class DisplayCurrencyRate : MonoBehaviour
{
    public enum RateKind { Click, Tick };
    public RateKind rateKind = RateKind.Click;
    public Currency currency = null;
    public Text textDisplayUI = null;

    public string bonusKeyword = ""; //Leave this empty if you want to pull the whole rate. Add it if you want to see a specific type of rate.

    public string prefix = "";
    public string separator = "/";
    public string suffix = "";

    public int maximumDecimalPlaces = 2;

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

        if (textDisplayUI == null)
            textDisplayUI = gameObject.GetComponent<Text>();

        enabled = textDisplayUI != null && currency != null;
	}
	
    private float _tempRate = 0;
	// Update is called once per frame
	void Update ()
    {
        if (bonusKeyword != "")
        {
            if (rateKind == RateKind.Tick)
                _tempRate = currency.TickRateByType("");
            else if (rateKind == RateKind.Click)
                _tempRate = currency.ClickRateByType(bonusKeyword);
        }
        else
        {
            if (rateKind == RateKind.Tick)
                _tempRate = currency.TickRate;
            else if (rateKind == RateKind.Click)
                _tempRate = currency.ClickRate;
        }//else
        if (_tempRate >= 1)
            textDisplayUI.text = string.Format("{0}${1:N" + maximumDecimalPlaces + "}{2}{3}", prefix, _tempRate, separator, suffix);
        else
            textDisplayUI.text = string.Format("{0}{1:N0}¢{2}{3}", prefix, _tempRate * 100, separator, suffix);
    }//Update
}//DisplayCurrencyRate
