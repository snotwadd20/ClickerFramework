using UnityEngine;
using System.Collections;

public class BuyableChain : ActivationChain
{
	public void OnPurchased ()
    {
        OnNextLink(); 
	}//OnPurchased
}//BuyableChain