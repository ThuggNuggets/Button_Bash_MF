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

	// The timer.
	private float m_Timer;

	// Iterates through the waves.
	private int m_WaveIterator = 0;

	// Iterates through the enemies in the waves.
	private int m_WaveEnemyIterator = 0;

	// Basically constructor.
    void Awake()
    {
		// Seed the random number generator with the current time.
		Random.InitState((int)System.DateTime.Now.Ticks);

		// Set the timer for spawns.
		m_Timer = Random.Range(m_MinSpawnTime, m_MaxSpawnTime);
    }

    // Update the wave manager.
    void Update()
    {
		// If the timer is less than or equal to 0, check the conditions to spawn an enemy.
		if (m_Timer <= 0.0f)
		{
			// If the wave iterator is less then the amount of waves there are, check the conditions to spawn an enemy.
			if (m_WaveIterator <= m_Waves.Length)
			{
				// If the wave enemy iterator is equal to the amount of enemies in the wave, move to the next wave.
				if (m_WaveEnemyIterator == m_Waves[m_WaveIterator].GetComponent<WaveInformation>().m_WaveEnemies.Length)
				{
					// Increment the wave iterator to the next wave.
					++m_WaveIterator;

					// Reset the wave enemy iterator.
					m_WaveEnemyIterator = 0;

					// Reset the timer.
					m_Timer = Random.Range(m_MinSpawnTime, m_MaxSpawnTime);
				}

				// Else, spawn an enemy.
				else
				{
					// The spawn point for the enemy is the position of the enemy spawn.
					Vector3 spawnPos = m_EnemySpawnpoint.transform.position;

					// Set the spawn point's x positon to a random number along the z axis of the spawner.
					spawnPos.z = Random.Range(-m_EnemySpawnpoint.transform.localScale.z / 2, m_EnemySpawnpoint.transform.position.z / 2);
					
					// Increase the spawn point's y by 1, so the enemies don't spawn in the ground.
					spawnPos.y += 1.0f;

					// Get the wave information.
					WaveInformation waveInformation = m_Waves[m_WaveIterator].GetComponent<WaveInformation>();

					// Instantiate an enemy, getting what type it is from the wave's enemies, spawning at the spawn positoin, with the enemy spawn point's rotation.
					GameObject enemy = Instantiate(waveInformation.m_WaveEnemies[m_WaveEnemyIterator], spawnPos, m_EnemySpawnpoint.transform.rotation);

					// Set the colour of the enemy to be the colour set in the wave.
					//enemy.GetComponent<EnemyBehaviour>().SetColour(waveInformation.m_EnemyColours[m_WaveEnemyIterator]);

					// Reset the timer.
					m_Timer = Random.Range(m_MinSpawnTime, m_MaxSpawnTime);

					// Increment the wave enemy iterator.
					++m_WaveEnemyIterator;
				}
			}
		}

		// Else, decrease the timer by delta time.
		else
			m_Timer -= Time.deltaTime;

		Debug.Log(m_Timer);
    }
}