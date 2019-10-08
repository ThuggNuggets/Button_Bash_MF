using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CharacterSelect : MonoBehaviour
{
	// Array of the player portraits.
	public Texture[] m_PlayerPortraits;

	// The current image.
	private int m_CurrentImage;

	// The event to move on to the next character.
	UnityEvent m_NextCharacter;

	private void Awake()
	{
		// If the name of the image is "Naiciam", set the current image to 0.
		if (GetComponent<RawImage>().texture.name == "Naiciam")
			m_CurrentImage = 0;

		// If the name of the image is "neilA", set the current image to 1.
		else if (GetComponent<RawImage>().texture.name == "neilA")
			m_CurrentImage = 1;

		// If the name of the image is "nemo", set the current image to 2.
		else if (GetComponent<RawImage>().texture.name == "nemo")
			m_CurrentImage = 2;

		// If the name of the image is "sesame", set the current image to 3.
		else if (GetComponent<RawImage>().texture.name == "sesame")
			m_CurrentImage = 3;

		// Create a new unity event.
		m_NextCharacter = new UnityEvent();

		// Add the next character selection function to the next character event.
		m_NextCharacter.AddListener(NextCharacter);
	}

	// Move on to the next character.
	public void NextCharacter()
	{
		// If the current image + 1 is less than the amount of portraits there are - 1, increment the current image.
		if (m_CurrentImage < m_PlayerPortraits.Length - 1)
			++m_CurrentImage;

		// Else, set the current image to 0.
		else
			m_CurrentImage = 0;

		// Set the portrait to the current image.
		GetComponent<RawImage>().texture = m_PlayerPortraits[m_CurrentImage];
	}

	// Get the current image of the image box.
	// Returns: the current image of the image box.
	public int GetCurrentImage() { return m_CurrentImage; }
}