using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Controls the movement of enemies as well as the colour that each enemy holds
/*put this code on all enemies*/
public class EnemyBehaviour : MonoBehaviour
{
    // Speed of the enemy.
    public float m_Speed = 2;

    // The end of movement trigger.
    public GameObject m_EndingTrigger = null;

    // The rigidbody.
    private Rigidbody m_Rigidbody;

    // The colour of the enemy.
    public Colours.Colour m_Colour = Colours.Colour.None;

    // The material of the enemy.
    private Material m_Material;

    // Start is called before the first frame update
    void Awake()
    {
        // Get the material of this enemy.
        m_Material = GetComponent<Renderer>().material;

        // Get the rigidbody of this enemy.
        m_Rigidbody = GetComponent<Rigidbody>();
        //changes the colousr of the enemy when it is pawned
       
    }

    // Update.
    void FixedUpdate()
    {
        // Move fowards at it's speed.
        transform.Translate(new Vector3(m_Speed, 0, 0) * Time.deltaTime, Space.World);

    }
        
    private void OnCollisionEnter(Collision collision)
    {
        // If the enemy reaches the end trigger x position, destroy the enemy.
        if (collision.gameObject.tag == "endingTrigger")
        {
            Destroy(gameObject);
        }
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
            case 0:            
                m_Colour = Colours.Colour.Red;
                break;

            // Set the material colour to blue.
            case 1:
                m_Colour = Colours.Colour.Blue;
                break;

            // Set the material colour to green.
            case 2:
                m_Colour = Colours.Colour.Green;
                break;

            // Set the material colour to yellow.
            case 3:
                m_Colour = Colours.Colour.Yellow;

                break;

            // Set the material colour to magenta, something went WRONG!
            default:
                break;
        }

    }
}