using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
	// The force on the projectile.
	public float m_Force;

	// The rigidbody on the projectile.
	private Rigidbody m_Rigidbody;

	// The material of the projectile.
	private Material m_Material;

	// The timer for the projectile.
	public float m_Timer;

	// The colour of the projectile.
	private Colours.Colour m_Colour = Colours.Colour.None;

   // Constructor.
    void Awake()
    {
		// Get the material of the projectile.
		m_Material = GetComponent<Renderer>().material;

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

	// Set the colour of the projectile.
	// Params: the colour of the projectile.
	public void SetColour(Colours.Colour colour)
	{
		// Try to assign a colour to the projectile.
		//try
		//{
			// Check the colour being set and set the projectile's colour to the appropriate colour.
			switch (colour)
			{
				// Set colour to red.
				case Colours.Colour.Red:
					m_Material.color = Color.red;
					break;

				// Set colour to blue.
				case Colours.Colour.Blue:
					m_Material.color = Color.blue;
					break;

				// Set colour to green.
				case Colours.Colour.Green:
					m_Material.color = Color.green;
					break;

				// Set colour to yellow.
				case Colours.Colour.Yellow:
					m_Material.color = Color.yellow;
					break;

				// Set colour to magenta, something went wrong with colour assignment.
				default:
					m_Material.color = Color.magenta;
					break;
			}

			// Set the colour of the projectile to the colour it is being assigned.
			m_Colour = colour;
		//}
		// Else do nothing.
		//catch { }
	}
}