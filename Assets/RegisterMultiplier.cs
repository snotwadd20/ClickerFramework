using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RegisterMultiplier : MonoBehaviour
{
    public MultiplierKind kindOfMultiplier = MultiplierKind.Tick;
    public string bonusKeyword = "general bonus";
    public float amountToAddToMultiplier = 0.0f;

    public Text displayAmountToAdd = null;

    void Start()
    {
        if (displayAmountToAdd != null)
            displayAmountToAdd.text = string.Format("{0:n1}", amountToAddToMultiplier);
    }
    void OnClick()
    {
        if (kindOfMultiplier == MultiplierKind.Click)
            GameManager.self.RegisterClickMult(bonusKeyword, amountToAddToMultiplier);

        else if (kindOfMultiplier == MultiplierKind.Tick)
            GameManager.self.RegisterTickMult(bonusKeyword, amountToAddToMultiplier);

        
    }
}
