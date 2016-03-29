using UnityEngine;
using System.Collections;

public class ClickForResource : MonoBehaviour
{
    public bool specifyAmount = false;
    public int resourceAmount = 1;
    
    public void OnClick()
    {
        if (!specifyAmount)
            GameManager.self.AddResource(GameManager.self.TotalClickMultiplier);
        else
            GameManager.self.AddResource(resourceAmount);
    }//OnClick
}//ClickForResource
