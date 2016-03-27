using UnityEngine;
using System.Collections;

public class ClickForResource : MonoBehaviour
{
    public void OnClick()
    {
        GameManager.self.AddResource(GameManager.self.TotalClickMultiplier);
    }//OnClick
}//ClickForResource
