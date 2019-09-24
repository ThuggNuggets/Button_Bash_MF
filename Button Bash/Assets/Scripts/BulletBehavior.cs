using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
	// The force on the bullet.
	public float m_Force = 3.0f;

	// The rigidbody.
	private Rigidbody m_Rigidbody;
	
	// The material of the bullet.
	private Material m_Material;

	// The timer for the bullet.
	public float m_Timer;

	// The colour of the bullet.
	private Colours.Colour m_Colour;

    //  Constructor.
	void Awake()
    {
		// Get the material of the bullet.
		m_Material = GetComponent<Renderer>().material;

		// Get the rigidbody.
		m_Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		// Move the bullet.
		m_Rigidbody.MovePosition(transform.position + (transform.forward * Time.deltaTime * m_Force));
        
		// If the timer is 0, delete this bullet
		if (m_Timer <= 0.0f)
			Destroy(gameObject);
		// Else, decrease the timer.
		else
			m_Timer -= Time.deltaTime;
    }

	// On collision.
	private void OnCollisionEnter(Collision collision)
	{
		// If the bullet collides with an enemy and the enemy shares a colour with the bullet, destroy the bullet.
		if (collision.gameObject.tag == "Enemy" /*|| collision.gameObject.tag == "babushkaLarge"|| collision.gameObject.tag == "babushkaMedium"*/|| collision.gameObject.tag == "babushkaSmall")
		{
                // Destroy the enemy.
                Destroy(collision.gameObject);
                // Destroy the bullet.
                Destroy(gameObject);
         }
	}

	// Set the colour of the bullet.
	public void SetColour(Colours.Colour colour)
	{
		// Set the colour of the bullet's material.
		switch ((int)colour)
		{
			// Set the material colour to red.
			case 1:
				m_Material.color = Color.red;
				break;

			// Set the material colour to blue.
			case 2:
				m_Material.color = Color.blue;
				break;

			// Set the material colour to green.
			case 3:
				m_Material.color = Color.green;
				break;

			// Set the material colour to yellow.
			case 4:
				m_Material.color = Color.yellow;
				break;

			// Set the material colour to magenta, something went WRONG!
			default:
				m_Material.color = Color.magenta;
				break;
		}

		m_Colour = colour;
	}

	// Set the force of the bullet.
	public void SetForce(float force) { m_Force = force; }

	// Get the colour of the bullet.
	public Colours.Colour GetColour() { return m_Colour; }
}