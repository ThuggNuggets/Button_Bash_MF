using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabushkaBehaviour : MonoBehaviour
{
    public GameObject nextLevel = null;

    //health for the babushka
    public float health = 2;
    //walls of the spawn area
    public GameObject LeftWall;
    public GameObject RightWall;

    private void OnCollisionEnter(Collision collision)
    {
        // If the bullet collides with an enemy and the enemy shares a colour with the bullet, destroy the bullet.
        if (collision.gameObject.tag == "bullet")
        {
            Destroy(collision.gameObject);
            if (gameObject.tag == "babushkaLarge" || gameObject.tag == "babushkaMedium")
            {
                //if it is the large or medium babushka span smaller one next to it then reduce health
                health--;
                Vector3 addedVector;
                if (health > 0)
                {
                    // creates the next babushka doll to the positive z of the current one
                    if(LeftWall.transform.position.z + 4 > transform.position.z)
                    {
                    addedVector = new Vector3(0, 0, 3);
                    }
                    else if(RightWall.transform.position.z - 4 < transform.position.z)
                    {
                        addedVector = new Vector3(0, 0, -3);
                    }
                    else
                    {
                        addedVector = new Vector3(-3, 0, 0);
                    }
                    GameObject enemy = Instantiate(nextLevel, (transform.position + addedVector), transform.rotation);
                    enemy.GetComponent<EnemyBehaviour>().SetColour((Colours.Colour)Random.Range((int)Colours.Colour.Red, (int)Colours.Colour.Count));
                }
                Destroy(collision.gameObject);
            }
            else
            {
                // Destroy self if it is the small babushka.
                Destroy(gameObject);
            }

            if (health <= 0)
            {
                // Destroy self.
                Destroy(gameObject);
            }
        }
    }

    
}