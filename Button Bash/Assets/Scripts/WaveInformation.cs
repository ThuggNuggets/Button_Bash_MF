using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveInformation : MonoBehaviour
{
	/// <summary>
	/// The enemies in the wave.
	/// </summary>
	public GameObject[] m_WaveEnemies;

	/// <summary>
	/// The timers to spawn the enemies.
	/// </summary>
	public float[] m_WaveEnemySpawnTimes;
}