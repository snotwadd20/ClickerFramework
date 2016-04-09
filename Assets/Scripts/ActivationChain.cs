using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ActivationChain: MonoBehaviour
{
    public ActivationChain nextLink = null;

    public float delayInSeconds = 1.0f;
    private bool _isDelaying = false;
    private float progress = 0;

    public MonoBehaviour progressBar = null;
    private IValue _pbValue = null; //Gets the progress bar as an IValue so it can change the bar's Value

    public GameObject[] alsoActivate = null;
    public MonoBehaviour[] alsoEnable = null;

    public bool isRoot = false;
    public bool deactivateSelf = true;
    // Use this for initialization

    public Button myButton = null;

    private ActivationChain _head = null;
    void Start ()
    {
        _head = nextLink;
        while(_head != null && _head != this && !_head.isRoot)
        {
            _head.gameObject.SetActive(false);
            _head = _head.nextLink;
        }//while

        if (progressBar != null)
        {
            _pbValue = progressBar.GetComponentInChildren<IValue>();

            if (_pbValue == null)
            {
                Debug.LogWarning("Progress bar game object does not have an attached script that implements IValue");
            }//else

            progressBar = (MonoBehaviour)_pbValue;
        }//if

        if(myButton == null)
        {
            myButton = gameObject.GetComponentInParent<Button>();
        }//if
    }//Start

    void Update()
    {
        if (_pbValue != null)
            _pbValue.Value = progress;
    }//Update

    private IEnumerator Delay()
    {
        

        if (!_isDelaying) 
        {
            if (myButton != null)
                myButton.interactable = false;

            float timer = 0.0f;
            _isDelaying = true;
            while (_isDelaying)
            {

                while (timer < delayInSeconds)
                {
                    timer += Time.deltaTime;
                    progress = timer / delayInSeconds;
                    yield return new WaitForEndOfFrame();
                }//while
                timer = 0;
                progress = 0;

                //yield return new WaitForSeconds(generateAfterSeconds);
                yield return new WaitForEndOfFrame();
                _isDelaying = false;
            }//while

            if (myButton != null)
                myButton.interactable = true;
        }//if
        else
        {
            Debug.LogWarning(this.GetType() + " is trying to start multiple Delay() coroutines on " + name);
        }//else
    }//Tick
    public void UnlockLinks()
    {
        setLinkActivityTo(true);
    }//UnlockLinks

    public void LockLinks()
    {
        setLinkActivityTo(false);
    }//LockLinks

    private void setLinkActivityTo(bool makeActive)
    {
        foreach (GameObject obj in alsoActivate)
            obj.SetActive(makeActive);

        foreach (MonoBehaviour bhvr in alsoEnable)
            bhvr.enabled = makeActive;
    }// LockLinks

    // OnPurchased and OnNextLink Messages do the same thing
    public void OnNextLink() 
    {
        StartCoroutine(DoNextLink());
    }//OnPurchased

    private IEnumerator DoNextLink()
    {
        if(delayInSeconds > 0)
        {
            StartCoroutine(Delay());
            //print("ASDF");
            yield return new WaitForEndOfFrame();
            yield return new WaitWhile(() => { return _isDelaying; });
        }//if
        
        if (nextLink != null)
        {
            nextLink.gameObject.SetActive(true);
        }//if

        UnlockLinks();
        
        gameObject.SetActive(!deactivateSelf);
        yield return new WaitForEndOfFrame();
    }
}//BuyableChain