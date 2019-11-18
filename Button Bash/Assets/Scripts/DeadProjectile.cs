using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadProjectile : MonoBehaviour
{
	/// <summary>
	/// The speed on the projectile.
	/// </summary>
	public float m_Force;

	/// <summary>
	/// The rigidbody of the projectile.
	/// </summary>
	private Rigidbody m_Rigidbody;

	/// <summary>
	/// The timer for despawning the projectile.
	/// </summary>
	public float m_DespawnTimer;

	/// <summary>
	/// On startup.
	/// </summary>
	private void Awake()
	{
		// Get the rigidbody of the projectile.
		// For moving the projectile.
		m_Rigidbody = GetComponent<Rigidbody>();
	}

	/// <summary>
	/// Update.
	/// </summary>
	void Update()
	{
		// Check the despawn timer, if it is less than or equal to 0, despawn the projectile,
		// else decrease the timer.
		if (m_DespawnTimer <= 0.0f)
			Destroy(gameObject);
		else
		{
			m_DespawnTimer -= Time.deltaTime;

			// Move the projectile foward.
			m_Rigidbody.MovePosition(transform.position + ((-transform.right) * m_Force * Time.deltaTime));
		}
    }

	/// <summary>
	/// When the button collides.
	/// </summary>
	/// <param name="collision">What the button collided with.</param>
	private void OnCollisionEnter(Collision collision)
	{
		transform.Rotate(new Vector3(0,0, -45));
	}
}
