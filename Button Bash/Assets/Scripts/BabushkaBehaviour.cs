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
    
    //used to find the new colour for the next babushka level
    private Colours.Colour newColour;
    private bool valid = false;

    //renderer
    private Renderer rend;
    //colour
    private Colours.Colour m_ChildTopColour;
    bool topColourcorrect = false;
    //materials for other babushkas
    ///-------put them in the list blue,red,green,yellow ------
    public Material[] otherBabushkas;
    //flinging
    int flingRotation;

    /// <summary>
    /// sets all of the variables
    /// </summary>
    private void Awake()
    {
        //dictates where the next babushka will spawn based on how close to the side walls they are
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

        //finds the object that hold the player lives 
        m_PlayerLivesCollider = GameObject.Find("Collider");
        m_PlayerLives = m_PlayerLivesCollider.GetComponent<playerLives>();

        flingRotation = Random.Range(0, 2);

        m_ChildTopColour = GetComponent<EnemyBehaviour>().m_Colour;
            foreach (Transform child in transform)
            {
                switch (m_ChildTopColour)
                {
                    case Colours.Colour.Blue:
                        {
                            rend = child.GetComponent<Renderer>();
                            rend.material = otherBabushkas[0];
                            break;
                        }
                    case Colours.Colour.Red:
                        {
                            rend = child.GetComponent<Renderer>();
                            rend.material = otherBabushkas[1];
                            break;
                        }
                    case Colours.Colour.Green:
                        {
                            rend = child.GetComponent<Renderer>();
                            rend.material = otherBabushkas[2];
                            break;
                        }
                    case Colours.Colour.Yellow:
                        {
                            rend = child.GetComponent<Renderer>();
                            rend.material = otherBabushkas[3];
                            break;
                        }

                }
         }
    }

  /// <summary>
  /// changes the top part of the next babushka colour to match and flings it when damage is taken
  /// </summary>
    private void FixedUpdate()
    {
        if (!topColourcorrect)
        {
            m_ChildTopColour = GetComponent<EnemyBehaviour>().m_Colour;
            topColourcorrect = true;
            foreach (Transform hello in transform)
            {
                switch (m_ChildTopColour)
                {
                    case Colours.Colour.Blue:
                        {
                            rend = hello.GetComponent<Renderer>();
                            rend.material = otherBabushkas[0];
                            break;
                        }
                    case Colours.Colour.Red:
                        {
                            rend = hello.GetComponent<Renderer>();
                            rend.material = otherBabushkas[1];
                            break;
                        }
                    case Colours.Colour.Green:
                        {
                            rend = hello.GetComponent<Renderer>();
                            rend.material = otherBabushkas[2];
                            break;
                        }
                    case Colours.Colour.Yellow:
                        {
                            rend = hello.GetComponent<Renderer>();
                            rend.material = otherBabushkas[3];
                            break;
                        }

                }
            }
        }
            //flinging top half of babushka when hit
            foreach (Transform child in transform)
        {
            if (health == 1)
            {
                child.transform.Translate(new Vector3(xFling, verticalFling, zFling) * Time.deltaTime, Space.World);
                Destroy(child.gameObject, 2);
                switch (flingRotation)
                {
                    case 0:
                        {
                            child.transform.Rotate(10, 0, 0);
                            break;
                        }
                    case 1:
                        {
                            child.transform.Rotate(0, 0, 10);
                            break;
                        }

                }
            }
        }
        if (health <= 0)
          {
            //fling the enemy
            transform.Translate(new Vector3(xFling, verticalFling, zFling) * Time.deltaTime, Space.World);
            //destroy self and bullet on collision
            Destroy(gameObject, 2);
            switch (flingRotation)
            {
                case 0:
                    {
                        transform.Rotate(10, 0, 0);
                        break;
                    }
                case 1:
                    {
                        transform.Rotate(0, 0, 10);
                        break;
                    }

            }
          }
    }

    /// <summary>
    /// when the doll hits something if the object it hit had the "bullet" tag then take damage/spawn next doll
    /// </summary>
    /// <param name="collision"></param>
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

            SoundManager sm = GameObject.Find("Sound bucket ").GetComponent<SoundManager>();
            AudioSource ac = GetComponent<AudioSource>();
            ac.clip = sm.m_SoundClips[3];
            ac.Play();


        }
    }
    /// <summary>
    /// 
    /// the creation of the next babushka doll 
    /// </summary>
    /// <param name="newTag"></param>
    private void createNextLevel(string newTag)
    {
        //creates next level babushka
        GameObject enemy = Instantiate(nextLevel, (transform.position + addedVector), transform.rotation);
        enemy.transform.localScale -= new Vector3(scale, scale, scale);
        enemy.gameObject.tag = newTag;

        rend = enemy.GetComponent<Renderer>();
        //changes the enemy oclour based off who still has lives
        while (!valid)
        {
                switch (Random.Range(0, 4))
                {
                    case 0:
                        {
                            if (m_PlayerLives.player1Lives > 0)
                            {
                                valid = true;
                                newColour = Colours.Colour.Blue;
                                rend.material = otherBabushkas[0];
                        }
                            break;
                        }
                    case 1:
                        {
                            if (m_PlayerLives.player2Lives > 0)
                            {
                                valid = true;
                                newColour = Colours.Colour.Red;
                                rend.material = otherBabushkas[1];
                        }
                            break;
                        }
                    case 2:
                        {
                            if (m_PlayerLives.player3Lives > 0)
                            {
                                valid = true;
                                newColour = Colours.Colour.Green;
                                rend.material = otherBabushkas[2];
                        }    
                            break;
                        }
                    case 3:
                        {
                            if (m_PlayerLives.player4Lives > 0)
                            {
                                valid = true;
                                newColour = Colours.Colour.Yellow;
                                rend.material = otherBabushkas[3];
                            }
                            break;
                        }
                }
        }
        
         enemy.GetComponent<EnemyBehaviour>().SetColour((Colours.Colour)newColour);
        
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject,2);
        }
        
    }

}