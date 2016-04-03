using UnityEngine;
using UnityEngine.UI;

public class DisplayQuantAndMax: MonoBehaviour
{
    public Text textDisplayUI = null;
    public GameObject targetObject = null;
    private IQuantity _Qobj = null;
    private IMaximum _Mobj = null;

    public string prefix = "";
    public string separator = " / ";
    public string suffix = "";

    public int maximumDecimalPlaces = 0;

    public string noLimitCharacter = "∞";

    // Use this for initialization
    void Start()
    {
        if (textDisplayUI == null)
        {
            textDisplayUI = gameObject.GetComponent<Text>();
            if (textDisplayUI == null)
                Debug.LogWarning("DisplayQuantAndMax requires a Text script be on the same gameobject, or that you provice a link to a Text object. Disabling.");
        }

        //Note: This will probably pull the same object f or both _Mobj and _Qobj. That's ok.
        if (targetObject != null)
        {
            _Mobj = targetObject.GetComponent<IMaximum>();
            _Qobj = targetObject.GetComponent<IQuantity>();
        }
        else
        {
            //Check the children
            _Mobj = gameObject.GetComponentInChildren<IMaximum>();
            _Qobj = gameObject.GetComponentInChildren<IQuantity>();

            //Check the parent
            if (_Mobj == null)
                _Mobj = gameObject.GetComponentInParent<IMaximum>();
           
            if(_Qobj == null)
                _Qobj = gameObject.GetComponentInParent<IQuantity>();

        }//else


        if (_Mobj == null || _Qobj == null)
            Debug.LogWarning("DisplayQuantAndMax requires a script that implements IMaximum AND IQuantity be be in the same heirarchy, or that you provide a link to an implementing object. Disabling.");
        
        enabled = (textDisplayUI != null && _Qobj != null && _Mobj != null);
    }

    private string _str = "";
    // Update is called once per frame
    void Update()
    {
        if (_Mobj.Maximum != int.MaxValue)
        {
            _str = "{0}{1:N" + maximumDecimalPlaces + "}{2}{3:N" + maximumDecimalPlaces + "}{4}";
            textDisplayUI.text = string.Format(_str,
            prefix,
            _Qobj.Quantity,
            separator,
            _Mobj.Maximum,
            suffix);
        }//if
        else
        {
            _str = "{0}{1:N" + maximumDecimalPlaces + "}{2}{3}{4}";
            textDisplayUI.text = string.Format(_str,
            prefix,
            _Qobj.Quantity,
            separator,
            noLimitCharacter,
            suffix);
        }//else
    }//Update
}//DisplayQuantAndMax
