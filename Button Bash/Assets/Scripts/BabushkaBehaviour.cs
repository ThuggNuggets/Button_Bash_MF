using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//allows splitting when hit with an object tagged with "bullet"
//splitting wil only occur if babushka is tagged with either "babushkaLarge" or "babushkaMedium"

/*put this code on all babushka enemies*/
public class BabushkaBehaviour : MonoBehaviour
{
    public GameObject nextLevel;
    private Vector3 addedVector =new Vector3 (0,0,3);
    //health for the babushka
    public float health = 2;

    // the size reduction of the next level babushka
    public float scale = 1;

    private void OnCollisionEnter(Collision collision)
    {
        // If the bullet collides with an enemy and the enemy shares a colour with the bullet, destroy the bullet.
        if (collision.gameObject.tag == "bullet")
        {
            Destroy(collision.gameObject);
            if (gameObject.tag == "babushkaLarge" )
            {
                if (health > 1)
                {
                    //creates next level babushka
                    GameObject enemy = Instantiate(nextLevel, (transform.position + addedVector), transform.rotation);
                    enemy.transform.localScale -= new Vector3(scale, scale, scale);
                    enemy.gameObject.tag = "babushkaMedium";
                    enemy.GetComponent<EnemyBehaviour>().SetColour((Colours.Colour)Random.Range((int)Colours.Colour.Red, (int)Colours.Colour.Count));
                }
                health--;
            }
            else if(gameObject.tag == "babushkaMedium")
            {
                if (health > 1)
                {
                    //creates next level babushka
                    GameObject enemy = Instantiate(nextLevel, (transform.position + addedVector), transform.rotation);
                    enemy.transform.localScale -= new Vector3(scale, scale, scale);
                    enemy.gameObject.tag = "babushkaSmall";
                    enemy.GetComponent<EnemyBehaviour>().SetColour((Colours.Colour)Random.Range((int)Colours.Colour.Red, (int)Colours.Colour.Count));
                }
                health--;
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