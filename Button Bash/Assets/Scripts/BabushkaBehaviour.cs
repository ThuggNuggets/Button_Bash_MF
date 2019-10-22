using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//allows splitting when hit with an object tagged with "bullet"
//splitting wil only occur if babushka is tagged with either "babushkaLarge" or "babushkaMedium"

/*put this code on all babushka enemies*/
public class BabushkaBehaviour : MonoBehaviour
{
    public GameObject nextLevel;
    //where the new babushka doll spawns
    private Vector3 addedVector =new Vector3 (-3,0,0);
    //health for the babushka
    public float health = 2;

    //walls
    public GameObject leftWall;
    public GameObject rightWall;
    // the size reduction of the next level babushka
    public float scale = 1;

    // The colour of the enemy.
    public Colours.Colour m_Colour = Colours.Colour.None;

    // The material of the enemy.
    private Material m_Material;
    //flinging the enemy when they have no health
    public float verticalFling = 50;
    public float xFling = 10;
    public float zFling = 10;
    private void Awake()
    {
        if(leftWall.transform.position.z + 13 > transform.position.z)
        {
            addedVector = new Vector3(0, 0, 3);
        }
        else  if (rightWall.transform.position.z - 13 < transform.position.z)
        {
            addedVector = new Vector3(0, 0, -3);
        }

        // Get the material of this enemy.
        m_Material = GetComponent<Renderer>().material;
        //get random fling values
        float minXFling = gameObject.GetComponent<EnemyBehaviour>().m_Speed;
        xFling = Random.Range(-xFling, -minXFling);
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
        // If the bullet collides with an enemy and the enemy shares a colour with the bullet, destroy the bullet.
        if (collision.gameObject.tag == "bullet")
        {
            Destroy(collision.gameObject);
            if (gameObject.tag == "babushkaLarge" )
            {
                if (health > 1)
                {
                    createNextLevel("babushkaMedium");
                }
                health--;
            }
            else if(gameObject.tag == "babushkaMedium")
            {
                if (health > 1)
                {
                    createNextLevel("babushkaSmall");
                }
                health--;
            }
            else
            {
                health = 0;
            }

        }
    }
    
    private void createNextLevel(string newTag)
    {
        //creates next level babushka
        GameObject enemy = Instantiate(nextLevel, (transform.position + addedVector), transform.rotation);
        enemy.transform.localScale -= new Vector3(scale, scale, scale);
        enemy.gameObject.tag = newTag;
        enemy.GetComponent<EnemyBehaviour>().SetBabushkaColour((Colours.Colour)Random.Range((int)Colours.Colour.Red, (int)Colours.Colour.Count));
        
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        
    }

}