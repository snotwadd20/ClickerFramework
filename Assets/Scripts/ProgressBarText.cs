using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ProgressBarText : MonoBehaviour, IValue
{
    public enum DisplayType { Percent, Factor }
    public DisplayType type = DisplayType.Percent;

    public Text displayProgress = null;
    public string prefix = "";
    public string postFix = "%";
    public int decimalPlaces = 0;

    private float _value = 0;
    public float Value
    {
        get { return _value; }
        set
        {
            if(displayProgress != null)
                displayProgress.text = string.Format("{0}{1}{2}", prefix, (type == DisplayType.Percent ? Mathf.RoundToInt(Mathf.Min(100 * value, 100)) : value), postFix);

            _value = value;
        }
    }


    // Use this for initialization
    void Start ()
    {
        if (displayProgress == null)
            displayProgress = gameObject.GetComponent<Text>();

        if(displayProgress == null)
        {
            Debug.LogWarning("ProgressBarText script is not linked to or placed on an object with a Text object. Disabling.");
            enabled = false;
        }
	}
    
	// Update is called once per frame
	void Update ()
    {
            
        
	}
}//ProgressBarText
