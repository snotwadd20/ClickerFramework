using UnityEngine;
using UnityEngine.UI;

public class DisplayResource : MonoBehaviour
{
    public Text textDisplayUI = null;
    public string prefix = "$";
    public string suffix = "";

    public int maximumDecimalPlaces = 2;

    // Use this for initialization
    void Start()
    {
        if (textDisplayUI == null)
            textDisplayUI = gameObject.GetComponent<Text>();

        enabled = textDisplayUI != null;
    }

    private string _str = "";
    // Update is called once per frame
    void Update()
    {
        _str = "{0}{1:N" + maximumDecimalPlaces + "}{2}";
        textDisplayUI.text =  string.Format(_str, prefix, GameManager.self.Resource, suffix);
    }
}
