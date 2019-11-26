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
	/// The sprites that represent which player has paused the game.
	/// </summary>
	public Sprite[] m_PausedPlayerImages;

	private RawImage m_PausedPlayerImageDisplay;

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

		m_PausedPlayerImageDisplay = transform.GetChild(0).GetChild(1).GetComponent<RawImage>();
	}

	/// <summary>
	/// Update.
	/// </summary>
	private void Update()
	{
		// If the game isn't paused, check input to pause.
		if (m_Paused == false)
		{
			// Check if a player pressed the pause button.
			// Then pause the game, remembering which player paused the game.
			// And set the pause menu to active, so it shows up on screen.

			if (XCI.GetButtonDown(XboxButton.Start, XboxController.First) ||
				XCI.GetButtonDown(XboxButton.Start, XboxController.Second) ||
				XCI.GetButtonDown(XboxButton.Start, XboxController.Third) ||
				XCI.GetButtonDown(XboxButton.Start, XboxController.Fourth))
			{
				m_Paused = true;
				Time.timeScale = 0.0f;
				m_PauseScreen.SetActive(true);

				// Check which player pressed the pause button.
				// Remember the number and display the corresponding texture.

				// First player.
				if (XCI.GetButtonDown(XboxButton.Start, XboxController.First) == true)
				{
					m_PausedPlayerNumber = 1;
					m_PausedPlayerImageDisplay.texture = m_PausedPlayerImages[0].texture;
				}
				// Second player.
				else if (XCI.GetButtonDown(XboxButton.Start, XboxController.Second) == true)
				{
					m_PausedPlayerNumber = 2;
					m_PausedPlayerImageDisplay.texture = m_PausedPlayerImages[1].texture;
				}
				// Third player.
				else if (XCI.GetButtonDown(XboxButton.Start, XboxController.Third) == true)
				{
					m_PausedPlayerNumber = 3;
					m_PausedPlayerImageDisplay.texture = m_PausedPlayerImages[2].texture;
				}
				// Fourth player.
				else if (XCI.GetButtonDown(XboxButton.Start, XboxController.Fourth) == true)
				{
					m_PausedPlayerNumber = 4;
					m_PausedPlayerImageDisplay.texture = m_PausedPlayerImages[3].texture;
				}
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
				m_Paused = false;
				m_PauseScreen.SetActive(false);
			}

			else if (XCI.GetButtonDown(XboxButton.Back, (XboxController)m_PausedPlayerNumber) == true)
			{
				Debug.Log("Return to menu!");
				Time.timeScale = 1.0f;
				m_Paused = false;
				m_PauseScreen.SetActive(false);
				GetComponent<ReturnToMenu>().ReturnMainMenuMenu();
			}
		}
	}
}