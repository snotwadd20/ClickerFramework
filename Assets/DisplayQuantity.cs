using UnityEngine;
using UnityEngine.UI;

public class DisplayQuantity : MonoBehaviour
{
    public Text textDisplayUI = null;
    public GameObject quantityObject = null;
    public IQuantity _qo = null;

    public string prefix = "";
    public string suffix = "";

    public int maximumDecimalPlaces = 2;

    // Use this for initialization
    void Start()
    {
        if (textDisplayUI == null)
        {
            textDisplayUI = gameObject.GetComponent<Text>();
            if (textDisplayUI == null)
                Debug.LogWarning("DisplayQuantity requires a Text script be on the same gameobject, or that you provice a link to a Text object. Disabling." );
        }

        if (quantityObject != null)
            _qo = quantityObject.GetComponent<IQuantity>();
        else
        {
            _qo = gameObject.GetComponentInChildren<IQuantity>();
            
            if (_qo == null)
            {
                _qo = gameObject.GetComponentInParent<IQuantity>();
            }
        }//else
        if(_qo == null)
            Debug.LogWarning("DisplayPrice requires a script that implements IQuantity be in the same heirarchy, or that you provide a link to an IQuantity-implmenting object. Disabling.");
        
        enabled = (textDisplayUI != null && _qo != null);
    }

    private string _str = "";
    // Update is called once per frame
    void Update()
    {
        _str = "{0}{1:N"+ maximumDecimalPlaces + "}{2}";
        textDisplayUI.text = string.Format(_str,
            prefix,
            _qo.Quantity,
            suffix);
    }
}
