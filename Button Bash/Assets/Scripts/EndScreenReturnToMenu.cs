using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using XboxCtrlrInput;
using XInputDotNetPure;

public class EndScreenReturnToMenu : MonoBehaviour
{
	/// <summary>
	/// The timer to delay input by.
	/// </summary>
	public float m_InputDelayTimer;

	private void Awake()
	{
		// Set the vibration on all the controllers to 0, for the one person still vibrating at the end of the round.
		GamePad.SetVibration(PlayerIndex.One, 0.0f, 0.0f);
		GamePad.SetVibration(PlayerIndex.Two, 0.0f, 0.0f);
		GamePad.SetVibration(PlayerIndex.Three, 0.0f, 0.0f);
		GamePad.SetVibration(PlayerIndex.Four, 0.0f, 0.0f);
	}

	/// <summary>
	/// Update.
	/// </summary>
	private void Update()
	{
		// Returns to menu when someone presses the A button after the delay timer has run out.
		// So players don't skip past the end screen because the A button is used in the game and to leave the end screen.

		if (m_InputDelayTimer <= 0.0f)
		{
			if (XCI.GetButton(XboxButton.A, XboxController.First) ||
				XCI.GetButton(XboxButton.A, XboxController.Second) ||
				XCI.GetButton(XboxButton.A, XboxController.Third) ||
				XCI.GetButton(XboxButton.A, XboxController.Fourth))
				SceneManager.LoadScene(0);
			else if (XCI.GetButton(XboxButton.X, XboxController.First) ||
				XCI.GetButton(XboxButton.X, XboxController.Second) ||
				XCI.GetButton(XboxButton.X, XboxController.Third) ||
				XCI.GetButton(XboxButton.X, XboxController.Fourth))
				SceneManager.LoadScene(5);
		}
		else
			m_InputDelayTimer -= Time.deltaTime;
	}
}
