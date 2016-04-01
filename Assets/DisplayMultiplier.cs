using UnityEngine;
using UnityEngine.UI;

public class DisplayMultiplier : MonoBehaviour
{
    public Text textDisplayUI = null;
    public MultiplierKind multiplierKind = MultiplierKind.Click;

    public string prefix = "";
    public string separator = "/";
    public string suffix = "";

    public int maximumDecimalPlaces = 2;

    // Use this for initialization
    void Start ()
    {
        if (textDisplayUI == null)
            textDisplayUI = gameObject.GetComponent<Text>();

        enabled = textDisplayUI != null;
	}
	
	// Update is called once per frame
	void Update ()
    {
        float multiplier = 0;

        if (multiplierKind == MultiplierKind.Tick && GameManager.self)
        {
            multiplier = GameManager.self.TotalTickMultiplier;
        }//if
        else if (multiplierKind == MultiplierKind.Click && GameManager.self)
        {
            multiplier = GameManager.self.TotalClickMultiplier;
        }//else if

        if (multiplier >= 1 || multiplier == 0)
            textDisplayUI.text = string.Format("${0:N" + maximumDecimalPlaces + "}{1}{2}", multiplier, separator, (multiplierKind.ToString()));
        else
            textDisplayUI.text = string.Format("{0:N0}¢{1}{2}", multiplier * 100, separator, multiplierKind.ToString());
    }
}
