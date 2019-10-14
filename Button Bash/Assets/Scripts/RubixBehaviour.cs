using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Code for the Rubix cube enemy which allows the changing colour when hit with something with the tag "bullet"
/*put this code on the Rubix enemy*/
public class RubixBehaviour : MonoBehaviour
{
    public float health = 3.0f;
    private int rotateDirection = 0;
    private Colours.Colour m_colour;
    private void Awake()
    {
        m_colour = gameObject.GetComponent<EnemyBehaviour>().GetColour();
    }
    private void OnCollisionEnter(Collision collision)
    {
        // If the bullet collides with an enemy destroy the bullet.
        if (collision.gameObject.tag == "bullet")
        {
            //destroy bullet
            Destroy(collision.gameObject);
            health--;
            rotateDirection = Random.Range(1,6);
            if(health > 0)
            {
            //changes its colour
            transform.Rotate(0, 90, 0, Space.Self);
            }
            else
            {
                Destroy(gameObject);
            }

            gameObject.GetComponent<EnemyBehaviour>().SetColour(m_colour);

            switch (m_colour)
            {
                case Colours.Colour.Blue:
                    {
                        m_colour = Colours.Colour.Red;
                        break;
                    }
                case Colours.Colour.Red:
                    {
                        m_colour = Colours.Colour.Green;
                        break;
                    }
                case Colours.Colour.Yellow:
                    {
                        m_colour = Colours.Colour.Blue;
                        break;
                    }
                case Colours.Colour.Green:
                    {
                        m_colour = Colours.Colour.Yellow;
                        break;
                    }
            }
            //when hit rotate body
           
             
        }
    }
}
