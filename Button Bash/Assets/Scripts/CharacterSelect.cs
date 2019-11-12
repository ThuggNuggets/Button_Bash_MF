using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using XboxCtrlrInput;

public class CharacterSelect : MonoBehaviour
{
	/// <summary>
	/// Array of the player portraits.
	/// </summary>
	public GameObject[] m_PlayerPortraits;

	/// <summary>
	/// The current image.
	/// </summary>
	private int m_CurrentImage = 0;

	/// <summary>
	/// The controller's x axis.
	/// </summary>
	private float m_XAxis;

	/// <summary>
	/// Dead zone for the controllers.
	/// </summary>
	public float m_DeadZone = 0.05f;

	/// <summary>
	/// Which player's image box this is.
	/// </summary>
	public int m_PlayerNumber;

	/// <summary>
	/// If the controller stick has been released.
	/// </summary>
	private bool m_LeftStickHasBeenReleased = false;

	/// <summary>
	/// If the A button was pressed on the controller.
	/// </summary>
	private bool m_AButtonPressed = false;

	/// <summary>
	/// If the B button was pressed on the controller.
	/// </summary>
	private bool m_BButtonPressed = false;

	/// <summary>
	/// If the character has been locked in.
	/// </summary>
	private bool m_CharacterLockedIn = false;

	/// <summary>
	/// What colour to set the image box to when a character is selected.
	/// </summary>
	public Color m_LockedInDarkenColour;

	public Button m_SelectButton;

	/// <summary>
	/// On startup.
	/// </summary>
	private void Awake()
	{
		// Check the name of the current image showing the currently selected character
		// and assign the index of the current image to match the current image in the array.
		// Example: Naiciam is at index 1 of the array of images, so set the index to 1 at startup.

		if (GetComponent<RawImage>().name == "nemo")
			m_CurrentImage = 0;
		else if (GetComponent<RawImage>().name == "Naiciam")
			m_CurrentImage = 1;
		else if (GetComponent<RawImage>().name == "neilA")
			m_CurrentImage = 2;
		else if (GetComponent<RawImage>().name == "sesame")
			m_CurrentImage = 3;

		// Reset the defeated player characters, so when we go to the next scene, the main game, the defeated players will have been reset.
		GameManager.ResetDefeatedCharacters();

		// Unlock the character on startup, in case we are returning to this scene.
		UnlockCharacter();
	}

	/// <summary>
	/// Update.
	/// </summary>
	private void Update()
	{
		// If a character hasn't been locked in, cycle through the characters.
		if (m_CharacterLockedIn == false)
		{
			m_XAxis = XCI.GetAxis(XboxAxis.LeftStickX, (XboxController)m_PlayerNumber + 1);

			// If the left stick has been released, check it's direction.
			// So the character doesn't change every frame.
			if (m_LeftStickHasBeenReleased == true)
			{
				// If the left stick's x axis is greater than the dead zone, move to the next character.
				if (m_XAxis > m_DeadZone)
				{
					NextCharacter();
					Debug.Log("Next character.");

					// Set that the stick has been released to false.
					m_LeftStickHasBeenReleased = false;
				}
				// Else if the left stick's x axis is less than negative the dead zone, move to the previous character.
				else if (m_XAxis < -m_DeadZone)
				{
					PrevCharacter();
					Debug.Log("Previous character.");

					// Set that the stick has been released to false.
					m_LeftStickHasBeenReleased = false;
				}
			}
			// Else if the left stick's x axis is 0, therefore isn't being pushed, 
			// set that the stick has been released to true.
			else if (m_XAxis == 0)
				m_LeftStickHasBeenReleased = true;
		}

		m_AButtonPressed = XCI.GetButtonDown(XboxButton.A, (XboxController)m_PlayerNumber + 1);
		m_BButtonPressed = XCI.GetButtonDown(XboxButton.B, (XboxController)m_PlayerNumber + 1);

		if (m_AButtonPressed == true)
		{
			// If a character hasn't been locked in, lock in the character.
			if (m_CharacterLockedIn == false)
				LockInCharacter();
		}

		if (m_BButtonPressed == true)
		{
			// If a character has been locked in, unlock the character.
			if (m_CharacterLockedIn == true)
				UnlockCharacter();
		}
	}

