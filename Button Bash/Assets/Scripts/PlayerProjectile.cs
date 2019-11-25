using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
	/// <summary>
	/// The force on the projectile.
	/// </summary>
	public float m_Force;

	/// <summary>
	/// The rigidbody on the projectile.
	/// </summary>
	private Rigidbody m_Rigidbody;

	/// <summary>
	/// The timer for the projectile.
	/// </summary>
	public float m_Timer;

	/// <summary>
	/// On startup.
	/// </summary>
    void Awake()
    {
		// Get the rigidbody of the projectile.
		m_Rigidbody = GetComponent<Rigidbody>();
    }

	/// <summary>
	/// Update the projectile. Fixed update for physics.
	/// </summary>
	void FixedUpdate()
    {
		// Move the projectile by it's position + the foward direction (foward for the game's orientation, it's actually left) * the force on the projectile.
		//m_Rigidbody.MovePosition(transform.position - transform.right * m_Force);
        transform.Translate(new Vector3(-m_Force, 0, 0) * Time.deltaTime, Space.World);

        // If the timer is equal to 0, destroy the projectile.
        if (m_Timer <= 0.0f)
			Destroy(gameObject);

		// Else, decrease the projectile by delta time.
		else
			m_Timer -= Time.deltaTime;
    }
}