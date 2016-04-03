using UnityEngine;
using System.Collections;

public class ScaleGeneratorSpeed : MonoBehaviour
{
    public Generator generator = null;
    public float speedMultiplier = 2.0f;
    
    void Start()
    {
        if(generator == null)
        {
            Debug.LogError(this.GetType() + ": No generator attached to" + gameObject.name + ". Disabling.");
            enabled = false;
        }//if
    }//Start

    public void Activate() { OnPurchased(); }
    public void OnPurchased()
    {
        generator.tickDurationInSeconds /= speedMultiplier;
    }//OnPurchased
}
