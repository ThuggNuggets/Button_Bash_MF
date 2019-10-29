using System.Collections;
using System.Collections.Generic;
using XboxCtrlrInput;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class SelectCharacter : MonoBehaviour
{
	/// <summary>
	/// The lock in character event.
	/// </summary>
	UnityEvent m_LockInCharacter;

	/// <summary>
	/// The image box.
	/// </summary>
	public RawImage m_PlayerImageBox;

	/// <summary>
	/// The player number.
	/// </summary>
	private int m_PlayerNumber = 0;

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
	/// On startup.
	/// </summary>
	private void Awake()
	{
		// Create the lock in character event.
		m_LockInCharacter = new UnityEvent();

		// Add the lock in character fucntion to the event.
		m_LockInCharacter.AddListener(LockInCharacter);

		// Get the player number of this button's parent.
		m_PlayerNumber = GetComponentInParent<CharacterSelect>().GetPlayerNumber();
	}

	/// <summary>
	/// Update.
	/// </summary>
	private void Update()
	{
		// Check the player number.
		switch (m_PlayerNumber)
		{
				// Player 1.
			case 0:
				m_AButtonPressed = XCI.GetButtonDown(XboxButton.A, XboxController.First);
				m_BButtonPressed = XCI.GetButtonDown(XboxButton.B, XboxController.First);
				break;

				// Player 2.
			case 1:
				m_AButtonPressed = XCI.GetButtonDown(XboxButton.A, XboxController.Second);
				m_BButtonPressed = XCI.GetButtonDown(XboxButton.B, XboxController.Second);
				break;

				// Player 3.
			case 2:
				m_AButtonPressed = XCI.GetButtonDown(XboxButton.A, XboxController.Third);
				m_BButtonPressed = XCI.GetButtonDown(XboxButton.B, XboxController.Third);
				break;

				// Player 4.
			case 3:
				m_AButtonPressed = XCI.GetButtonDown(XboxButton.A, XboxController.Fourth);
				m_BButtonPressed = XCI.GetButtonDown(XboxButton.B, XboxController.Fourth);
				break;

				// Something is wrong.
			default:
				Debug.Log("An image box has the player number " + m_PlayerNumber + " assigned. The player numbers are between 0 - 3.");
				break;
		}

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
	/// Lock in the character.
	/// </summary>
	public void LockInCharacter()
	{       
         // Get the character the player selected.
        int playerSelect = m_PlayerImageBox.GetComponent<CharacterSelect>().GetCurrentImage();

		// Add the player image box to the game manager.
		GameManager.AddPlayerCharacter(m_PlayerNumber, playerSelect);

		// Set that the character has been locked in to true.
		m_CharacterLockedIn = true;

		// Play a sound when a character is selected, with the sound correlating with the character that was selected.
		AudioSource ac = GetComponent<AudioSource>();
		SoundManager sm = GameObject.Find("Sound bucket ").GetComponent<SoundManager>();
		switch (playerSelect)
		{
			case 0:
				ac.clip = sm.m_SoundClips[8];
				break;
			case 1:
				ac.clip = sm.m_SoundClips[7];
				break;
			case 2:
				ac.clip = sm.m_SoundClips[6];
				break;
			case 3:
				ac.clip = sm.m_SoundClips[9];
				break;
		}

		ac.Play();

		// Set the select button to not be interactable (just for representation).
		GetComponent<Button>().interactable = false;

		// Find all the image boxes in the scene to compare them.
		// So if two people have the same character selected, 
		// the person that didn't lock them in gets moved on to the next unlocked character.
		GameObject[] imgBoxes = GameObject.FindGameObjectsWithTag("Image Box");

		// Go through each of the image boxes.
		for (int i = 0; i < imgBoxes.Length; ++i)
		{
			// So we don't compare with ourself.
			if (imgBoxes[i] != transform.parent.gameObject)
			{
				// If the current character of the other image box is the same as this one, move the other image box to the next character.
				if (imgBoxes[i].GetComponent<CharacterSelect>().GetCurrentImage() == GetComponentInParent<CharacterSelect>().GetCurrentImage())
				{
					imgBoxes[i].GetComponent<CharacterSelect>().NextCharacter();
				}
			}
		}
	}

	/// <summary>
	/// Unlock the currently selected character.
	/// </summary>
	public void UnlockCharacter()
	{
		// Get the character the player selected.
		int playerSelect = m_PlayerImageBox.GetComponent<CharacterSelect>().GetCurrentImage();

		// Remove the player image box from the game manager.
		GameManager.RemovePlayerCharacter(playerSelect);

		// Set that the character has been locked in to false.
		m_CharacterLockedIn = false;

		// Set the select button to be interactable (just for representation).
		GetComponent<Button>().interactable = true;
	}
}