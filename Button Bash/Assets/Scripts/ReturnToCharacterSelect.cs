using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using XboxCtrlrInput;

public class ReturnToCharacterSelect : MonoBehaviour
{
	/// <summary>
	/// If this script should check player input.
	/// </summary>
	public bool m_UseDirectPlayerInput = true;

    /// <summary>
	/// Update.
	/// </summary>
    void Update()
    {
		if (m_UseDirectPlayerInput == true)
		{
			if (XCI.GetButtonDown(XboxButton.A) == true)
				GoToCharacterSelect();
		}
    }

	/// <summary>
	/// Load the character select scene.
	/// </summary>
	public void GoToCharacterSelect()
	{
		SceneManager.LoadScene(1);
	}
}
