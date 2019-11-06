using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
	/// Update.
	/// </summary>
	private void Update()
	{
		// If the game isn't paused, check input to pause.
		if (m_Paused == false)
		{
			// Check which player pressed the pause button.
			// Then pause the game, remembering which one player paused the game.

			if (XCI.GetButtonDown(XboxButton.Start, XboxController.First) == true)
			{
				m_Paused = true;
				Time.timeScale = 0.0f;
				m_PausedPlayerNumber = 1;
			}
			else if (XCI.GetButtonDown(XboxButton.Start, XboxController.Second) == true)
			{
				m_Paused = true;
				Time.timeScale = 0.0f;
				m_PausedPlayerNumber = 2;
			}
			else if (XCI.GetButtonDown(XboxButton.Start, XboxController.Third) == true)
			{
				m_Paused = true;
				Time.timeScale = 0.0f;
				m_PausedPlayerNumber = 3;
			}
			else if (XCI.GetButtonDown(XboxButton.Start, XboxController.Fourth) == true)
			{
				m_Paused = true;
				Time.timeScale = 0.0f;
				m_PausedPlayerNumber = 4;
			}
		}
		// Else if the game is paused, check if the player that paused the game pressed the pause button.
		else if (m_Paused == true)
		{ 
			if (XCI.GetButtonDown(XboxButton.Start, (XboxController)m_PausedPlayerNumber) == true)
			{
				// Reset time and assign the player number to something not correlating to a controller.
				Time.timeScale = 1.0f;
				m_PausedPlayerNumber = -1;
				m_Paused = false;
			}
		}
	}
}