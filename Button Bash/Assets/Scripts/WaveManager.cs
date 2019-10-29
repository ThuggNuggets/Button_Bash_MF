using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
	/// <summary>
	/// The spawn point for the enemies.
	/// </summary>
	public GameObject m_EnemySpawnpoint;

	/// <summary>
	/// Array of the waves to spawn.
	/// </summary>
	public GameObject[] m_Waves;

	/// <summary>
	/// The timer for inbetween waves.
	/// </summary>
	public float m_WaveTimer;

	/// <summary>
	/// Wave timer for use in code, other one is just for changing outside of code.
	/// </summary>
	private float m_CodeWaveTimer;

	/// <summary>
	/// Iterates through the waves.
	/// </summary>
	private int m_WaveIterator = 0;

	/// <summary>
	/// Iterates through the enemies in the waves.
	/// </summary>
	private int m_WaveEnemyIterator = 0;

	/// <summary>
	/// The script that is storing all the player's lives.
	/// </summary>
	private playerLives m_PlayerLives;

	/// <summary>
	/// The collider that holds the player lives.
	/// </summary>
	public GameObject m_PlayerLivesCollider;

	/// <summary>
	/// The information of the current wave.
	/// </summary>
	private WaveInformation m_CurrentWaveInformation;

	/// <summary>
	/// The code timer for spawning enemies.
	/// </summary>
	private float m_WaveEnemySpawnCodeTimer;

	/// <summary>
	/// Iterates through the timers to spawn the enemies.
	/// </summary>
	private int m_WaveEnemySpawnTimerIterator = 0;

	// Variables for when a player is dead, replacing an enemy that shares a colour with the dead enemy.
	//------------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// Array of the block enemies.
	/// </summary>
	public GameObject[] m_BlockEnemies;

	/// <summary>
	/// Array of the teddy enemies.
	/// </summary>
	public GameObject[] m_TeddyEnemies;

	/// <summary>
	/// Array of the doll enemies.
	/// </summary>
	public GameObject[] m_DollEnemies;

	/// <summary>
	/// Array of the rubix enemies.
	/// </summary>
	public GameObject[] m_RubixEnemies;
	//------------------------------------------------------------------------------------------------------------------

	/// <summary>
	/// The enemy to spawn.
	/// </summary>
	private GameObject m_EnemyToSpawn;

	/// <summary>
	/// On startup.
	/// </summary>
	void Awake()
    {
		// Seed the random number generator with the current time so the random number is different every time.
		Random.InitState((int)System.DateTime.Now.Ticks);

		// Set the code wave timer to the wave timer.
		m_CodeWaveTimer = m_WaveTimer;

		// Get the player lives script from the player lives collider.
		m_PlayerLives = m_PlayerLivesCollider.GetComponent<playerLives>();

		// Get the information of the current wave.
		m_CurrentWaveInformation = m_Waves[m_WaveIterator].GetComponent<WaveInformation>();

		// Set the spawn timer to be the first timer in the wave's timer array
		m_WaveEnemySpawnCodeTimer = m_CurrentWaveInformation.m_WaveEnemySpawnTimes[m_WaveEnemySpawnTimerIterator];
    }

	/// <summary>
	/// Update the wave manager.
	/// </summary>
	void Update()
    {
		if (m_WaveEnemySpawnCodeTimer <= 0.0f)
		{
			if (m_WaveIterator <= m_Waves.Length)
			{
				// If the wave enemy iterator is equal to the amount of enemies in the wave, move to the next wave.
				if (m_WaveEnemyIterator == m_CurrentWaveInformation.m_WaveEnemies.Length - 1)
				{
					// If the code wave timer is equal to or less than 0, move on to the next wave.
					if (m_CodeWaveTimer <= 0.0f)
					{
						// If the wave iterator does not equal the amount of waves there are - 1, increment the waves iterator.
						// -1 because length is 1 more than the index of the final element of the array.
						if (m_WaveIterator != m_Waves.Length - 1)
							++m_WaveIterator;
						// Else, reset the wave iterator, restarting the waves.
						else
							m_WaveIterator = 0;

						// Reset the wave enemy iterator for the next wave.
						m_WaveEnemyIterator = 0;

						// Reset the code wave timer.
						m_CodeWaveTimer = m_WaveTimer;

						// Get the information of the next wave.
						m_CurrentWaveInformation = m_Waves[m_WaveIterator].GetComponent<WaveInformation>();

						// Reset the timer iterator.
						m_WaveEnemySpawnTimerIterator = 0;

						// Reset the timer.
						m_WaveEnemySpawnCodeTimer = m_CurrentWaveInformation.m_WaveEnemySpawnTimes[m_WaveEnemySpawnTimerIterator];
					}
					// Else, decrease the code wave timer by delta time.
					else
						m_CodeWaveTimer -= Time.deltaTime;
				}

				// ENEMY SPAWNING
				//------------------------------------------------------------------------------------------------------------------
				else
				{
					// Check the player that shares a colour with the upcoming enemy,
					// if that player is dead, spawn a random enemy of the same type that doesn't share their colour with a dead player.

					m_EnemyToSpawn = m_CurrentWaveInformation.m_WaveEnemies[m_WaveEnemyIterator];

					// CHECK IF PLAYER OF THAT COLOUR IS ALIVE
					//---------------------------------------------------------------------------------------------------------------
					// While the enemy shares a colour with a defeated player, spawn a different enemy of the same type.
					while (CheckPlayerLivesForEnemySpawning() == false)
					{
						// Check the tag of the enemy.
						switch (m_EnemyToSpawn.transform.GetChild(0).gameObject.tag)
						{
							// LETTER BLOCK
							//---------------------------------------------------------------------------------------------------------
							case "letterBlock":
								// Check the colour of the up coming enemy.
								switch (m_EnemyToSpawn.GetComponentInChildren<EnemyBehaviour>().m_Colour)
								{
									// If the enemy is blue.
									case Colours.Colour.Blue:
										// If player 1 is dead, replace the enemy with a different enemy.
										if (m_PlayerLives.player1Lives <= 0)
										{
											while (m_EnemyToSpawn.GetComponentInChildren<EnemyBehaviour>().m_Colour == Colours.Colour.Blue)
												m_EnemyToSpawn = m_BlockEnemies[Random.Range(0, m_BlockEnemies.Length)];

											break;
										}
										break;

									// If the enemy is red.
									case Colours.Colour.Red:
										// If player 2 is dead, replace the enemy with a different enemy.
										if (m_PlayerLives.player2Lives <= 0)
										{
											while (m_EnemyToSpawn.GetComponentInChildren<EnemyBehaviour>().m_Colour == Colours.Colour.Red)
												m_EnemyToSpawn = m_BlockEnemies[Random.Range(0, m_BlockEnemies.Length)];
										}
										break;

									// If the enemy is green.
									case Colours.Colour.Green:
										// If player 3 is dead, replace the enemy with a different enemy.
										if (m_PlayerLives.player3Lives <= 0)
										{
											while (m_EnemyToSpawn.GetComponentInChildren<EnemyBehaviour>().m_Colour == Colours.Colour.Green)
												m_EnemyToSpawn = m_BlockEnemies[Random.Range(0, m_BlockEnemies.Length)];
										}
										break;

									// If the enemy is yellow.
									case Colours.Colour.Yellow:
										// If player 4 is dead, replace the enemy with a different enemy.
										if (m_PlayerLives.player4Lives <= 0)
										{
											while (m_EnemyToSpawn.GetComponentInChildren<EnemyBehaviour>().m_Colour == Colours.Colour.Yellow)
												m_EnemyToSpawn = m_BlockEnemies[Random.Range(0, m_BlockEnemies.Length)];
										}
										break;
								}
								break;
							//---------------------------------------------------------------------------------------------------------

							// TEDDY BEAR
							//---------------------------------------------------------------------------------------------------------
							case "teddyBear":
								// Check the colour of the up coming enemy.
								switch (m_EnemyToSpawn.GetComponentInChildren<EnemyBehaviour>().m_Colour)
								{
									// If the enemy is blue.
									case Colours.Colour.Blue:
										// If player 1 is dead, replace the enemy with a different enemy.
										if (m_PlayerLives.player1Lives <= 0)
										{
											while (m_EnemyToSpawn.GetComponentInChildren<EnemyBehaviour>().m_Colour == Colours.Colour.Blue)
												m_EnemyToSpawn = m_TeddyEnemies[Random.Range(0, m_TeddyEnemies.Length)];
										}
										break;

									// If the enemy is red.
									case Colours.Colour.Red:
										// If player 2 is dead, replace the enemy with a different enemy.
										if (m_PlayerLives.player2Lives <= 0)
										{
											while (m_EnemyToSpawn.GetComponentInChildren<EnemyBehaviour>().m_Colour == Colours.Colour.Red)
												m_EnemyToSpawn = m_TeddyEnemies[Random.Range(0, m_TeddyEnemies.Length)];
										}
										break;

									// If the enemy is green.
									case Colours.Colour.Green:
										// If player 3 is dead, replace the enemy with a different enemy.
										if (m_PlayerLives.player3Lives <= 0)
										{
											while (m_EnemyToSpawn.GetComponentInChildren<EnemyBehaviour>().m_Colour == Colours.Colour.Green)
												m_EnemyToSpawn = m_TeddyEnemies[Random.Range(0, m_TeddyEnemies.Length)];
										}
										break;

									// If the enemy is yellow.
									case Colours.Colour.Yellow:
										// If player 4 is dead, replace the enemy with a different enemy.
										if (m_PlayerLives.player4Lives <= 0)
										{
											while (m_EnemyToSpawn.GetComponentInChildren<EnemyBehaviour>().m_Colour == Colours.Colour.Yellow)
												m_EnemyToSpawn = m_TeddyEnemies[Random.Range(0, m_TeddyEnemies.Length)];
										}
										break;
								}
								break;
							//---------------------------------------------------------------------------------------------------------

							// BABUSHKA DOLL
							//---------------------------------------------------------------------------------------------------------
							case "babushkaLarge":
								// Check the colour of the up coming enemy.
								switch (m_EnemyToSpawn.GetComponentInChildren<EnemyBehaviour>().m_Colour)
								{
									// If the enemy is blue.
									case Colours.Colour.Blue:
										// If player 1 is dead, replace the enemy with a different enemy.
										if (m_PlayerLives.player1Lives <= 0)
										{
											while (m_EnemyToSpawn.GetComponentInChildren<EnemyBehaviour>().m_Colour == Colours.Colour.Blue)
												m_EnemyToSpawn = m_DollEnemies[Random.Range(0, m_DollEnemies.Length)];
										}
										break;

									// If the enemy is red.
									case Colours.Colour.Red:
										// If player 2 is dead, replace the enemy with a different enemy.
										if (m_PlayerLives.player2Lives <= 0)
										{
											while (m_EnemyToSpawn.GetComponentInChildren<EnemyBehaviour>().m_Colour == Colours.Colour.Red)
												m_EnemyToSpawn = m_DollEnemies[Random.Range(0, m_DollEnemies.Length)];
										}
										break;

									// If the enemy is green.
									case Colours.Colour.Green:
										// If player 3 is dead, replace the enemy with a different enemy.
										if (m_PlayerLives.player3Lives <= 0)
										{
											while (m_EnemyToSpawn.GetComponentInChildren<EnemyBehaviour>().m_Colour == Colours.Colour.Green)
												m_EnemyToSpawn = m_DollEnemies[Random.Range(0, m_DollEnemies.Length)];
										}
										break;

									// If the enemy is yellow.
									case Colours.Colour.Yellow:
										// If player 4 is dead, replace the enemy with a different enemy.
										if (m_PlayerLives.player4Lives <= 0)
										{
											while (m_EnemyToSpawn.GetComponentInChildren<EnemyBehaviour>().m_Colour == Colours.Colour.Yellow)
												m_EnemyToSpawn = m_DollEnemies[Random.Range(0, m_DollEnemies.Length)];
										}
										break;
								}
								//------------------------------------------------------------------------------------------------------
								break;

							// RUBIX CUBE
							//---------------------------------------------------------------------------------------------------------
							case "rubix":
								// Check the colour of the up coming enemy.
								switch (m_EnemyToSpawn.GetComponentInChildren<EnemyBehaviour>().m_Colour)
								{
									// If the enemy is blue.
									case Colours.Colour.Blue:
										// If player 1 is dead, replace the enemy with a different enemy.
										if (m_PlayerLives.player1Lives <= 0)
										{
											while (m_EnemyToSpawn.GetComponentInChildren<EnemyBehaviour>().m_Colour == Colours.Colour.Blue)
												m_EnemyToSpawn = m_RubixEnemies[Random.Range(0, m_RubixEnemies.Length)];
										}
										break;

									// If the enemy is red.
									case Colours.Colour.Red:
										// If player 2 is dead, replace the enemy with a different enemy.
										if (m_PlayerLives.player2Lives <= 0)
										{
											while (m_EnemyToSpawn.GetComponentInChildren<EnemyBehaviour>().m_Colour == Colours.Colour.Red)
												m_EnemyToSpawn = m_RubixEnemies[Random.Range(0, m_RubixEnemies.Length)];
										}
										break;

									// If the enemy is green.
									case Colours.Colour.Green:
										// If player 3 is dead, replace the enemy with a different enemy.
										if (m_PlayerLives.player3Lives <= 0)
										{
											while (m_EnemyToSpawn.GetComponentInChildren<EnemyBehaviour>().m_Colour == Colours.Colour.Green)
												m_EnemyToSpawn = m_RubixEnemies[Random.Range(0, m_RubixEnemies.Length)];
										}
										break;

									// If the enemy is yellow.
									case Colours.Colour.Yellow:
										// If player 4 is dead, replace the enemy with a different enemy.
										if (m_PlayerLives.player4Lives <= 0)
										{
											while (m_EnemyToSpawn.GetComponentInChildren<EnemyBehaviour>().m_Colour == Colours.Colour.Yellow)
												m_EnemyToSpawn = m_RubixEnemies[Random.Range(0, m_RubixEnemies.Length)];
										}
										break;
								}
								break;
							//---------------------------------------------------------------------------------------------------------
						}
					}
					//---------------------------------------------------------------------------------------------------------------

					// The player that shares a colour with the next enemy is alive, therefore, spawn the enemy.
					// Try to spawn an enemy, if an exception is thrown, output a message.
					try
					{
						// The spawn point for the enemy is the position of the enemy spawn.
						Vector3 spawnPos = m_EnemySpawnpoint.transform.position;

						// Set the spawn point's z positon to a random number along the z axis of the spawner.
						spawnPos.z = Random.Range(-m_EnemySpawnpoint.transform.localScale.z / 2, m_EnemySpawnpoint.transform.localScale.z / 2);

						// Instantiate an enemy, getting it from the wave's enemies, spawning at the spawn positoin, with the enemy spawn point's rotation.
						GameObject enemy = Instantiate(m_EnemyToSpawn, spawnPos, m_EnemySpawnpoint.transform.rotation);

						// So we don't go outside the bounds of the array.
						if (m_WaveEnemySpawnTimerIterator < m_CurrentWaveInformation.m_WaveEnemySpawnTimes.Length - 1)
						{
							// Increment the spawn timer iterator.
							m_WaveEnemySpawnTimerIterator++;

							// Set the timer to the next timer in the wave's timer array.
							m_WaveEnemySpawnCodeTimer = m_CurrentWaveInformation.m_WaveEnemySpawnTimes[m_WaveEnemySpawnTimerIterator];
						}
					}
					// An exception was thrown, output a message about which enemy in which wave caused the exception.
					catch
					{
						Debug.Log("Enemy " + m_WaveEnemyIterator + " of wave " + m_WaveIterator + " threw an exception, something is wrong with it.");
					}

					// Increment the wave enemy iterator.
					++m_WaveEnemyIterator;
				}
				//------------------------------------------------------------------------------------------------------------------
			}
		}
		// Else, decrease the timer by delta time.
		else
			m_WaveEnemySpawnCodeTimer -= Time.deltaTime;
    }

	/// <summary>
	/// Check the player with the current enemy to spawn's colour to see if that player is alive.
	/// </summary>
	/// <returns>True if the player is alive, else false.</returns>
	private bool CheckPlayerLivesForEnemySpawning()
	{
		switch(m_EnemyToSpawn.GetComponentInChildren<EnemyBehaviour>().m_Colour)
		{
			// Enemy is blue.
			case Colours.Colour.Blue:
				// If the blue player is alive, return that the player is alive.
				// Else return false.
				if (m_PlayerLives.GetComponent<playerLives>().player1Lives > 0)
					return true;
				else
					return false;

			// Enemy is red.
			case Colours.Colour.Red:
				// If the red player is alive, return that the player is alive.
				// Else return false.
				if (m_PlayerLives.GetComponent<playerLives>().player2Lives > 0)
					return true;
				else
					return false;

			// Enemy is green.
			case Colours.Colour.Green:
				// If the green player is alive, return that the player is alive.
				// Else return false.
				if (m_PlayerLives.GetComponent<playerLives>().player3Lives > 0)
					return true;
				else
					return false;

			// Enemy is yellow.
			case Colours.Colour.Yellow:
				// If the yellow player is alive, return that the player is alive.
				// Else return false.
				if (m_PlayerLives.GetComponent<playerLives>().player4Lives > 0)
					return true;
				else
					return false;

				// Default to returning false.
			default:
				Debug.Log("Something went wrong with the wave manager checking alive players.");
				return false;
		}
	}
}