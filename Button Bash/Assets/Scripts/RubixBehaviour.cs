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

    // The script that is storing all the player's lives
    private playerLives m_PlayerLives;
    // The collider that holds the player lives.
    private GameObject m_PlayerLivesCollider;
    //used to find the new colour for the next babushka level
    private Colours.Colour newColour;
    private bool valid = false;

    //flinging
    int flingRotation;
    private void Awake()
    {
        m_colour = gameObject.GetComponent<EnemyBehaviour>().GetColour();
        //get random fling values
        float minXFling = gameObject.GetComponent<EnemyBehaviour>().m_Speed;
        xFling = Random.Range(-xFling, -minXFling);
        zFling = Random.Range(-zFling, zFling);

        //finds the object that hold the player lives 
        m_PlayerLivesCollider = GameObject.Find("Collider");
        m_PlayerLives = m_PlayerLivesCollider.GetComponentInChildren<playerLives>();

        flingRotation = Random.Range(0, 3);
    }
    private void FixedUpdate()
    {
        if (health <= 0)
        {
           
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
                        transform.Rotate(0, 10, 0);
                        break;
                    }
                case 2:
                    {
                        transform.Rotate(0, 0, 10);
                        break;
                    }
            }
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
            if (health > 0)
            {
               
                valid = false;

                //changes its colour
                while (!valid)
                {
                    switch (m_colour)
                    {
                        case Colours.Colour.Blue:
                            {
                                m_colour = Colours.Colour.Red;
                                transform.Rotate(0, 90, 0, Space.Self);
                                if (m_PlayerLives.player2Lives > 0)
                                {
                                    valid = true;
                                }
                                break;
                            }
                        case Colours.Colour.Red:
                            {
                                m_colour = Colours.Colour.Green;
                                transform.Rotate(0, 90, 0, Space.Self);
                                if (m_PlayerLives.player3Lives > 0)
                                {
                                    valid = true;
                                }
                                break;
                            }
                        case Colours.Colour.Green:
                            {
                                m_colour = Colours.Colour.Yellow;
                                transform.Rotate(0, 90, 0, Space.Self);
                                if (m_PlayerLives.player4Lives > 0)
                                {
                                    valid = true;
                                }

                                break;
                            }
                        case Colours.Colour.Yellow:
                            {
                                m_colour = Colours.Colour.Blue;
                                transform.Rotate(0, 90, 0, Space.Self);
                                if (m_PlayerLives.player1Lives > 0)
                                {
                                    valid = true;
                                }

                                break;
                            }
                    }
                }
                
            }
            gameObject.GetComponent<EnemyBehaviour>().SetColour(m_colour);
            //when hit rotate body
            SoundManager sm = GameObject.Find("Sound bucket ").GetComponent<SoundManager>();
            AudioSource ac = GetComponent<AudioSource>();
            ac.clip = sm.m_SoundClips[2];
            ac.pitch = Random.Range(0, 3);
            ac.Play();
        }
    }
}
