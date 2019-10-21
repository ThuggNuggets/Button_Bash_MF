using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonHealthUI : MonoBehaviour
{
	// Array of the balloons.
	public GameObject[] m_Balloons;

	// Iterates through the balloons in the array.
	private int m_BalloonIterator;

	// Display that damage has been taken by having a balloon float up the screen.
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