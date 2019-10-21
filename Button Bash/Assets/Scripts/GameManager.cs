using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager
{
	// The player's and their selection of character.
	private static int[] m_PlayerCharacters;

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
	public static void AddPlayerCharacter(int playerCharacter, int index) {	 m_PlayerCharacters[index] = playerCharacter; }

	// Get a player character.
	// Params: the index for the array of player characters.
	// Returns: the player number.
	public static int GetPlayerCharacter(int index) { return m_PlayerCharacters[index]; }
}