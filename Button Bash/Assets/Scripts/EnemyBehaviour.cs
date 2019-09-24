using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
	// Speed of the enemy.
	public float m_Speed = 2;

	// The end of movement trigger.
	public GameObject m_EndingTrigger = null;

	// The rigidbody.
	private Rigidbody m_Rigidbody;

	// The colour of the enemy.
	private Colours.Colour m_Colour = Colours.Colour.None;

	// The material of the enemy.
	private Material m_Material;

    // Start is called before the first frame update
    void Awake()
    {
		// Get the material of this enemy.
		m_Material = GetComponent<Renderer>().material;

		// Get the rigidbody of this enemy.
		m_Rigidbody = GetComponent<Rigidbody>();
    }

    // Update.
    void FixedUpdate()
    {
        // Move fowards at it's speed.
        m_Rigidbody.MovePosition(transform.position + (-transform.forward) * m_Speed * Time.deltaTime);

        // If the enemy reaches the end trigger, destroy the enemy.
        if (transform.position.z <= m_EndingTrigger.transform.position.z+0.5f)
            Destroy(gameObject);
    }

	// Get this enemy's colour.
	public Colours.Colour GetColour() { return m_Colour; }

	// Set the enemy's colour.
	public void SetColour(Colours.Colour colour)
	{
		// Set the colour of the enemy's material.
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
}