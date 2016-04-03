using UnityEngine;
using System.Collections;

public class Generator : MonoBehaviour
{
    //---------------------------------------------
    // VARIABLES AND ACCESSORS
    //---------------------------------------------
    public Currency currency = null;

    /// <summary>
    /// This should be a script that implements IValue
    /// </summary>
    public MonoBehaviour progressBar = null;
    private IValue _pbValue = null; //Gets the progress bar as an IValue so it can change the bar's Value

    public float amountPerLevelPerTick = 0.1f;
    public float tickDurationInSeconds = 1.0f; 

    public bool useGlobalTickRate = false;

    public int currentLevel = 0;
    
    
    public bool _debugPrintTick = false;

    private float _progress = 0.0f;
    public float progress { get { return _progress; } set { _progress = value; } }

    private bool _isStarted = false;
    public bool isStarted { get { return _isStarted; } }

    //---------------------------------------------
    // METHODS
    //---------------------------------------------
    public void OnPurchased()
    {
        currentLevel++;
    }//OnPurchased

    //public MonoBehaviour behavior = null;
	// Use this for initialization
	void Start ()
    {
        if (currency == null && Currency.Default == null)
        {
            Debug.LogWarning(this.GetType() + ": No Currency specified. Disabling.");
            enabled = false;
            return;
        }//if
        else if (currency == null && Currency.Default != null)
        {
            currency = Currency.Default;
        }//else if

        if(progressBar != null)
        {
            _pbValue = progressBar.GetComponentInChildren<IValue>();
         
            if (_pbValue == null)
            {
                Debug.LogWarning("Progress bar game object does not have an attached script that implements IValue");
            }//else

            progressBar = (MonoBehaviour)_pbValue;
        }

        StartCoroutine(Tick());
    }

    void Update()
    {
        if (_pbValue != null && currentLevel > 0)
        {
            _pbValue.Value = progress;
        }//if

    }//Update

    public float TotalAmountPerClick(int level)
    {
        return (amountPerLevelPerTick * level + ((useGlobalTickRate) ? currency.TickRate : 0)); 
    }

    public IEnumerator Tick()
    {
        float timer = 0.0f;
        _isStarted = true;
        while (true)
        {
            if (_debugPrintTick)
                print(name + " -> TICK (" + tickDurationInSeconds + "s) for $" + TotalAmountPerClick(currentLevel));

            while (timer < tickDurationInSeconds)
            {
                timer += Time.deltaTime;
                progress = timer / tickDurationInSeconds;
                yield return new WaitForEndOfFrame();
            }//while
            timer = 0;
            currency.AddAmount(TotalAmountPerClick(currentLevel));

            //yield return new WaitForSeconds(generateAfterSeconds);
            yield return new WaitForEndOfFrame();

        }//while

    }//Tick

    
    
}
