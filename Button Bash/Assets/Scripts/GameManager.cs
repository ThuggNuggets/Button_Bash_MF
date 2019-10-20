using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	// The states the game can be in.
	public enum GameStates
	{
		Menu,
		Playing,
		Pause,
		GameOver
	}

	// The current game state.
	public GameStates m_CurrentGameState;

	// This instance of the game manager.
	private static GameManager m_Instance = null;

	// The player's and their selection of character.
	public static int[] m_PlayerCharacters;

	// The winning player.
	private int m_WinningPlayer = 0;

	// On startup.
	private void Awake()
	{
		// Set this game manager's instance to itself.
		m_Instance = this;

		// Get the current scene.
		Scene currentScene = SceneManager.GetActiveScene();

		// If the current scene is the main game, set the current state to playing.
		if (currentScene.name == "ButtonBash Example")
			// Set the current state to playing.
			m_CurrentGameState = GameStates.Playing;

		// Don't destroy the game manager when loading a new scene.
		DontDestroyOnLoad(gameObject);

		// Initialise the player characters array.
		m_PlayerCharacters = new int[4];
	}

	// Set the game state.
	// Params: the new game state.
	public void SetGameState(GameStates newState) { m_CurrentGameState = newState; }

	// Get the current game state.
	// Returns: the current game state.
	public GameStates GetGameState() { return m_CurrentGameState; }

	// Get the instance of the game manager.
	// Returns: this instance of the game manager.
	public static GameManager GetInstance()	{ return m_Instance; }

	// Set which player won.
	// Params: the winning player.
	public void SetWinningPlayer(int winner) 	{ m_WinningPlayer = winner; }

	// Get the player that won.
	// Returns: the winning player.
	public int GetWinningPlayer() { return m_WinningPlayer; }

	// Add a player character to the game manager.
	// Params: the player character to add to the game manager, the index for the array.
	public void AddPlayerCharacter(int playerCharacter, int index) {	 m_PlayerCharacters[index] = playerCharacter; }

	// Get a player character.
	// Params: the index for the array of player characters.
	// Returns: the player number.
	public int GetPlayerCharacter(int index) { return m_PlayerCharacters[index]; }
}