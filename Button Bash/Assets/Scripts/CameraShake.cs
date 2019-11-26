using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
	/// <summary>
	/// The original position of the camera before screen shake.
	/// </summary>
	Vector3 m_OriginalPosition;

	bool m_ScreenShake = false;

	Vector3 m_ShakePosition;

	public float m_ShakeAmount;

	private void Awake()
	{
		m_OriginalPosition = transform.position;
		m_ShakePosition = m_OriginalPosition;
	}

	// Update is called once per frame
	void Update()
    {
        if (m_ScreenShake == true)
		{
			m_ShakePosition.y = m_OriginalPosition.y + Random.Range(-m_ShakeAmount, m_ShakeAmount);
			m_ShakePosition.z = m_OriginalPosition.z + Random.Range(-m_ShakeAmount, m_ShakeAmount);

			transform.position = m_ShakePosition;
		}
    }

	public void SetScreenShake(bool shake)
	{
		m_ScreenShake = shake;
		if (m_ScreenShake == false)
			transform.position = m_OriginalPosition;
	}
}