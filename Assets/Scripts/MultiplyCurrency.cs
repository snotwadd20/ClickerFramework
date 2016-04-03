using UnityEngine;
using System.Collections;

public class MultiplyCurrency : MonoBehaviour
{
    public Currency currency = null;
    public float multiplyFactor = 2.0f;
	
	void OnClick ()
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

        currency.MultiplyAmount(multiplyFactor);
    }//OnClick
}//MultiplyResource
