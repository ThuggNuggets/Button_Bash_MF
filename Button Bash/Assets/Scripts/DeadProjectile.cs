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
	/// If the dead player projectile has collided with something yet.
	/// </summary>
	private bool m_Collided = false;

	/// <summary>
	/// The speed at which the button is shot up into the air.
	/// </summary>
	public float m_FlingSpeed;

	private int m_RotationIterator = 0;

	public float m_XFling;

	public float m_ZFling;

	/// <summary>
	/// On startup.
	/// </summary>
	private void Awake()
	{
		// Get the rigidbody of the projectile.
		// For moving the projectile.
		m_Rigidbody = GetComponent<Rigidbody>();

		m_XFling = Random.Range(-m_XFling, m_XFling);
		m_ZFling = Random.Range(-m_ZFling, m_ZFling);
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
			// If the button has not collided with something, move the button foward.
			// Else, fling the button up into the air and rotate around.
			if (m_Collided == false)
				m_Rigidbody.MovePosition(transform.position + ((-transform.right) * m_Force * Time.deltaTime));
			else
			{
				transform.Translate(new Vector3(m_XFling, m_FlingSpeed, m_ZFling) * Time.deltaTime, Space.World);

				// Rotate the button, so it isn't boring.
				switch(m_RotationIterator)
				{
					case 0:
						transform.Rotate(10, 0, 0);
						m_RotationIterator = 1;
						break;

					case 1:
						transform.Rotate(0, 10, 0);
						m_RotationIterator = 2;
						break;

					case 2:
						transform.Rotate(0, 0, 10);
						m_RotationIterator = 0;
						break;
				}
			}
		}
    }

	/// <summary>
	/// When the button collides.
	/// </summary>
	/// <param name="collision">What the button collided with.</param>
	private void OnCollisionEnter(Collision collision)
	{
		// We want the button to fly up into the air when it collides with something.

		m_Collided = true;
	}
}
