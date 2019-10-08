using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
	// On startup.
	void Awake()
	{
		// Don't destroy this object when loading other scenes.
		DontDestroyOnLoad(gameObject);
	}
}