using UnityEngine;
using UnityEngine.UI;

public class DisplayResource : MonoBehaviour
{
    public Text textDisplayUI = null;

    // Use this for initialization
    void Start()
    {
        if (textDisplayUI == null)
            textDisplayUI = gameObject.GetComponent<Text>();

        enabled = textDisplayUI != null;
    }

    // Update is called once per frame
    void Update()
    {
        textDisplayUI.text = "$" + string.Format("{0:N2}", GameManager.self.Resource);
    }
}
