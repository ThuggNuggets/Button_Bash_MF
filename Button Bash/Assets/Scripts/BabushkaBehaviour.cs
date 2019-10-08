using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//allows splitting when hit with an object tagged with "bullet"
//splitting wil only occur if babushka is tagged with either "babushkaLarge" or "babushkaMedium"
//requires two objects for the walls to decide where the new babushka doll spawns

/*put this code on all babushka enemies*/
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
                    // creates the next babushka doll based off how close it is to the walls so it doesnt spawn it out of bounds/add variety
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
                    //creates next level babushka
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
                // Destroy self if no health left
                Destroy(gameObject);
            }
        }
    }

    
}