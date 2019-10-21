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
				m_AButtonPressed = XCI.GetButton(XboxButton.A, XboxController.First);
				break;

				// Player 2.
			case 1:
				m_AButtonPressed = XCI.GetButton(XboxButton.A, XboxController.Second);
				break;

				// Player 3.
			case 2:
				m_AButtonPressed = XCI.GetButton(XboxButton.A, XboxController.Third);
				break;

				// Player 4.
			case 3:
				m_AButtonPressed = XCI.GetButton(XboxButton.A, XboxController.Fourth);
				break;

				// Something is wrong.
			default:
				Debug.Log("One of the image boxes doesn't have a player number assigned.");
				break;
		}

		// If the A button has been pressed, lock in the current character.
		if (m_AButtonPressed == true)
		{
			// If a character hasn't been locked in.
			if (m_CharacterLockedIn == false)
				LockInCharacter();
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
		gameObject.transform.parent.GetChild(2).GetComponent<Button>().interactable = false;
	}
}