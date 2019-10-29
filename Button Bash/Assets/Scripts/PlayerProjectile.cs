using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
	// The force on the projectile.
	public float m_Force;

	// The rigidbody on the projectile.
	private Rigidbody m_Rigidbody;

	// The timer for the projectile.
	public float m_Timer;

   // Constructor.
    void Awake()
    {
		// Get the rigidbody of the projectile.
		m_Rigidbody = GetComponent<Rigidbody>();
    }

   // Update the projectile. Fixed update for physics.
    void FixedUpdate()
    {
		// Move the projectile by it's position + the foward direction (foward for the game's orientation, it's actually left) * the force on the projectile.
		m_Rigidbody.MovePosition(transform.position + (-transform.right) * m_Force);

		// If the timer is equal to 0, destroy the projectile.
		if (m_Timer <= 0.0f)
			Destroy(gameObject);

		// Else, decrease the projectile by delta time.
		else
			m_Timer -= Time.deltaTime;
    }
}