using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class DeadPlayerReticle : MonoBehaviour
{
	/// <summary>
	/// The player number of the reticle.
	/// </summary>
	public int m_PlayerNumber;

	/// <summary>
	/// The x axis of the controller input.
	/// </summary>
	private float m_XAxis;

	/// <summary>
	/// The y axis of the controller input.
	/// </summary>
	private float m_YAxis;

    void Awake()
    {
        
    }

    void Update()
    {
		m_XAxis = XCI.GetAxis(XboxAxis.LeftStickX, (XboxController)m_PlayerNumber + 1);
		m_YAxis = XCI.GetAxis(XboxAxis.LeftStickY, (XboxController)m_PlayerNumber + 1);

		Vector3 translation = new Vector3(m_XAxis, m_YAxis, 0);

		translation *= Time.deltaTime;
		transform.Translate(translation);
    }
}