using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

	// On startup, just for debugging, to quickly get to the main game.
	private void Awake()
	{
		// Get the current scene.
		Scene currentScene = SceneManager.GetActiveScene();

		// If the current scene is the main game, set the current state to playing.
		if (currentScene.name == "ButtonBash Example")
			// Set the current state to playing.
			m_CurrentGameState = GameStates.Playing;
	}

	// Set the game state.
	// Params: the new game state.
	public void SetGameState(GameStates newState)
	{
		// If the new state is the same as the current game state
		if (newState == m_CurrentGameState)
			return;

		// Check what the new state is.
		switch (newState)
		{
			// Game is on the menu.
			case GameStates.Menu:
				// Show the menu.
				break;

			// Game is on the game over screen
			case GameStates.GameOver:
				// Show the end screen.
				break;

			// Game is currently being played.
			case GameStates.Playing:
				// Start the game.
				break;

			// Game is paused.
			case GameStates.Pause:
				// Pause the game.
				break;
		}

		// Set the current game state to be the new game state.
		m_CurrentGameState = newState;
	}

	// Get the current game state.
	// Returns: the current game state.
	public GameStates GetGameState() { return m_CurrentGameState; }

	// Get the instance of the game manager.
	// Returns: this instance of the game manager.
	public static GameManager GetInstance()
	{
		// If there isn't an instance of a game manager, find a game manager in the scene.
		if (m_Instance == null)
		{
			// Find a game manager.
			m_Instance = (GameManager)FindObjectOfType(typeof(GameManager));

			// If there wasn't a game manager in the scene, create a new game manager in the scene.
			if (m_Instance == null)
			{
				// Create a new game object.
				GameObject gameManager = new GameObject();

				// Add the game manager script to the object.
				m_Instance = gameManager.AddComponent<GameManager>();

				// Call the object "Game Manager".
				gameManager.name = "Game Manager";

				// Don't destroy the game manager when loading a new scene.
				DontDestroyOnLoad(gameManager);
			}
		}

		// Return this instance of the game manager.
		return m_Instance;
	}
}
