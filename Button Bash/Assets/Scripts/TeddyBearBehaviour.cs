using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//increases object size the more it gets hit with objects tagged with "bullet"
/*put this code on the Teddy bear enemy*/
public class TeddyBearBehaviour : MonoBehaviour
{
    //hits until it is defeated
    public float health = 5;
    // amount its size increased each time
    public float scale = 0.5f;
    private void OnCollisionEnter(Collision collision)
    {
        // If the bullet collides with an enemy and the enemy shares a colour with the bullet, destroy the bullet.
        if (collision.gameObject.tag == "bullet")
        {
            //destroy the bullet
                Destroy(collision.gameObject);
                health--;
                if (health > 0)
                {
                //increases size of bear
                transform.localScale += new Vector3(scale, scale, scale);
                }

            if (health <= 0)
            {
                // Destroy self.
                Destroy(gameObject);
            }
        }
    }

}