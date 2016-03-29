using UnityEngine;
using System.Collections;

public class MultiplyResource : MonoBehaviour
{
    public float multiplyFactor = 2.0f;
	
	void OnClick ()
    {
        GameManager.self.MultiplyResource(multiplyFactor);
	}//OnClick
}//MultiplyResource
