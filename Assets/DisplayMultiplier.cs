using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DisplayMultiplier : MonoBehaviour
{
    public Text textDisplayUI = null;
    public MultiplierKind multiplierKind = MultiplierKind.Click;

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

        if (multiplierKind == MultiplierKind.Tick)
        {
            multiplier = GameManager.self.TotalTickMultiplier;
        }//if
        else if (multiplierKind == MultiplierKind.Click)
        {
            multiplier = GameManager.self.TotalClickMultiplier;
        }//else if

        textDisplayUI.text = "Generate: $" + string.Format("{0:n1}", multiplier +" / " + (multiplierKind.ToString()));

    }
}
