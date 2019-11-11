using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XboxCtrlrInput;

public class GamePause : MonoBehaviour
{
	/// <summary>
	/// If the game is paused, default to false.
	/// </summary>
	private bool m_Paused = false;

	/// <summary>
	/// The number of the player that paused the game.
	/// </summary>
	private int m_PausedPlayerNumber;

	/// <summary>
	/// The pause screen.
	/// </summary>
	private GameObject m_PauseScreen;

	/// <summary>
	/// On startup.
	/// </summary>
	private void Awake()
	{
		// Get the pause screen.
		m_PauseScreen = transform.GetChild(0).gameObject;

		// If the pause screen is active on startup, set it to inactive.
		// We don't want the pause screen up while the game isn't paused when the scene is loaded.
		if (m_PauseScreen.activeSelf == true)
			m_PauseScreen.SetActive(false);
	}

	/// <summary>
	/// Update.
	/// </summary>
	private void Update()
	{
		// If the game isn't paused, check input to pause.
		if (m_Paused == false)
		{
			// Check which player pressed the pause button.
			// Then pause the game, remembering which one player paused the game.
			// And set the pause menu to active, so it shows up on screen.

			if (XCI.GetButtonDown(XboxButton.Start, XboxController.First) == true)
			{
				m_Paused = true;
				Time.timeScale = 0.0f;
				m_PausedPlayerNumber = 1;
				m_PauseScreen.SetActive(true);
				GetComponentInChildren<Text>().text = "Paused by Player " + m_PausedPlayerNumber;
			}
			else if (XCI.GetButtonDown(XboxButton.Start, XboxController.Second) == true)
			{
				m_Paused = true;
				Time.timeScale = 0.0f;
				m_PausedPlayerNumber = 2;
				m_PauseScreen.SetActive(true);
				GetComponentInChildren<Text>().text = "Paused by Player " + m_PausedPlayerNumber;
			}
			else if (XCI.GetButtonDown(XboxButton.Start, XboxController.Third) == true)
			{
				m_Paused = true;
				Time.timeScale = 0.0f;
				m_PausedPlayerNumber = 3;
				m_PauseScreen.SetActive(true);
				GetComponentInChildren<Text>().text = "Paused by Player " + m_PausedPlayerNumber;
			}
			else if (XCI.GetButtonDown(XboxButton.Start, XboxController.Fourth) == true)
			{
				m_Paused = true;
				Time.timeScale = 0.0f;
				m_PausedPlayerNumber = 4;
				m_PauseScreen.SetActive(true);
				GetComponentInChildren<Text>().text = "Paused by Player " + m_PausedPlayerNumber;
			}
		}
		// Else if the game is paused, check if the player that paused the game pressed the pause button.
		else if (m_Paused == true)
		{ 
			// If the player that paused the game presses the start button, unpause the game.
			// Set the pause menu to inactive.
			if (XCI.GetButtonDown(XboxButton.Start, (XboxController)m_PausedPlayerNumber) == true)
			{
				// Reset time and assign the player number to something not correlating to a controller.
				Time.timeScale = 1.0f;
				m_PausedPlayerNumber = -1;
				m_Paused = false;
				m_PauseScreen.SetActive(false);
			}

			if (XCI.GetButtonDown(XboxButton.A, (XboxController)m_PausedPlayerNumber) == true)
			{
				Debug.Log("Return to menu!");
				GetComponent<ReturnToMenu>().ReturnMainMenuMenu();
			}
		}
	}
}