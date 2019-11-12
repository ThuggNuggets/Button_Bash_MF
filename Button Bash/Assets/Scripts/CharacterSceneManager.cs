﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class CharacterSceneManager : MonoBehaviour
{
	/// <summary>
	/// How many players are ready.
	/// </summary>
	private int m_PlayersReady = 0;

	/// <summary>
	/// If the players are ready.
	/// </summary>
	private bool m_ReadyPlayerOne = false;
	private bool m_ReadyPlayerTwo = false;
	private bool m_ReadyPlayerThree = false;
	private bool m_ReadyPlayerFour = false;

	/// <summary>
	/// On startup.
	/// </summary>
	private void Awake()
	{
		// Reset the character selection portraits.
		transform.GetChild(0).GetComponent<CharacterSelect>().SetCurrentImage(0);
		transform.GetChild(1).GetComponent<CharacterSelect>().SetCurrentImage(1);
		transform.GetChild(2).GetComponent<CharacterSelect>().SetCurrentImage(2);
		transform.GetChild(3).GetComponent<CharacterSelect>().SetCurrentImage(3);
	}

	/// <summary>
	/// Update.
	/// </summary>
	private void Update()
	{
		// Goes through each of the player's controllers and check that they have pressed a button:
		// A = ready.
		// B = unready.

		// Player 1
		if (XCI.GetButtonUp(XboxButton.A, XboxController.First) && m_ReadyPlayerOne == false)
		{
			m_PlayersReady++;
			m_ReadyPlayerOne = true;
		}
		else if (XCI.GetButtonUp(XboxButton.B, XboxController.First) && m_ReadyPlayerOne == true)
		{
			m_PlayersReady--;
			m_ReadyPlayerOne = false;
		}
		
		// Player 2.
		if (XCI.GetButtonUp(XboxButton.A, XboxController.Second) && m_ReadyPlayerTwo == false)
		{
			m_PlayersReady++;
			m_ReadyPlayerTwo = true;
		}
		else if (XCI.GetButtonUp(XboxButton.B, XboxController.Second) && m_ReadyPlayerTwo == true)
		{
			m_PlayersReady--;
			m_ReadyPlayerTwo = false;
		}

		// Player 3.
		if (XCI.GetButtonUp(XboxButton.A, XboxController.Third) && m_ReadyPlayerThree == false)
		{
			m_PlayersReady++;
			m_ReadyPlayerThree = true;
		}
		else if (XCI.GetButtonUp(XboxButton.B, XboxController.Third) && m_ReadyPlayerThree == true)
		{
			m_PlayersReady--;
			m_ReadyPlayerThree = false;
		}

		// Player 4.
		if (XCI.GetButtonUp(XboxButton.A, XboxController.Fourth) && m_ReadyPlayerFour == false)
		{
			m_PlayersReady++;
			m_ReadyPlayerFour = true;
		}
		else if (XCI.GetButtonUp(XboxButton.B, XboxController.Fourth) && m_ReadyPlayerFour == true)
		{
			m_PlayersReady--;
			m_ReadyPlayerFour = false;
		}

		// If all the players are ready, move on to the next scene.
		if (m_PlayersReady == 4)
		{
			GetComponent<MoveToNextScene>().NextScene();
		}
	}
}