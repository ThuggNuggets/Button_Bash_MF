using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using XboxCtrlrInput;

public class EndScreenReturnToMenu : MonoBehaviour
{
	/// <summary>
	/// The timer to delay input by.
	/// </summary>
	public float m_InputDelayTimer;

	/// <summary>
	/// Update.
	/// </summary>
	private void Update()
	{
		// Returns to menu when someone presses the A button after the delay timer has run out.
		// So players don't skip past the end screen because the A button is used in the game and to leave the end screen.

		if (m_InputDelayTimer <= 0.0f)
		{
			if (XCI.GetButton(XboxButton.A) == true)
				SceneManager.LoadScene(0);
			else if (XCI.GetButton(XboxButton.X) == true)
				SceneManager.LoadScene(5);
		}
		else
			m_InputDelayTimer -= Time.deltaTime;
	}
}
