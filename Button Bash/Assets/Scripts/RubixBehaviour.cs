using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubixBehaviour : MonoBehaviour
{
    public float health = 3.0f;
    private void OnCollisionEnter(Collision collision)
    {
        // If the bullet collides with an enemy destroy the bullet.
        if (collision.gameObject.tag == "bullet")
        {
            Destroy(collision.gameObject);
            health--;
            if(health > 0)
            {
            gameObject.GetComponent<EnemyBehaviour>().SetColour((Colours.Colour)Random.Range((int)Colours.Colour.Red, (int)Colours.Colour.Count));
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
