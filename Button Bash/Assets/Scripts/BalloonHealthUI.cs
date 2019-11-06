using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonHealthUI : MonoBehaviour
{
	/// <summary>
	/// Array of the balloons. 
	/// </summary>
	public GameObject[] m_Balloons;

	/// <summary>
	/// Iterates through the balloons in the array. 
	/// </summary>
	private int m_BalloonIterator;

	/// <summary>
	/// Display that damage has been taken by having a balloon float up the screen.
	/// </summary>
	public void TakeDamage()
	{
		// Make sure we don't try to access something outside the bounds of the array.
		if (m_BalloonIterator < m_Balloons.Length)
		{
			// Activate the current balloon in the array.
			m_Balloons[m_BalloonIterator].GetComponent<HealthBalloonBehaviour>().SetActive(true);

			// Increment the balloon iterator, so SetActive only gets called once on each balloon.
			m_BalloonIterator++;
		}
	}
}