using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
	/// <summary>
	/// The timer for the fake loading screen to stay up for.
	/// </summary>
	public float m_LoadTime;

	/// <summary>
	/// Update.
	/// </summary>
	private void Update()
	{
        // Decrement the time, when it reaches 0, load the game.
        if (m_LoadTime <= 0.0f)
        {
            SceneManager.LoadScene(3);
        }
        else
        {
            m_LoadTime -= Time.deltaTime;
        }
	}
}
