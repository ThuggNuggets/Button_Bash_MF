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
	/// The canvas with the quit query.
	/// </summary>
	private GameObject m_QuitScreenCanvas;

	/// <summary>
	/// On startup.
	/// </summary>
	private void Awake()
	{
		// Store the canvas.
		// The script needs to not be on the object that gets set to inactive, as then the script wouldn't run,
		// so the quit query is the child of the object that has this script, stored for easy access.
		m_QuitScreenCanvas = transform.GetChild(0).gameObject;

		// If the quit query is active, set it to inactive.
		// If someone forgets to set the screen to inactive in the editor.
		if (m_QuitScreenCanvas.activeInHierarchy == true)
			m_QuitScreenCanvas.SetActive(false);
	}

	/// <summary>
	/// Update.
	/// </summary>
	void Update()
    {
		// If a player hasn't pressed the back button, check if a player has pressed the back button.
		if (m_QueryQuit == false)
		{
			if (XCI.GetButton(XboxButton.Back, XboxController.First) ||
				XCI.GetButton(XboxButton.Back, XboxController.Second) ||
				XCI.GetButton(XboxButton.Back, XboxController.Third) ||
				XCI.GetButton(XboxButton.Back, XboxController.Fourth))
			{
				// Present the "Are you sure you want to quit?" query.
				m_QueryQuit = true;
				m_QuitScreenCanvas.SetActive(true);

				try
				{
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
				catch
				{
					m_QuittingPlayer = -1;
				}

				GameObject.Find("start button").GetComponent<MoveToNextScene>().m_UseControllerInputDirectly = false;
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
				m_QuitScreenCanvas.SetActive(false);
				GameObject.Find("start button").GetComponent<MoveToNextScene>().m_UseControllerInputDirectly = true;
			}
		}
    }
}