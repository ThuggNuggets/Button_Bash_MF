using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
	// The event to begin the game.
	UnityEvent m_BeginEvent;

	// On startup.
	private void Awake()
	{
		// Create a new unity event.
		m_BeginEvent = new UnityEvent();

		// Add the begin game function to the begin event.
		m_BeginEvent.AddListener(BeginGame);
	}

	// Begin the game.
	public void BeginGame()
	{
		// Get the instance of the game manager.
		GameManager gm = GameManager.GetInstance();

		// Set the state of the game manager to playing.
		gm.SetGameState(GameManager.GameStates.Playing);

        // Load the main game scene.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);


        GameObject.Find("Canvas").SetActive(false);
	}
}