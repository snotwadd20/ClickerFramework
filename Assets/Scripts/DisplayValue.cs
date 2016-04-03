using UnityEngine;
using UnityEngine.UI;

public class DisplayValue : MonoBehaviour
{
    public Text textDisplayUI = null;
    public GameObject valueObject = null;
    public IValue _iObj = null;

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
                Debug.LogWarning("DisplayQuantity requires a Text script be on the same gameobject, or that you provice a link to a Text object. Disabling.");
        }

        if (valueObject != null)
            _iObj = valueObject.GetComponent<IValue>();
        else
        {
            _iObj = gameObject.GetComponentInChildren<IValue>();

            if (_iObj == null)
            {
                _iObj = gameObject.GetComponentInParent<IValue>();
            }
        }//else
        if (_iObj == null)
            Debug.LogWarning("DisplayPrice requires a script that implements IQuantity be in the same heirarchy, or that you provide a link to an IQuantity-implmenting object. Disabling.");

        enabled = (textDisplayUI != null && _iObj != null);
    }

    private string _str = "";
    // Update is called once per frame
    void Update()
    {
        _str = "{0}{1:N" + maximumDecimalPlaces + "}{2}";
        textDisplayUI.text = string.Format(_str,
            prefix,
            _iObj.Value,
            suffix);
    }
}
