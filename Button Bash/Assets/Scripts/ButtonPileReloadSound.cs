using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPileReloadSound : MonoBehaviour
{
	/// <summary>
	/// The audio source on the button pile.
	/// </summary>
	private AudioSource m_AudioSource;

	private void Awake()
	{
		// Set the audio source's clip to the reload sound.
		m_AudioSource = GetComponent<AudioSource>();
	}

	/// <summary>
	/// When something collides with the button pile.
	/// </summary>
	/// <param name="collision"></param>
	private void OnCollisionEnter(Collision collision)
	{
		// Play the sound in the audio source, should be the reload sound.
		m_AudioSource.Play();
	}
}