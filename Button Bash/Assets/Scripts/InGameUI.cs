using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
	/// <summary>
	/// The text box.
	/// </summary>
	private Text m_Text;

	/// <summary>
	/// On startup.
	/// </summary>
	void Awake()
	{
		// Get the text box the script is attatched to.
		m_Text = GetComponent<Text>();
	}

	/// <summary>
	/// Update the text on the text object.
	/// </summary>
	/// <param name="newText">The new text for the text box.</param>
	public void UpdateText(string newText)
	{
		m_Text.text = newText;
	}
}