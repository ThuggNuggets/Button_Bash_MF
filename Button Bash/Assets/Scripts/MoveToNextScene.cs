using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using XboxCtrlrInput;

public class MoveToNextScene : MonoBehaviour
{
	private void Update()
	{
		// If the A button is pressed, move on to the next scene.
		if (XCI.GetButtonDown(XboxButton.A))
		{
			NextScene();
		}
	}

	public void NextScene()
	{
		// Load the next scene.
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
}