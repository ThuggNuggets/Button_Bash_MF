using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using XboxCtrlrInput;

public class ReturnToMenu : MonoBehaviour
{
	// Return the game to the main menu.
	public void ReturnMainMenuMenu()
	{
		// Load the first scene (should be the main menu.)
		SceneManager.LoadScene(0);
	}
}
