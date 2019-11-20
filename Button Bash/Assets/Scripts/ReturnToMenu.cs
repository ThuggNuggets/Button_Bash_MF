using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using XboxCtrlrInput;

public class ReturnToMenu : MonoBehaviour
{
	/// <summary>
	/// If the script is to check for controller input.
	/// </summary>
	public bool m_UseControllerInput = false;
    public float m_delay;
	/// <summary>
	/// Update, checks input if it is using direct controller input.
	/// Otherwise it's just for the function.
	/// </summary>
	private void Update()
	{
        if (m_delay < 0)
        {
            if (m_UseControllerInput == true)
            {
                if (XCI.GetButtonDown(XboxButton.A) == true)
                    ReturnMainMenuMenu();
            }
        }
        else
        {
            m_delay -= Time.deltaTime;
        }
        Debug.Log(m_delay);
	}

	/// <summary>
	/// Return to the main menu.
	/// </summary>
	public void ReturnMainMenuMenu()
	{
		// Load the first scene (should be the main menu.)
		SceneManager.LoadScene(0);
	}
}
