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

    //flinging the enemy when they have no health
    public float verticalFling = 50;
    public float xFling = 10;
    public float zFling = 10;

    //used to make sure the next babushka is a colour of the remaining players
    // The script that is storing all the player's lives
    private playerLives m_PlayerLives;
    // The collider that holds the player lives.
    private GameObject m_PlayerLivesCollider;

    private bool valid = false;
    private Colours.Colour newColour;
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
        //get random fling values
        float minXFling = gameObject.GetComponent<EnemyBehaviour>().m_Speed;
        xFling = Random.Range(-xFling, -minXFling);
        zFling = Random.Range(-zFling, zFling);

        m_PlayerLivesCollider = GameObject.Find("Collider");
            m_PlayerLives = m_PlayerLivesCollider.GetComponentInChildren<playerLives>();


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
            //soud trest alex
            
              //  GetComponent<AudioSource>().Play();
           

        }
    }
    
    private void createNextLevel(string newTag)
    {
        //creates next level babushka
        GameObject enemy = Instantiate(nextLevel, (transform.position + addedVector), transform.rotation);
        enemy.transform.localScale -= new Vector3(scale, scale, scale);
        enemy.gameObject.tag = newTag;

        while (!valid)
        {
            switch(Random.Range(0,4))
            {
                case 0:
                    {
                        newColour = Colours.Colour.Blue;
                        break;
                    }
                case 1:
                    {
                        newColour = Colours.Colour.Red;
                        break;
                    }
                case 2:
                    {
                        newColour = Colours.Colour.Green;
                        break;
                    }
                case 3:
                    {
                        newColour = Colours.Colour.Yellow;
                        break;
                    }
            }
            
            switch(newColour)
            {
                case Colours.Colour.Blue:
                    {
                        int p1 = m_PlayerLives.player1Lives;
                        if (p1 > 0)
                        { 
                            valid = true;
                            Debug.Log("Blue");
                        }
                        break;
                    }
                case Colours.Colour.Red:
                    {
                        int p2 = m_PlayerLives.player2Lives;
                        if (p2 > 0)
                        { 
                            valid = true;
                            Debug.Log("Red");
                        }
                        break;
                    }
                case Colours.Colour.Green:
                    {
                        int p3 = m_PlayerLives.player3Lives;
                        if (p3 > 0)
                        {
                            valid = true;
                            Debug.Log("Green");
                        }
                        break;
                    }
                case Colours.Colour.Yellow:
                    {
                        int p4 = m_PlayerLives.player4Lives;
                        if ( p4 > 0)
                        {
                            valid = true;
                            Debug.Log("Yellow");
                        }
                        break;
                    }

            }


        }

        enemy.GetComponent<EnemyBehaviour>().SetBabushkaColour((Colours.Colour)newColour);
        
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        
    }

}