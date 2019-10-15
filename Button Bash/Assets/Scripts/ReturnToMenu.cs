using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using XboxCtrlrInput;

public class ReturnToMenu : MonoBehaviour
{
	// The return to menu event.
	UnityEvent m_ReturnToMenuEvent;

	// On startup.
	private void Awake()
	{
		// Set the return to menu event to be a unity event.
		m_ReturnToMenuEvent = new UnityEvent();

		// Add the return to main menu function to the event.
		m_ReturnToMenuEvent.AddListener(ReturnMainMenuMenu);
	}

	private void Update()
	{
		if (XCI.GetButtonDown(XboxButton.A))
		{
			ReturnMainMenuMenu();
		}
	}

	// Return the game to the main menu.
	public void ReturnMainMenuMenu()
	{
		// Load the first scene (should be the main menu.)
		SceneManager.LoadScene(0);
	}
}
