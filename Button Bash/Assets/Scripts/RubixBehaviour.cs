using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Code for the Rubix cube enemy which allows the changing colour when hit with something with the tag "bullet"
/*put this code on the Rubix enemy*/
public class RubixBehaviour : MonoBehaviour
{
    public float health = 3.0f;
    private int rotateDirection = 0;

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
            gameObject.GetComponent<EnemyBehaviour>().SetColour((Colours.Colour)Random.Range((int)Colours.Colour.Red, (int)Colours.Colour.Count));
            }
            else
            {
                Destroy(gameObject);
            }
            //when hit rotate body
            switch(rotateDirection)
            {
                case 1:
                    {
                       transform.Rotate(90, 0, 0, Space.Self);
                        break;
                    }
                case 2:
                    {
                        transform.Rotate(-90, 0, 0, Space.Self);
                        break;
                    }
                case 3:
                    {
                        transform.Rotate(0, 90, 0, Space.Self);
                        break;
                    }
                case 4:
                    {
                        transform.Rotate(0, -90, 0, Space.Self);
                        break;
                    }
                case 5:
                    {
                        transform.Rotate(0, 0, 90, Space.Self);
                        break;
                    }
                case 6:
                    {
                        transform.Rotate(0, 0, -90, Space.Self);
                        break;
                    }
            }
        }
    }
}
