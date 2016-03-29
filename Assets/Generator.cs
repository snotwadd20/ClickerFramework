using UnityEngine;
using System.Collections;

public class Generator : MonoBehaviour
{
    public float resourcePerSecond = 0.1f;

    public float bonusCash = 0.0f;

    public float generateAfterSeconds = 1.0f;

    public bool startImmediately = false;

    //public float levelUpMultiplier = 0.1f;
    public int currentLevel = 0;

    public bool printTick = false;
    public void OnPurchased()
    {
        if (!startImmediately && currentLevel == 0)
            StartCoroutine(Tick());

        currentLevel++;
    }//OnPurchase

	// Use this for initialization
	void Start ()
    {
        GameManager.self.AddResource(bonusCash);

        if(startImmediately)
            StartCoroutine(Tick());

    }

   

    public IEnumerator Tick()
    {
        while (true)
        {
            float resources = (resourcePerSecond * currentLevel);
            GameManager.self.AddResource(resources);

            if (printTick)
                print(name + " -> TICK (" + generateAfterSeconds + "s) for $" + resources);

            yield return new WaitForSeconds(generateAfterSeconds);
        }//while

    }//Tick
}
