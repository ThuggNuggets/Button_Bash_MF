using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndPortrait : MonoBehaviour
{
	/// <summary>
	/// The portraits of the players.
	/// </summary>
	public Texture[] m_PlayerPortraits;

	/// <summary>
	/// Position of players this portrait will display.
	/// </summary>
	public int m_PositionNumber;

	/// <summary>
	/// On startup.
	/// </summary>
	void Awake()
	{
		GetComponent<RawImage>().texture = m_PlayerPortraits[GameManager.GetDefeatedCharacter(m_PositionNumber)];
	}
}