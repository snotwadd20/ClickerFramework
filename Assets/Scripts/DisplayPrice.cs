using UnityEngine;
using UnityEngine.UI;

public class DisplayPrice : MonoBehaviour
{
    public Text textDisplayUI = null;
    public GameObject priceObject = null;
    public IPrice _po = null;

    public string prefix = "$";
    public string suffix = "";

    public int maximumDecimalPlaces = 2;

    // Use this for initialization
    void Start()
    {
        if (textDisplayUI == null)
        {
            textDisplayUI = gameObject.GetComponent<Text>();
            if (textDisplayUI == null)
                Debug.LogWarning("DisplayPrice requires a Text script be on the same gameobject, or that you provice a link to a Text object. Disabling." );
        }

        if (priceObject != null)
            _po = priceObject.GetComponent<IPrice>();
        else
        {
            _po = gameObject.GetComponentInChildren<IPrice>();
            
            if (_po == null)
            {
                _po = gameObject.GetComponentInParent<IPrice>();
            }
        }//else
        if(_po == null)
            Debug.LogWarning("DisplayPrice requires a script that implements IPrice be on the same gameobject, or that you provide a link to an IPrice-implmenting object. Disabling.");
        
        enabled = (textDisplayUI != null && _po != null);
    }

    private string _str = "";
    // Update is called once per frame
    void Update()
    {
        _str = "{0}{1:N"+ maximumDecimalPlaces + "}{2}";
        textDisplayUI.text = string.Format(_str,
            prefix,
            _po.Price,
            suffix);
    }
}
