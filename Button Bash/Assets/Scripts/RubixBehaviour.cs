using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Code for the Rubix cube enemy which allows the changing colour when hit with something with the tag "bullet"
/*put this code on the Rubix enemy*/
public class RubixBehaviour : MonoBehaviour
{
    public float m_Health = 3.0f;
    private Colours.Colour m_Colour;
    //flinging the enemy when they have no health
    public float m_VerticalFling = 85;
    public float m_XFling = 80;
    public float m_ZFling = 50;

    // The script that is storing all the player's lives
    private playerLives m_PlayerLives;
    // The collider that holds the player lives.
    private GameObject m_PlayerLivesCollider;
    //used to find the new colour for the next babushka level
    private Colours.Colour m_NewColour;
    private bool m_Valid = false;

    //flinging
    int m_FlingRotation;
    private void Awake()
    {
        m_Colour = gameObject.GetComponent<EnemyBehaviour>().GetColour();
        //get random fling values
        float minXFling = gameObject.GetComponent<EnemyBehaviour>().m_Speed;
        m_XFling = Random.Range(-m_XFling, -minXFling);
        m_ZFling = Random.Range(-m_ZFling, m_ZFling);

        //finds the object that hold the player lives 
        m_PlayerLivesCollider = GameObject.Find("Collider");
        m_PlayerLives = m_PlayerLivesCollider.GetComponentInChildren<playerLives>();

        m_FlingRotation = Random.Range(0, 3);
    }
    private void FixedUpdate()
    {
        if (m_Health <= 0)
        {
           //flinging
            transform.Translate(new Vector3(m_XFling, m_VerticalFling, m_ZFling) * Time.deltaTime, Space.World);
            //destroy self and bullet on collision
            Destroy(gameObject, 2);
            //rotates the object
            switch (m_FlingRotation)
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
            m_Health--;
            if (m_Health > 0)
            {
               
                m_Valid = false;

                //changes its colour
                while (!m_Valid)
                {
                    switch (m_Colour)
                    {
                        case Colours.Colour.Blue:
                            {
                                m_Colour = Colours.Colour.Red;
                                transform.Rotate(0, 90, 0, Space.Self);
                                if (m_PlayerLives.m_Player2Lives > 0)
                                {
                                    m_Valid = true;
                                }
                                break;
                            }
                        case Colours.Colour.Red:
                            {
                                m_Colour = Colours.Colour.Green;
                                transform.Rotate(0, 90, 0, Space.Self);
                                if (m_PlayerLives.m_Player3Lives > 0)
                                {
                                    m_Valid = true;
                                }
                                break;
                            }
                        case Colours.Colour.Green:
                            {
                                m_Colour = Colours.Colour.Yellow;
                                transform.Rotate(0, 90, 0, Space.Self);
                                if (m_PlayerLives.m_Player4Lives > 0)
                                {
                                    m_Valid = true;
                                }

                                break;
                            }
                        case Colours.Colour.Yellow:
                            {
                                m_Colour = Colours.Colour.Blue;
                                transform.Rotate(0, 90, 0, Space.Self);
                                if (m_PlayerLives.m_Player1Lives > 0)
                                {
                                    m_Valid = true;
                                }

                                break;
                            }
                    }
                }
                
            }
            gameObject.GetComponent<EnemyBehaviour>().SetColour(m_Colour);
            // play a sound from the sound bucket
            SoundManager sm = GameObject.Find("Sound bucket ").GetComponent<SoundManager>();
            AudioSource ac = GetComponent<AudioSource>();
            ac.clip = sm.m_SoundClips[2];
            ac.pitch = Random.Range(1, 3);
            ac.Play();
        }
    }
}
