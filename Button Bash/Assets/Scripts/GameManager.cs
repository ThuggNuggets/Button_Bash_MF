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
	public RawImage[] m_PlayersPortraits;

	// On startup.
	private void Awake()
	{
		// Set this game manager's instance to itself.
		m_Instance = this;

		// Get the current scene.
		Scene currentScene = SceneManager.GetActiveScene();

		// When a scene is loaded, call the function "OnSceneLoaded".
		SceneManager.sceneLoaded += OnSceneLoaded;

		// If the current scene is the main game, set the current state to playing.
		if (currentScene.name == "ButtonBash Example")
			// Set the current state to playing.
			m_CurrentGameState = GameStates.Playing;

		// Don't destroy the game manager when loading a new scene.
		DontDestroyOnLoad(gameObject);
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

	// When a scene is loaded.
	// Params: the scene, the load scene mode.
	void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		Debug.Log(scene.name + " loaded");

		// If the current scene is "ButtonBash Example", find the players and apply their player number.
		if (scene.name == "ButtonBash Example")
		{
			// Get the player characters.
			GameObject[] playerCharacters = GameObject.FindGameObjectsWithTag("Player");

			// For each player character there is, assign it's player number.
			for (int i = 0; i < playerCharacters.Length; ++i)
			{
				try
				{
					// Assign each player character's player number to their player's number. (If that didn't make sense, read it again.)
					playerCharacters[i].GetComponent<TEMPORARYPLAYERCONTROLS>().playerNumber = m_PlayersPortraits[i].GetComponent<CharacterSelect>().GetCurrentImage();
				}
				catch { }
			}
		}
	}
}