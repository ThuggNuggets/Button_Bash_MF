using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class CharacterSceneManager : MonoBehaviour
{
	// How many players are ready.
	//private int m_PlayersReady = 0;

	//private bool m_ReadyPlayerOne = false;
	//private bool m_ReadyPlayerTwo = false;
	//private bool m_ReadyPlayerThree = false;
	//private bool m_ReadyPlayerFour = false;

	private void Update()
	{
		//if (XCI.GetButtonUp(XboxButton.A, XboxController.First) && m_ReadyPlayerOne == false)
		//{
		//	m_PlayersReady++;
		//	m_ReadyPlayerOne = true;
		//}
		//
		//if (XCI.GetButtonUp(XboxButton.A, XboxController.Second) && m_ReadyPlayerTwo == false)
		//{
		//	m_PlayersReady++;
		//	m_ReadyPlayerTwo = true;
		//}
		//
		//if (XCI.GetButtonUp(XboxButton.A, XboxController.Third) && m_ReadyPlayerThree == false)
		//{
		//	m_PlayersReady++;
		//	m_ReadyPlayerThree = true;
		//}
		//
		//if (XCI.GetButtonUp(XboxButton.A, XboxController.Fourth) && m_ReadyPlayerFour == false)
		//{
		//	m_PlayersReady++;
		//	m_ReadyPlayerFour = true;
		//}
		//
		//if (m_PlayersReady == Input.GetJoystickNames().Length - 1)
		//{
		//	GetComponentInChildren<StartGame>().BeginGame();
		//}
		//
		//Debug.Log(Input.GetJoystickNames().Length);

		if (XCI.GetButtonUp(XboxButton.A))
		{
			GetComponentInChildren<StartGame>().BeginGame();
		}
	}
}