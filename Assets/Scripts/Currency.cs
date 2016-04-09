using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Currency : MonoBehaviour
{
    //---------------------------------------------------------------
    // The first currency to be detected that has a "CurrencyDefault"
    // script on it will be set as the Default here.
    //---------------------------------------------------------------
    public static Currency Default = null;

    //---------------------------------------------------------------
    // CLICK AND TICK RATE DATA
    //---------------------------------------------------------------
    private Dictionary<string, float> ClickRates;
    private Dictionary<string, float> TickRates;
    
    //---------------------------------------------------------------
    // DEBUG TOOLS FOR INSPECTOR
    //---------------------------------------------------------------
    public bool _doDebugChange = false;
    public float _debugOverwriteCurrency = 0;
    public float _debugAddToCurrency = 0;

    //---------------------------------------------------------------
    // ACCESSORS
    //---------------------------------------------------------------
    private float _amount = 0;
    public float Amount { get { return _amount; } }
    public float _AmountRO = 0;
    /// <summary>
    /// Calculates the total ClickRate by adding up all the different "sorted" bonuses in ClickRates
    /// </summary>
    private float _clickRate = 0f;
    private bool _clickRateIsDirty = true;
    public float ClickRate
    {
        get
        {
            if (_clickRateIsDirty)
            {
                _clickRateIsDirty = false;
                _clickRate = 0;
                foreach (KeyValuePair<string, float> mult in ClickRates)
                    _clickRate += mult.Value;
            }//if
            return _clickRate;
        }//get
    }//ClickRate

    /// <summary>
    /// Calculates the total TickRate by adding up all the different "sorted" bonuses in TickRates
    /// </summary>
    private float _tickRate = 0.0f;
    private bool tickRateIsDirty = true;
    public float TickRate
    {
        get
        {
            if (tickRateIsDirty)
            {
                tickRateIsDirty = false;
                _tickRate = 0;

                foreach (KeyValuePair<string, float> mult in TickRates)
                    _tickRate += mult.Value;
            }//if
            return _tickRate;
        }//get
    }//TickRate

    /// <summary>
    /// Gets a value out of the Dictionary ClickRates[] via the string "type"
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public float ClickRateByType(string type)
    {
        if (ClickRates.ContainsKey(type))
            return ClickRates[type];
        else
        {
            Debug.LogWarning("Currency - ClickRateByType can't find a ClickRate of type " + type + " returning 0.");
            return 0.0f;
        }//else
    }//ClickRateByType

    /// <summary>
    /// Gets a value out of the Dictionary TlickRates[] via the string "type"
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public float TickRateByType(string type)
    {
        if (TickRates.ContainsKey(type))
            return TickRates[type];
        else
        {
            Debug.LogWarning("Currency - TickRateByType can't find a TickRate of type " + type + " returning 0.");
            return 0.0f;
        }//else
    }//TickRateByType


    //---------------------------------------------------------------
    // METHODS TO CHANGE THE CURRENCY AMOUNT
    //---------------------------------------------------------------
    /// <summary>
    /// Adds "amount" to the currency total.
    /// </summary>
    /// <param name="amount"></param>
    /// <returns>float</returns>
    public float AddAmount(float amount)
    {
        _amount += amount;
        return _amount;
    }//AddAmount

    /// <summary>
    /// Multiplies the currency total by "factor".
    /// </summary>
    /// <param name="factor"></param>
    /// <returns>float</returns>
    public float MultiplyAmount(float factor)
    {
        _amount *= factor;
        return Amount;
    }//MultiplyAmount

    /// <summary>
    /// Subtracts "amount" from the currency total
    /// </summary>
    /// <param name="amount"></param>
    /// <returns></returns>
    public float SubtractAmount(float amount)
    {
        return AddAmount(-amount);
    }

    //---------------------------------------------------------------
    // METHODS THAT ALTER THE RATES OF REWARDS FOR TICKS AND CLICKS
    //---------------------------------------------------------------
    /// <summary>
    /// Adds "rateToAdd" to the total Tick Rate, filed under the "type" specified
    /// </summary>
    /// <param name="type"></param>
    /// <param name="rateToAdd"></param>
    public void AddToTickRate(string type, float rateToAdd)
    {
        if (rateToAdd == 0)
        {
            Debug.LogWarning(type + " - attempting to add 0 tick rate. Ignoring.\nDo a check first.");
            return;
        }//if

        if (TickRates.ContainsKey(type))
            TickRates[type] += rateToAdd;
        else
            TickRates.Add(type, rateToAdd);

        tickRateIsDirty = true;
    }//AddToTickRate

    /// <summary>
    /// Adds "rateToAdd" to the total Click Rate, filed under the "type" specified
    /// </summary>
    /// <param name="type"></param>
    /// <param name="rate"></param>
    public void AddToClickRate(string type, float rate)
    {
        if (rate == 0)
        {
            Debug.LogWarning(type + " - attempting to add 0 click rate. Ignoring.");
            return;
        }//if

        if (ClickRates.ContainsKey(type))
            ClickRates[type] += rate;
        else
            ClickRates.Add(type, rate);

        _clickRateIsDirty = true;
    }//AddToClickRate

    //---------------------------------------------------------------
    // MONOBEHAVIOUR METHODS
    //---------------------------------------------------------------
    // Use this for initialization
    void Awake()
    {
        ClickRates = new Dictionary<string, float>();
        TickRates = new Dictionary<string, float>();

        if (Currency.Default == null && gameObject.GetComponent<CurrencySetDefault>() != null)
            Currency.Default = this;
    }

    // Update is called once per frame
    void Update ()
    {

        _AmountRO = Amount;

        if (_doDebugChange)
        {
            if(_debugOverwriteCurrency > 0)
            {
                _amount = 0;
                AddAmount(_debugOverwriteCurrency);
            }
            AddAmount(_debugAddToCurrency);
            _doDebugChange = false;
        }//if
	}//Update
}//Currency
