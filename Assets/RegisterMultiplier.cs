using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RegisterMultiplier : MonoBehaviour
{
    public MultiplierKind kindOfMultiplier = MultiplierKind.Tick;
    public string bonusKeyword = "general bonus";
    public float increaseBy = 0.0f;

    public Text displayIncreaseByText = null;

    void Start()
    {
        if (displayIncreaseByText != null)
            displayIncreaseByText.text = string.Format("${0:n2}", increaseBy);
    }
    void OnClick()
    {
        if (kindOfMultiplier == MultiplierKind.Click)
            GameManager.self.RegisterClickMult(bonusKeyword, increaseBy);

        else if (kindOfMultiplier == MultiplierKind.Tick)
            GameManager.self.RegisterTickMult(bonusKeyword, increaseBy);

        
    }
}
