using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinnerPortraitBehaviour : MonoBehaviour
{
	/// <summary>
	/// The portraits of the players.
	/// </summary>
	public Texture[] m_PlayerPortraits;

	void Awake()
	{
		// Get the winning player.
		int winningPlayer = GameManager.GetWinningPlayer();

		// Set the texture of the winning player's portrait to the portrait of the winning player.
		GetComponent<RawImage>().texture = m_PlayerPortraits[winningPlayer - 1];
	}
}