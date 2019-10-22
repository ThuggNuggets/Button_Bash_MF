using System.Collections;
using System.Collections.Generic;
using XboxCtrlrInput;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class SelectCharacter : MonoBehaviour
{
	// The lock in character event.
	UnityEvent m_LockInCharacter;

	// The image box.
	public RawImage m_PlayerImageBox;

	// The player number.
	private int m_PlayerNumber = 0;

	// If the A button was pressed on the controller.
	private bool m_AButtonPressed = false;

	// If the B button was pressed on the controller.
	private bool m_BButtonPressed = false;

	// If the character has been locked in.
	private bool m_CharacterLockedIn = false;

	private void Awake()
	{
		// Create the lock in character event.
		m_LockInCharacter = new UnityEvent();

		// Add the lock in character fucntion to the event.
		m_LockInCharacter.AddListener(LockInCharacter);

		// Get the player number of this button's parent.
		m_PlayerNumber = GetComponentInParent<CharacterSelect>().GetPlayerNumber();
	}

	// Update.
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

	// Lock in the character.
	public void LockInCharacter()
	{
		// Get the character the player selected.
		int playerSelect = m_PlayerImageBox.GetComponent<CharacterSelect>().GetCurrentImage();

		// Add the player image box to the game manager.
		GameManager.AddPlayerCharacter(m_PlayerNumber, playerSelect);

		// Set that the character has been locked in to true.
		m_CharacterLockedIn = true;

		// Set the select button to not be interactable (just for representation).
		GetComponent<Button>().interactable = false;
	}

	// Unlock the currently selected character.
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