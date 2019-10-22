﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager
{
	/// <summary>
	/// The player's and their selection of character.
	/// </summary>
	private static int[] m_PlayerCharacters = new int[4] { -1, -1, -1 ,-1};

	/// <summary>
	/// The winning player.
	/// </summary>
	private static int m_WinningPlayer = 0;

	/// <summary>
	///  Set which player won.
	///  </summary>
	/// <param name="winner">The winning player.</param>
	public static void SetWinningPlayer(int winner) 	{ m_WinningPlayer = winner; }

	/// <summary>
	/// Get the player that won.
	/// </summary>
	/// <returns>The winning player.</returns>
	public static int GetWinningPlayer() { return m_WinningPlayer; }

	/// <summary>
	/// Add a player character to the game manager.
	/// </summary>
	/// <param name="playerCharacter">The player character to add to the game manager.</param>
	/// <param name="index">The index for the array.</param>
	public static void AddPlayerCharacter(int playerCharacter, int index) { m_PlayerCharacters[index] = playerCharacter; }

	/// <summary>
	/// Get a player character from the player character array.
	/// </summary>
	/// <param name="index">The index for the array of player characters.</param>
	/// <returns>The player number</returns>
	public static int GetPlayerCharacter(int index) { return m_PlayerCharacters[index]; }

	/// <summary>
	/// Get the array of player characters.
	/// </summary>
	/// <returns>The array of player characters.</returns>
	public static int[] GetPlayerCharacters() { return m_PlayerCharacters; }

	/// <summary>
	/// Remove a player character from the array of player characters at the specified index.
	/// </summary>
	/// <param name="index">Index of the character to remove.</param> 
	public static void RemovePlayerCharacter(int index) { m_PlayerCharacters[index] = -1; }

	/// <summary>
	/// Check the specified index for a specified number.
	/// </summary>
	/// <param name="check">The item to check.</param>
	/// <param name="index">The index to check.</param>
	/// <returns>If the item is at the specified index.</returns>
	public static bool CheckPlayerCharactersIndex(int check, int index)
	{
		// Check the specified index of player character's for the specified parameter and return if the parameter is at the index.
		if (m_PlayerCharacters[index] == check)
			return true;
		else
			return false;
	}
}