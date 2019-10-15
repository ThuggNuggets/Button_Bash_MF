using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using XboxCtrlrInput;

public class CharacterSelect : MonoBehaviour
{
	// Array of the player portraits.
	public Texture[] m_PlayerPortraits;

	// The current image.
	private int m_CurrentImage;

	// The event to move on to the next character.
	UnityEvent m_NextCharacter;

	// The controller's x axis.
	private float m_XAxis;

	// The controller's y axis.
	private float m_YAxis;

	// Dead zone for the controllers.
	public float m_DeadZone = 0.05f;

	// Which player's image box this is.
	public int m_PlayerNumber;

	private bool m_ControllerHasBeenReleased = false;

	// On startup.
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

	// Update.
	private void Update()
	{
		// Check the player number of this character select instance.
		switch(m_PlayerNumber)
		{
				// Player 1.
			case 1:
				m_XAxis = XCI.GetAxis(XboxAxis.LeftStickX, XboxController.First);
				m_YAxis = XCI.GetAxis(XboxAxis.LeftStickY, XboxController.First);
				break;

				// Player 2.
			case 2:
				m_XAxis = XCI.GetAxis(XboxAxis.LeftStickX, XboxController.Second);
				m_YAxis = XCI.GetAxis(XboxAxis.LeftStickY, XboxController.Second);
				break;

				// Player 3.
			case 3:
				m_XAxis = XCI.GetAxis(XboxAxis.LeftStickX, XboxController.Third);
				m_YAxis = XCI.GetAxis(XboxAxis.LeftStickY, XboxController.Third);
				break;

				// Player 4.
			case 4:
				m_XAxis = XCI.GetAxis(XboxAxis.LeftStickX, XboxController.Fourth);
				m_YAxis = XCI.GetAxis(XboxAxis.LeftStickY, XboxController.Fourth);
				break;

				// Something is wrong.
			default:
				Debug.Log("One of the image boxes doesn't have a player number assigned.");
				break;
		}

		// If the left stick has been released, check it's direction.
		if (m_ControllerHasBeenReleased == true)
		{
			// If the left stick's x axis is greater than the dead zone, move to the next character.
			if (m_XAxis > m_DeadZone)
			{
				NextCharacter();
			}

			// Else if the left stick's x axis is less than negative the dead zone, move to the previous character.
			else if (m_XAxis < -m_DeadZone)
			{
				PrevCharacter();
			}

			// Set that the stick has been released to false.
			m_ControllerHasBeenReleased = false;
		}

		// Else if the left stick's x axis is less than the dead zone and greater than the negative dead zone, set that the stick has been released to true.
		else if (m_XAxis < m_DeadZone && m_XAxis > -m_DeadZone)
			m_ControllerHasBeenReleased = true;
	}

	// Move on to the next character.
	public void NextCharacter()
	{
		// If the current image is less than the amount of portraits there are - 1, increment the current image.
		if (m_CurrentImage < m_PlayerPortraits.Length - 1)
			++m_CurrentImage;

		// Else, set the current image to 0.
		else
			m_CurrentImage = 0;

		// Set the portrait to the current image.
		GetComponent<RawImage>().texture = m_PlayerPortraits[m_CurrentImage];
	}

	public void PrevCharacter()
	{
		// If the current image is less than the amount of portraits there are - 1, decrement the current image.
		if (m_CurrentImage > 0)
			--m_CurrentImage;

		// Else, set the current image to the amount of portraits there are - 1.
		else
			m_CurrentImage = m_PlayerPortraits.Length - 1;

		// Set the portrait to the current image.
		GetComponent<RawImage>().texture = m_PlayerPortraits[m_CurrentImage];
	}

	// Get the current image of the image box.
	// Returns: the current image of the image box.
	public int GetCurrentImage() { return m_CurrentImage; }

	// Get the player number.
	// Returns: the player number.
	public int GetPlayerNumber() { return m_PlayerNumber; }
}