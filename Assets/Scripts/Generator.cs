using UnityEngine;
using System.Collections;

public class Generator : MonoBehaviour
{
    public enum GeneratorType { Click, Tick }
    //---------------------------------------------
    // VARIABLES AND ACCESSORS
    //---------------------------------------------
    public Currency currency = null;
    public GeneratorType generatorType = GeneratorType.Tick;
    private GeneratorType _cachedType = GeneratorType.Tick;

    /// <summary>
    /// This should be a script that implements IValue
    /// </summary>
    public MonoBehaviour progressBar = null;
    private IValue _pbValue = null; //Gets the progress bar as an IValue so it can change the bar's Value

    public float amountPerLevelPerTick = 0.1f;
    public float tickDurationInSeconds = 1.0f; 

    public bool useGlobalTickRate = false;

    public int currentLevel = 0;

    //public bool activateAfterClick = false;
    
    public bool _debugPrintTick = false;

    private float _progress = 0.0f;
    public float progress { get { return _progress; } set { _progress = value; } }

    private bool _tickStarted = false;
    //public bool tickStarted { get { return _tickStarted; } }

    private bool _clickInProgress = false;

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
        _cachedType = generatorType;

        if (generatorType == GeneratorType.Tick)
            StartCoroutine(Tick());
    }

    void Update()
    {
        if(generatorType != _cachedType)
        {
            ChangeGeneratorType(generatorType);
        }//if

        if (_pbValue != null && currentLevel > 0)
        {
            _pbValue.Value = progress;
        }//if

    }//Update

    public void ChangeGeneratorType(GeneratorType type)
    {
        generatorType = type;
        _cachedType = generatorType;

        if (type == GeneratorType.Tick && !_tickStarted)
            StartCoroutine(Tick());
    }

    public float TotalAmountPerClick(int level)
    {
        return (amountPerLevelPerTick * level + ((useGlobalTickRate) ? currency.TickRate : 0)); 
    }

    private IEnumerator Tick()
    {
        if (!_tickStarted) // Throw it away if there's  already a Tick going. Just to be sure we don't end up with multiples per generator.
        {
            float timer = 0.0f;
            _tickStarted = true;
            while (generatorType == GeneratorType.Tick)
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
            _tickStarted = false;
        }//if
        else
        {
            Debug.LogWarning(this.GetType() + " is trying to start multiple Tick() coroutines");
        }//else
    }//Tick

    private IEnumerator Click()
    {
        if (!_clickInProgress && generatorType == GeneratorType.Click) //Throw it away if there's already been a click
        {
            _clickInProgress = true;
            float timer = 0.0f;

            if (_debugPrintTick)
                print(name + " -> TICK (" + tickDurationInSeconds + "s) for $" + TotalAmountPerClick(currentLevel));

            while (timer < tickDurationInSeconds && generatorType == GeneratorType.Click)
            {
                timer += Time.deltaTime;
                progress = timer / tickDurationInSeconds;
                yield return new WaitForEndOfFrame();
            }//while
            timer = 0;
            currency.AddAmount(TotalAmountPerClick(currentLevel));
            _clickInProgress = false;
        }
        yield return new WaitForEndOfFrame();
    }//Click

    public void OnClick()
    {
        if(generatorType == GeneratorType.Click)
        {
            StartCoroutine(Click());
        }//if
    }//OnClick

    
    
}
