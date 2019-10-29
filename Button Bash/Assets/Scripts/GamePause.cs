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
		// If someone presses the pause button, 
		// pause the game and assign who pressed the pause button.
		if (XCI.GetButtonDown(XboxButton.Start) == true && m_Paused == false)
		{
			// Will slow the game down the point that nothing will move.
			Time.timeScale = 0.0f;

			// Checks who pressed the pause button.
			if (XCI.GetButtonDown(XboxButton.Start, XboxController.First) == true)
				m_PausedPlayerNumber = 1;
			else if (XCI.GetButtonDown(XboxButton.Start, XboxController.Second) == true)
				m_PausedPlayerNumber = 2;
			else if (XCI.GetButtonDown(XboxButton.Start, XboxController.Third) == true)
				m_PausedPlayerNumber = 3;
			else if (XCI.GetButtonDown(XboxButton.Start, XboxController.Fourth) == true)
				m_PausedPlayerNumber = 4;

			m_Paused = true;
		}
		// Else if the pause button was pressed by the person who paused the game, 
		// unpause the game and unassign the player that paused the game.
		else if (XCI.GetButtonDown(XboxButton.Start, (XboxController)m_PausedPlayerNumber) == true && m_Paused == true)
		{
			// Reset time and assign the player number to something not correlating to a controller.
			Time.timeScale = 1.0f;
			m_PausedPlayerNumber = -1;
			m_Paused = false;
		}
	}
}
