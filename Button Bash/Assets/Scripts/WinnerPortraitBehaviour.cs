using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinnerPortraitBehaviour : MonoBehaviour
{
	void Awake()
	{
		// Get the instance of the game manager.
		GameManager gm = GameManager.GetInstance();

		// Get the winning player.
		int winningPlayer = gm.GetComponent<GameManager>().GetWinningPlayer();

		// Set the texture of the winning player's portrait to the portrait of the winning player.
		GetComponent<RawImage>().texture = GetComponent<CharacterSelect>().m_PlayerPortraits[winningPlayer - 1];
	}
}