using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FullscreenToggle: MonoBehaviour
{
	/// <summary>
	/// On startup.
	/// </summary>
	private void Awake()
	{
		GetComponent<Toggle>().isOn = Screen.fullScreen;
	}

	/// <summary>
	/// Toggle fullscreen.
	/// </summary>
	public void ToggleFullscreen()
	{
		if (Screen.fullScreen == true)
			Screen.fullScreen = false;
		else
			Screen.fullScreen = true;

		Debug.Log(Screen.fullScreen);
	}
}