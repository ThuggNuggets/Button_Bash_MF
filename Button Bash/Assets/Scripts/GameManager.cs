using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager
{
	// The player's and their selection of character.
	private static int[] m_PlayerCharacters = new int[4] { -1, -1, -1 ,-1};

	// The winning player.
	private static int m_WinningPlayer = 0;

	// Set which player won.
	// Params: the winning player.
	public static void SetWinningPlayer(int winner) 	{ m_WinningPlayer = winner; }

	// Get the player that won.
	// Returns: the winning player.
	public static int GetWinningPlayer() { return m_WinningPlayer; }

	// Add a player character to the game manager.
	// Params: the player character to add to the game manager, the index for the array.
	public static void AddPlayerCharacter(int playerCharacter, int index) { m_PlayerCharacters[index] = playerCharacter; }

	// Get a player character.
	// Params: the index for the array of player characters.
	// Returns: the player number.
	public static int GetPlayerCharacter(int index) { return m_PlayerCharacters[index]; }

	// Get the array of player characters.
	// Returns: the array of player characters.
	public static int[] GetPlayerCharacters() { return m_PlayerCharacters; }

	// Remove a player character from the array of locked characters.
	// Params: index of the character to remove.
	public static void RemovePlayerCharacter(int index) { m_PlayerCharacters[index] = -1; }

	// Check the specified index.
	// Params: the item to check, the index to check.
	// Returns: if the item is at the specified index.
	public static bool CheckIndex(int check, int index)
	{
		if (m_PlayerCharacters[index] == check)
			return true;
		else
			return false;
	}
}