	/// <summary>
	/// Lock in the current character.
	/// </summary>
	public void LockInCharacter()
	{
		// Add the current character to the game manager.
		// So it can be accessed in the main game scene.
		GameManager.AddPlayerCharacter(m_PlayerNumber, m_CurrentImage);

		m_CharacterLockedIn = true;

		// Try to play a sound, right now doesn't have audio sources on the portraits.
		try
		{
			// Play a sound when a character is selected, with the sound correlating with the character that was selected.
			AudioSource ac = GetComponent<AudioSource>();
			SoundManager sm = GameObject.Find("Sound bucket ").GetComponent<SoundManager>();
			switch (m_CurrentImage)
			{
				case 0:
					ac.clip = sm.m_SoundClips[5];
					break;
				case 1:
					ac.clip = sm.m_SoundClips[12];
					break;
				case 2:
					ac.clip = sm.m_SoundClips[13];
					break;
				case 3:
					ac.clip = sm.m_SoundClips[14];
					break;
			}
			ac.Play();
		}
		catch { }

		// Represent the select button being used.
		m_SelectButton.interactable = false;

		// Find all the image boxes in the scene, so we can check their current character.
		GameObject[] imgBoxes = GameObject.FindGameObjectsWithTag("Image Box");

		// Go through each of the image boxes.
		for (int i = 0; i < imgBoxes.Length; ++i)
		{
			// So we don't compare with ourself.
			if (imgBoxes[i] != gameObject)
			{
				// If the current character of the other image box is the same as this one, move the other image box to the next character.
				if (imgBoxes[i].GetComponent<CharacterSelect>().GetCurrentImage() == m_CurrentImage)
				{
					imgBoxes[i].GetComponent<CharacterSelect>().NextCharacter();
				}
			}
		}
		// Darken the image of the character to indicate it is selected.
		GetComponent<SpriteRenderer>().color = m_LockedInDarkenColour;
	}

	/// <summary>
	/// Unlock the current character.
	/// </summary>
	public void UnlockCharacter()
	{
		// Remove the character from the game manager.
		GameManager.RemovePlayerCharacter(m_CurrentImage);

		m_CharacterLockedIn = false;

		m_SelectButton.interactable = true;

		GetComponent<SpriteRenderer>().color = Color.white;
	}

	/// <summary>
	/// Move on to the next character.
	/// </summary>
	public void NextCharacter()
	{
		// If the current image is less than the amount of portraits there are - 1, select the next image in the array.
		if (m_CurrentImage < m_PlayerPortraits.Length - 1)
			++m_CurrentImage;
		// Else, set the current image to 0.
		else
			m_CurrentImage = 0;

		// This goes through the array of characters, finding the next character that someone hasn't selected.
		while (true)
		{
			// If there isn't -1 at the index of the current image, select the next character,
			// as that character has been selected by someone else.
			// Else, break out of the loop, since that character hasn't been selected.
			if (GameManager.CheckPlayerCharactersIndex(-1, m_CurrentImage) == false)
			{
				// If the current image is less than the amount of portraits there are - 1, select the next image in the array.
				if (m_CurrentImage < m_PlayerPortraits.Length - 1)
					++m_CurrentImage;
				// Else, set the current image to 0.
				else
					m_CurrentImage = 0;
			}
			else
				break;
		}

		// Set the portrait to the current image.
		UpdateImage();
	}

	/// <summary>
	/// Move on to the previous character.
	/// </summary>
	public void PrevCharacter()
	{
		// If the current image is less than the amount of portraits there are - 1, select the previous image in the array.
		if (m_CurrentImage > 0)
			--m_CurrentImage;
		// Else, set the current image to the amount of portraits there are - 1.
		else
			m_CurrentImage = m_PlayerPortraits.Length - 1;

		// This goes through the array of characters, finding the next character that someone hasn't selected.
		while (true)
		{
			// If there isn't -1 at the index of the current image, select the previous character.
			// Else, break out of the loop.
			if (GameManager.CheckPlayerCharactersIndex(-1, m_CurrentImage) == false)
			{
				// If the current image is greater than 0, select the next image in the array.
				if (m_CurrentImage > 0)
					--m_CurrentImage;
				// Else, set the current image to the last image in the array.
				else
					m_CurrentImage = m_PlayerPortraits.Length - 1;
			}
			else
				break;
		}
		// Set the portrait to the current image.
		UpdateImage();
	}

	/// <summary>
	/// Get the current image of the image box.
	/// </summary>
	/// <returns>The current image of the image box.</returns>
	public int GetCurrentImage() { return m_CurrentImage; }

	/// <summary>
	/// Set the current image.
	/// </summary>
	/// <param name="newCurrentImage">The image for the current image to be set to.</param>
	public void SetCurrentImage(int newCurrentImage) { m_CurrentImage = newCurrentImage; }

	/// <summary>
	/// Update the current image.
	/// </summary>
	private void UpdateImage()
	{
		// Get the sprite and the animator controller of the game object in the array and set it to this objects sprite renderer and animator.
		GetComponent<SpriteRenderer>().sprite = m_PlayerPortraits[m_CurrentImage].GetComponent<SpriteRenderer>().sprite;
		GetComponent<Animator>().runtimeAnimatorController = m_PlayerPortraits[m_CurrentImage].GetComponent<Animator>().runtimeAnimatorController;
	}
}