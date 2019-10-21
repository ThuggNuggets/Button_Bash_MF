using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBalloonBehaviour : MonoBehaviour
{
	// If the balloon is active.
	private bool m_Active;

	// The timer for despawning the balloons.
	public float m_Timer;

	// The upwards float force for the balloon.
	public float m_BalloonUpwardFloatForce;

	// Update the balloon.
	public void Update()
	{
		// If the balloon is active, check the despawn timer.
		if (m_Active == true)
		{
			// If the timer is equal to or less than 0, despawn the balloon.
			if (m_Timer <= 0.0f)
				Destroy(gameObject);
			// Else, decrease the despawn timer.
			else
				m_Timer -= Time.deltaTime;
		}
	}

	// Set if the balloon is active or not.
	// Params: if the balloon is active or not.
	public void SetActive(bool newActive)
	{
		m_Active = newActive;

		// If the balloon has been set to active, have the balloon float up.
		if (m_Active == true)
		{
			// Get the rigidbody of the balloon, to add force.
			Rigidbody body = gameObject.GetComponent<Rigidbody>();
			// Unfreeze  position, just in case they are frozen.
			body.constraints = RigidbodyConstraints.FreezeRotation;
			// Add upwards force the the balloon.
			body.AddForce(new Vector3(0, m_BalloonUpwardFloatForce, 0));
		}
	}
}
