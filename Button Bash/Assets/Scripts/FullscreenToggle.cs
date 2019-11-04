using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowedOnOff : MonoBehaviour
{
	/// <summary>
	/// Toggle fullscreen.
	/// </summary>
	public void SetWindowed()
	{
		if (Screen.fullScreen == true)
			Screen.fullScreen = false;
		else
			Screen.fullScreen = true;
	}
}
