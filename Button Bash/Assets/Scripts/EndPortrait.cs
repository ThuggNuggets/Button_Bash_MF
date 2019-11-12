using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndPortrait : MonoBehaviour
{
	/// <summary>
	/// The portraits of the players.
	/// </summary>
	public GameObject[] m_PlayerPortraits;

	/// <summary>
	/// Position of players this portrait will display.
	/// </summary>
	public int m_PositionNumber;

	/// <summary>
	/// On startup.
	/// </summary>
	void Awake()
	{
		// Set the sprite of the player in this position to be the character in this position.
		GetComponent<SpriteRenderer>().sprite = m_PlayerPortraits[GameManager.GetDefeatedCharacter(m_PositionNumber)].GetComponent<SpriteRenderer>().sprite;
		// Set the animator controller of the player in this position to be the character in this position.
		GetComponent<Animator>().runtimeAnimatorController = m_PlayerPortraits[GameManager.GetDefeatedCharacter(m_PositionNumber)].GetComponent<Animator>().runtimeAnimatorController;
	}
}