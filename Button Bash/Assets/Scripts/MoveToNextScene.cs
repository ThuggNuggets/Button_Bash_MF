﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using XboxCtrlrInput;

public class MoveToNextScene : MonoBehaviour
{
	/// <summary>
	/// If the script should check controller input.
	/// </summary>
	public bool m_UseControllerInputDirectly = true;

	private void Update()
	{
		// If this script is to use controller input directly.
		// Since the character select screen doesn't want to check if the A button has been pressed to move on to the next screen,
		// but the start screen does.
		if (m_UseControllerInputDirectly == true)
		{
			// If the A button is pressed, move on to the next scene.
			if (XCI.GetButtonDown(XboxButton.A))
				NextScene();
		}
	}

	/// <summary>
	/// Move on to the next scene.
	/// </summary>
	public void NextScene()
	{
		// Load the next scene.
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
}