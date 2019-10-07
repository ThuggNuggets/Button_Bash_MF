﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
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
	private GameStates m_CurrentGameState;

	// This instance of the game manager.
	private static GameManager m_Instance = null;

	// On startup.
	private void Awake()
	{
		// If the game manager instance is null, set the game manager instance to this.
		if (m_Instance == null)
			m_Instance = this;
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
			case GameStates.Menu:
				// Show the menu.
				break;

			case GameStates.GameOver:
				// Show the end screen.
				break;

			case GameStates.Playing:
				// Start the game.
				break;

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
	public static GameManager GetInstance() { return m_Instance; }
}