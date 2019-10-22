using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBalloonBehaviour : MonoBehaviour
{
	// If the balloon is active.
	private bool m_Active;

	// The timer for despawning the balloons.
	public float m_DespawnTimer;

	// The upwards float force for the balloon.
	public float m_BalloonUpwardFloatForce;

	// How far out to swing the balloon while it is floating up.
	public float m_SwingOffset;

	// The speed at which the balloon swings at.
	public float m_SwingSpeed;

	// The time of the start of the lerp for the swing.
	private float m_StartTime;

	// The length of the lerp.
	private float m_JourneyLength;

	// The end position of the lerp.
	private Vector3 m_EndPosition;

	// If the balloon is to swing left or not.
	private bool m_SwingLeft = true;

	// Update the balloon.
	public void Update()
	{
		// If the balloon is active, check the despawn timer.
		if (m_Active == true)
		{
			// Update the y position of the end positon.
			m_EndPosition.y = transform.position.y;

			// If the lerp is to be restarted, update the end positon to the other side.
			if (Vector3.Distance( m_EndPosition, transform.position) < 1.0f)
			{
				// Reset the start time.
				m_StartTime = Time.time;

				// Check if the balloon has to swing left and update the end position accordingly.
				switch (m_SwingLeft)
				{
					// Swing left.
					case true:
						// End position is to the left.
						m_EndPosition.z = transform.position.z - m_SwingOffset * 2;
						// Don't swing left next time.
						m_SwingLeft = false;
						break;
					// Swing right.
					case false:
						// End position is to the right.
						m_EndPosition.z = transform.position.z + m_SwingOffset * 2;
						// Don't swing right next time.
						m_SwingLeft = true;
						break;
				}

				// Find the journey length between the balloon's position and the end position.
				m_JourneyLength = Vector3.Distance(transform.position, m_EndPosition);
			}

			// Else, lerp towards the end point.
			else
			{
				// Get the distance covered so far.
				float distanceCovered = ((Time.time - m_StartTime) * m_SwingSpeed) * Time.deltaTime;

				// Get the distance between the balloon's position and the end point now.
				float fractionOfJourney = distanceCovered / m_JourneyLength;

				// Set the positoin of the balloon along the lerp to the end point.
				transform.position = Vector3.Lerp(transform.position, m_EndPosition, fractionOfJourney);
			}

			// If the timer is equal to or less than 0, despawn the balloon.
			if (m_DespawnTimer <= 0.0f)
				Destroy(gameObject);
			// Else, decrease the despawn timer.
			else
				m_DespawnTimer -= Time.deltaTime;
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
			body.AddForce(Vector3.up * m_BalloonUpwardFloatForce);
			m_EndPosition = transform.position;
			m_EndPosition.z = m_EndPosition.z + m_SwingOffset;

			// Find the journey length between the balloon's position and the end position.
			m_JourneyLength = Vector3.Distance(transform.position, m_EndPosition);
		}
	}
}
