using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Code for the Rubix cube enemy which allows the changing colour when hit with something with the tag "bullet"
/*put this code on the Rubix enemy*/
public class RubixBehaviour : MonoBehaviour
{
    public GameObject m_DeathPA;
    public float m_Health = 3.0f;
    private Colours.Colour m_Colour;
    //flinging the enemy when they have no health
    public float m_VerticalFling = 25;
    public float m_BackForce = 20;
    public float m_fallTimer = 5;

    // The script that is storing all the player's lives
    private playerLives m_PlayerLives;
    // The collider that holds the player lives.
    private GameObject m_PlayerLivesCollider;

    private bool m_Valid = false;

    //flinging
    int m_FlingRotation;
    //speed of rotation after being hit by a button
    public float m_RubixSpinSpeed = 1;
    private float m_RubixSpinSpeedAmount = 0;
    bool m_Rotating;
    bool m_Spin360;
    Quaternion m_TargetRotation;
    Quaternion m_StartRotation;
    private void Awake()
    {
        m_Spin360 = false;
        m_Colour = gameObject.GetComponent<EnemyBehaviour>().GetColour();

        //finds the object that hold the player lives 
        m_PlayerLivesCollider = GameObject.Find("Collider");
        m_PlayerLives = m_PlayerLivesCollider.GetComponentInChildren<playerLives>();

        m_FlingRotation = Random.Range(0, 3);
    }
    private void Update()
    {
        if (m_Health <= 0)
        {
            //flinging
            m_fallTimer -= Time.deltaTime;
            if (m_fallTimer > 2.5f)
            {
                transform.Translate(new Vector3(-m_BackForce, m_VerticalFling, 0) * Time.deltaTime, Space.World);
            }
            else if(m_fallTimer > 1)
            {
                m_FlingRotation = 3;
                transform.Translate(new Vector3(-m_BackForce, -1, 0) * Time.deltaTime, Space.World);
            }
            else
            {
                transform.Translate(new Vector3(0, 0, 0) * Time.deltaTime, Space.World);
            }
            //destroy self and bullet on collision
            Destroy(gameObject, 10);
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
                case 3:
                    {
                        transform.Rotate(0, 0, 0);
                        break;
                    }
            }
        }

        if(m_Rotating)
        {
            if (m_Spin360)
            {
                m_RubixSpinSpeedAmount += Time.deltaTime * m_RubixSpinSpeed * 2;
                transform.rotation = Quaternion.Slerp(m_StartRotation, m_TargetRotation, m_RubixSpinSpeedAmount);
            }
            else
            {
                m_RubixSpinSpeedAmount += Time.deltaTime * m_RubixSpinSpeed;
                transform.rotation = Quaternion.Slerp(m_StartRotation, m_TargetRotation, m_RubixSpinSpeedAmount);
            }
            if(m_RubixSpinSpeedAmount >= 1)
            {
                if (m_Spin360)
                {
                   m_TargetRotation *= Quaternion.Euler(0, 180.0f, 0);
                    m_RubixSpinSpeedAmount = 0;
                    m_Spin360 = false;
                }
                else
                {
                    m_Rotating = false;
                    m_RubixSpinSpeedAmount = 0;
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
                    ///-------------------------------------------------------------------------------------------
                    switch (Random.Range(0, 4))
                    {
                        case 0:
                            {
                                if (m_PlayerLives.m_Player2Lives > 0)
                                {
                                    m_Valid = true;
                                    m_Rotating = true;
                                    m_TargetRotation = Quaternion.Euler(0, 0, 0);
                                    if(m_Colour  == Colours.Colour.Red)
                                    {
                                        m_TargetRotation = Quaternion.Euler(0, 180.0f, 0);
                                        m_Spin360 = true;
                                    }
                                    m_Colour = Colours.Colour.Red;
                                    m_StartRotation = transform.rotation;
                                    
                                }
                                break;
                            }
                        case 1:
                            {
                                if (m_PlayerLives.m_Player3Lives > 0)
                                {
                                    m_Valid = true;
                                    m_Rotating = true;
                                    m_TargetRotation = Quaternion.Euler(0, 90, 0);
                                    if (m_Colour == Colours.Colour.Green)
                                    {
                                        m_TargetRotation = Quaternion.Euler(0, -90, 0);
                                        m_Spin360 = true;
                                    }
                                    m_Colour = Colours.Colour.Green;
                                    m_StartRotation = transform.rotation;
                                }
                                break;
                            }
                        case 2:
                        {
                            if (m_PlayerLives.m_Player4Lives > 0)
                            {
                                    m_Valid = true;
                                    m_Rotating = true;
                                    m_TargetRotation = Quaternion.Euler(0, 180, 0);
                                    if (m_Colour == Colours.Colour.Yellow)
                                    {
                                        m_TargetRotation = Quaternion.Euler(0, 0, 0);
                                        m_Spin360 = true;
                                    }
                                    m_Colour = Colours.Colour.Yellow;
                                    m_StartRotation = transform.rotation;
                            }
                            break;
                        }
                        case 3:
                            {
                                if (m_PlayerLives.m_Player1Lives > 0)
                                {
                                    m_Valid = true;
                                    m_Rotating = true;
                                    m_TargetRotation = Quaternion.Euler(0, -90, 0);
                                    if (m_Colour == Colours.Colour.Blue)
                                    {
                                        m_TargetRotation = Quaternion.Euler(0, 90, 0);
                                        m_Spin360 = true;
                                    }
                                    m_Colour = Colours.Colour.Blue;
                                    m_StartRotation = transform.rotation;
                                }

                                break;
                            }
                    }
                    ///
                }
                
            }
            gameObject.GetComponent<EnemyBehaviour>().SetColour(m_Colour);
            // play a sound from the sound bucket
            SoundManager sm = GameObject.Find("Sound bucket ").GetComponent<SoundManager>();
            AudioSource ac = GetComponent<AudioSource>();
            ac.clip = sm.m_SoundClips[2];
            ac.pitch = Random.Range(1, 3);
            ac.Play();
            if (m_Health == 0)
            {
                m_fallTimer = 5;
                Instantiate(m_DeathPA, transform.position, transform.rotation);
            }
        }
    }
}
