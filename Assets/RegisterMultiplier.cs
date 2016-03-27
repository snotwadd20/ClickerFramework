using UnityEngine;
using System.Collections;

public class RegisterMultiplier : MonoBehaviour
{
    public enum MultiplierKind { Click, Tick };
    public MultiplierKind kindOfMultiplier = MultiplierKind.Tick;
    public string bonusKeyword = "general bonus";
    public float amountToAddToMultiplier = 0;


    void OnClick()
    {
        if (kindOfMultiplier == MultiplierKind.Click)
            GameManager.self.RegisterClickMult(bonusKeyword, amountToAddToMultiplier);
        else if (kindOfMultiplier == MultiplierKind.Tick)
            GameManager.self.RegisterTickMult(bonusKeyword, amountToAddToMultiplier);
    }
}
