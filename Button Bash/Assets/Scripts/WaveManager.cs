using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
	// The spawn point for the enemies.
	public GameObject m_EnemySpawnpoint;

	// The minimum time for enemies spawning.
	public float m_MinSpawnTime;

	// The maximum time for enemies spawning.
	public float m_MaxSpawnTime;

	// The waves.
	public GameObject[] m_Waves;

	// The wave enemy spawning timer.
	private float m_WaveEnemySpawningTimer;

	// The timer for inbetween waves.
	public float m_WaveTimer;

	// Wave timer for use in code, other one is just for changing outside of code.
	private float m_CodeWaveTimer;

	// Iterates through the waves.
	private int m_WaveIterator = 0;

	// Iterates through the enemies in the waves.
	private int m_WaveEnemyIterator = 0;

	// Basically constructor.
    void Awake()
    {
		// Seed the random number generator with the current time so the random number is different every time.
		Random.InitState((int)System.DateTime.Now.Ticks);

		// Set the code wave timer to the wave timer.
		m_CodeWaveTimer = m_WaveTimer;

		// Set the timer for spawns.
		m_WaveEnemySpawningTimer = Random.Range(m_MinSpawnTime, m_MaxSpawnTime);
    }

    // Update the wave manager.
    void Update()
    {
		// If the timer is less than or equal to 0, check the conditions to spawn an enemy.
		if (m_WaveEnemySpawningTimer <= 0.0f)
		{
			// If the wave iterator is less then the amount of waves there are, check the conditions to spawn an enemy.
			if (m_WaveIterator <= m_Waves.Length)
			{
				// If the wave enemy iterator is equal to the amount of enemies in the wave, move to the next wave.
				if (m_WaveEnemyIterator == m_Waves[m_WaveIterator].GetComponent<WaveInformation>().m_WaveEnemies.Length)
				{
					// If the code wave timer is equal to or less than 0, move on to the next wave.
					if (m_CodeWaveTimer <= 0.0f)
					{
						// If the wave iterator does not equal the amount of waves there are - 1, increment the waves iterator.
						if (m_WaveIterator != m_Waves.Length - 1)
							// Increment the wave iterator to the next wave.
							++m_WaveIterator;

						// Else, don't reset (for now), we are at the end of the waves.
						else
							return;

						// Reset the wave enemy iterator.
						m_WaveEnemyIterator = 0;

						// Reset the timer.
						m_WaveEnemySpawningTimer = Random.Range(m_MinSpawnTime, m_MaxSpawnTime);

						// Reset the code wave timer.
						m_CodeWaveTimer = m_WaveTimer;
					}

					// Else, decrease the code wave timer by delta time.
					else
						m_CodeWaveTimer -= Time.deltaTime;
				}

				// Else, spawn an enemy.
				else
				{
					// Try to spawn an enemy, if an exception is thrown, do nothing.
					try
					{
						// The spawn point for the enemy is the position of the enemy spawn.
						Vector3 spawnPos = m_EnemySpawnpoint.transform.position;

						// Set the spawn point's x positon to a random number along the z axis of the spawner.
						spawnPos.z = Random.Range(-m_EnemySpawnpoint.transform.localScale.z / 2, m_EnemySpawnpoint.transform.localScale.z / 2);

						// Increase the spawn point's y by 1, so the enemies don't spawn in the ground.
						spawnPos.y += 1.0f;

						// Get the wave information.
						WaveInformation waveInformation = m_Waves[m_WaveIterator].GetComponent<WaveInformation>();

						// Instantiate an enemy, getting what type it is from the wave's enemies, spawning at the spawn positoin, with the enemy spawn point's rotation.
						GameObject enemy = Instantiate(waveInformation.m_WaveEnemies[m_WaveEnemyIterator], spawnPos, m_EnemySpawnpoint.transform.rotation);

						// Reset the timer.
						m_WaveEnemySpawningTimer = Random.Range(m_MinSpawnTime, m_MaxSpawnTime);
					}
					// If an exception was thrown, do nothing.
					catch { }

					// Increment the wave enemy iterator.
					++m_WaveEnemyIterator;
				}
			}
		}

		// Else, decrease the timer by delta time.
		else
			m_WaveEnemySpawningTimer -= Time.deltaTime;

		//Debug.Log(m_CodeWaveTimer);
    }
}