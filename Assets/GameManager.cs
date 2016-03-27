using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public Text resourceDisplay = null;
    public Text tickRateDisplay = null;
    public Text clickRateDispay = null;

    private float _resource = 0;

    public float _debugOverwriteResource = 0;
    public float _debugAddToResource = 0;
    public bool _doDebugChange = false;

    public float Resource
    {
        get
        {
            return _resource;
        }
    }

    private Dictionary<string, float> ClickMultipliers;
    private Dictionary<string, float> TickMultipliers;
    
    // Use this for initialization
    void Start ()
    {
        if(_self == null)
            _self = this;

        ClickMultipliers = new Dictionary<string, float>();
        TickMultipliers = new Dictionary<string, float>();

        StartCoroutine("Tick");
    }

    public float AddResource(float amount)
    {
        _resource += amount;
        if (resourceDisplay != null)
            resourceDisplay.text = string.Format("{0:n0}", _resource);

        return _resource;
    }//AddResource

    public float SubtractResource(float amount)
    {
        return AddResource(-amount);
    }

    public IEnumerator Tick()
    {
        while (true)
        { 
            AddResource( TotalTickMultiplier);

            yield return new WaitForSeconds(1.0f);
        }//while

    }//Tick

    public void RegisterTickMult(string type, float multiplier)
    {
        if (TickMultipliers.ContainsKey(type))
            TickMultipliers[type] += multiplier;
        else
            TickMultipliers.Add(type, 1.0f+multiplier);//start at one and add to it

        _tMultIsDirty = true;
    }//RegisterTickMult

    private float _tickMultiplier = 0.0f;
    private bool _tMultIsDirty = true;
    public float TotalTickMultiplier
    {
        get
        {
            if (_tMultIsDirty)
            {
                _tMultIsDirty = false;
                _tickMultiplier = 1;
                foreach (KeyValuePair<string, float> mult in TickMultipliers)
                {
                    _tickMultiplier *= mult.Value;
                }//foreach
                _tickMultiplier -= 1;
            }//if
            return _tickMultiplier;
        }//get
    }

    public void RegisterClickMult(string type, float multiplier)
    {
        if (ClickMultipliers.ContainsKey(type))
            ClickMultipliers[type] += multiplier;
        else
            ClickMultipliers.Add(type, 1.0f + multiplier);//start at one and add to it

        _cMultIsDirty = true;
    }//RegisterClickMult

    private float _clickMultiplier = 1.0f;
    private bool _cMultIsDirty = true;
    public float TotalClickMultiplier
    {
        get
        {
            if (_cMultIsDirty)
            {
                _cMultIsDirty = false;
                _clickMultiplier = 1;
                foreach (KeyValuePair<string, float> mult in ClickMultipliers)
                {
                    _clickMultiplier *= mult.Value;
                }//foreach
                _clickMultiplier -= 1;
            }//if
            return _clickMultiplier;
        }//get
    }
	// Update is called once per frame
	void Update ()
    {
        if (_doDebugChange)
        {
            if(_debugOverwriteResource > 0)
            {
                _resource = 0;
                AddResource(_debugOverwriteResource);
            }
            AddResource(_debugAddToResource);
            _doDebugChange = false;
        }//if

        if (clickRateDispay != null)
            clickRateDispay.text = TotalClickMultiplier + "/click";

        if (tickRateDisplay != null)
            tickRateDisplay.text = TotalTickMultiplier + "/s";

	}

    //SINGLETON
    public static GameManager _self = null;

    public static GameManager self
    {
        get
        {
            if (_self == null)
                InitSingleton();

            return _self;
        }//get
    }//self

    private static void InitSingleton()
    {
        _self = new GameObject("GameManager").AddComponent<GameManager>();
    }//InitSingleton

}
