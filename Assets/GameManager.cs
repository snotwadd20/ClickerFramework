using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public enum MultiplierKind { Click, Tick };

public class GameManager : MonoBehaviour
{
    //public float beginningResources = 0.0f;
    //public float beginningTickRate = 0.0f;
    public float beginningClickRate = 1.0f;

    //public Text resourceDisplay = null;
    //public Text tickRateDisplay = null;
    //public Text clickRateDispay = null;
    
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
    void Awake ()
    {
        if(_self == null)
            _self = this;

        ClickMultipliers = new Dictionary<string, float>();
        TickMultipliers = new Dictionary<string, float>();

        //AddResource(beginningResources);
        RegisterClickMult("startingClickRate", beginningClickRate);
        //RegisterTickMult("startingTickRate", beginningTickRate);
        
        //StartCoroutine("Tick");
    }

    public float MultiplyResource(float factor)
    {
        _resource *= factor;

        //if (resourceDisplay != null)
            //resourceDisplay.text = string.Format("{0:n1}", _resource);

        return Resource;
    }//MultiplyResource

    public float AddResource(float amount)
    {
        _resource += amount;
        //if (resourceDisplay != null)
            //resourceDisplay.text = string.Format("{0:n1}", _resource);

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
        if (multiplier == 0)
        {
            Debug.LogWarning(type + " - attempting to add 0 tick multiplier. Ignoring.\nDo a check first.");
            return;
        }//if

        if (TickMultipliers.ContainsKey(type))
            TickMultipliers[type] += multiplier;
        else
            TickMultipliers.Add(type, multiplier);//start at one and add to it

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
                _tickMultiplier = 0;
                foreach (KeyValuePair<string, float> mult in TickMultipliers)
                {
                       _tickMultiplier += mult.Value;

                    print(mult.Key + " -> " + mult.Value);

                }//foreach
                _tickMultiplier = (float)Math.Round(_tickMultiplier, 1); //Drop insignificant digits (floating point bs)

            }//if
            return _tickMultiplier;
        }//get
    }

    public void RegisterClickMult(string type, float multiplier)
    {
        if (multiplier == 0)
        {
            Debug.LogWarning(type + " - attempting to add 0 click multiplier. Ignoring.");
            return;
        }//if

        if (ClickMultipliers.ContainsKey(type))
            ClickMultipliers[type] += multiplier;
        else
            ClickMultipliers.Add(type, multiplier);//start at one and add to it

        _cMultIsDirty = true;
    }//RegisterClickMult

    private float _clickMultiplier = 0f;
    private bool _cMultIsDirty = true;
    public float TotalClickMultiplier
    {
        get
        {
            if (_cMultIsDirty)
            {
                _cMultIsDirty = false;
                _clickMultiplier = 0;
                foreach (KeyValuePair<string, float> mult in ClickMultipliers)
                {
                    _clickMultiplier += mult.Value;
                    
                    print(mult.Key + " -> " + mult.Value);
                }//foreach
                _clickMultiplier = (float)Math.Round(_clickMultiplier, 1); //Drop insignificant digits (floating point bs)
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

        /*if (clickRateDispay != null)
            clickRateDispay.text = TotalClickMultiplier + "/click";

        if (tickRateDisplay != null)
            tickRateDisplay.text = TotalTickMultiplier + "/s";
            */
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
