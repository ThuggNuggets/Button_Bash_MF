using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Code for the Rubix cube enemy which allows the changing colour when hit with something with the tag "bullet"
/*put this code on the Rubix enemy*/
public class RubixBehaviour : MonoBehaviour
{
    public float health = 3.0f;
    private Colours.Colour m_colour;
    //flinging the enemy when they have no health
    public float verticalFling = 50;
    public float xFling = 10;
    public float zFling = 10;
    private Rigidbody rb;
    private void Awake()
    {
        m_colour = gameObject.GetComponent<EnemyBehaviour>().GetColour();
        //get random fling values
        xFling = Random.Range(-xFling, xFling);
        zFling = Random.Range(-zFling, zFling);
    }
    private void FixedUpdate()
    {
        if (health <= 0)
        {
            transform.Translate(new Vector3(xFling, verticalFling, zFling) * Time.deltaTime, Space.World);
            //destroy self and bullet on collision
            Destroy(gameObject, 2);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        // If the bullet collides with an enemy destroy the bullet.
        if (collision.gameObject.tag == "bullet")
        {
            //destroy bullet
            Destroy(collision.gameObject);
            health--;
            if(health > 0)
            {
            //changes its colour
            transform.Rotate(0, 90, 0, Space.Self);
            }
            switch (m_colour)
            {
                case Colours.Colour.Blue:
                    {
                        m_colour = Colours.Colour.Red;
                        Debug.Log("change to red");
                        break;
                    }
                case Colours.Colour.Red:
                    {
                        m_colour = Colours.Colour.Green;
                        Debug.Log("change to green");
                        break;
                    }
                case Colours.Colour.Yellow:
                    {
                        m_colour = Colours.Colour.Blue;
                        Debug.Log("change to blue");
                        break;
                    }
                case Colours.Colour.Green:
                    {
                        m_colour = Colours.Colour.Yellow;
                        Debug.Log("change to yellow");
                        break;
                    }
            }
            gameObject.GetComponent<EnemyBehaviour>().SetColour(m_colour);
            //when hit rotate body


        }
    }
}
