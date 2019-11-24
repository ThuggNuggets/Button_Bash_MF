using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class QuitGame : MonoBehaviour
{
	/// <summary>
	/// If the quit query is on screen.
	/// </summary>
	private bool m_QueryQuit = false;

	/// <summary>
	/// Number of which player started quitting.
	/// </summary>
	private int m_QuittingPlayer;

    /// <summary>
	/// Update.
	/// </summary>
    void Update()
    {
		// If a player hasn't pressed the back button, check if a player has pressed the back button.
		if (m_QueryQuit == false)
		{
			if (XCI.GetButton(XboxButton.Back))
			{
				// Present the "Are you sure you want to quit?" query.
				m_QueryQuit = true;
				transform.GetChild(0).gameObject.SetActive(true);

				// Remember which player pressed the back button.
				if (XCI.GetButton(XboxButton.Back, XboxController.First))
					m_QuittingPlayer = 1;
				else if (XCI.GetButton(XboxButton.Back, XboxController.Second))
					m_QuittingPlayer = 2;
				else if (XCI.GetButton(XboxButton.Back, XboxController.Third))
					m_QuittingPlayer = 3;
				else if (XCI.GetButton(XboxButton.Back, XboxController.Fourth))
					m_QuittingPlayer = 4;
			}
		}
		// If the quit query is on screen.
		else
		{
			if (XCI.GetButton(XboxButton.A, (XboxController)m_QuittingPlayer))
				Application.Quit();
			else if (XCI.GetButton(XboxButton.B, (XboxController)m_QuittingPlayer))
			{
				// Back out of the quit query.
				m_QueryQuit = false;
				transform.GetChild(0).gameObject.SetActive(false);
			}
		}
    }
}
