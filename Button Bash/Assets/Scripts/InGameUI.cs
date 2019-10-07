using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
	// The text box.
	private Text m_Text;

	void Awake()
	{
		// Get the text box the script is attatched to.
		m_Text = GetComponent<Text>();
	}

	// Update the text on the text object.
	// Params: the new text for the text box.
	public void UpdateText(string newText)
	{
		m_Text.text = newText;
	}
}