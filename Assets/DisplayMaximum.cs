using UnityEngine;
using UnityEngine.UI;

public class DisplayMaximum : MonoBehaviour
{
    public Text textDisplayUI = null;
    public GameObject maximumObject = null;
    public IMaximum _obj = null;

    public string prefix = "";
    public string suffix = "";

    public int maximumDecimalPlaces = 0;

    // Use this for initialization
    void Start()
    {
        if (textDisplayUI == null)
        {
            textDisplayUI = gameObject.GetComponent<Text>();
            if (textDisplayUI == null)
                Debug.LogWarning("DisplayPrice requires a Text script be on the same gameobject, or that you provice a link to a Text object. Disabling." );
        }

        if (maximumObject != null)
            _obj = maximumObject.GetComponent<IMaximum>();
        else
        {
            _obj = gameObject.GetComponentInChildren<IMaximum>();
            
            if (_obj == null)
            {
                _obj = gameObject.GetComponentInParent<IMaximum>();
            }
        }//else
        if(_obj == null)
            Debug.LogWarning("DisplayMaximum requires a script that implements IMaximum be be in the same heirarchy, or that you provide a link to an IMaximum-implmenting object. Disabling.");
        
        enabled = (textDisplayUI != null && _obj != null);
    }

    private string _str = "";
    // Update is called once per frame
    void Update()
    {
        _str = "{0}{1:N"+ maximumDecimalPlaces + "}{2}";
        textDisplayUI.text = string.Format(_str,
            prefix,
            _obj.Maximum,
            suffix);
    }
}
