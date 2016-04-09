using UnityEngine;
using System.Collections;

public class BuyableChain : ActivationChain
{
	public void OnPurchased ()
    {
        if(nextLink != null)
            nextLink.gameObject.SetActive(true);

        UnlockLinks();

        gameObject.SetActive(false); 
	}//OnPurchased
}//BuyableChain