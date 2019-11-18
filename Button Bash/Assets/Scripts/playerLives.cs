using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


//This code is used to write to 4 different texts on a UI and hold the remaining lives each player has
// player 1 - blue
// player 2 - red
// player 3 - green
// player 4 - yellow

/*Place this code on the fence that the enemies collide with to deal damage*/
public class playerLives : MonoBehaviour
{
    //starting lives for the players
    public int m_MaxHealth = 5;
    [HideInInspector]
    public int m_Player1Lives;
    [HideInInspector]
    public int m_Player2Lives;
    [HideInInspector]
    public int m_Player3Lives;
    [HideInInspector]
    public int m_Player4Lives;
    //holds colour variable
    Colours.Colour m_EnemyColour = Colours.Colour.None;

	public GameObject[] m_HealthBalloons;

	public GameObject[] m_DefeatedPlayerReticals;

	// How many players have lost all their lives.
	private int m_PlayerDeathIterator = 0;

	public GameObject m_EnemyHitParticleEffect;

    private void Awake()
    {
        m_Player1Lives = m_MaxHealth;
        m_Player2Lives = m_MaxHealth;
        m_Player3Lives = m_MaxHealth;
        m_Player4Lives = m_MaxHealth;
		m_PlayerDeathIterator = 0;
    }
    private void OnCollisionEnter(Collision collision)
    {
        // If the enemy reaches the end trigger deal damage to specific player
        if(collision.gameObject.tag == "letterBlock" || collision.gameObject.tag == "teddyBear" || collision.gameObject.tag == "rubix" || collision.gameObject.tag == "babushkaSmall" || collision.gameObject.tag == "babushkaMedium" || collision.gameObject.tag == "babushkaLarge" )
        {
			m_EnemyColour = collision.gameObject.GetComponent<EnemyBehaviour>().GetColour();

			// alex sound
			//GetComponent<AudioSource>().clip = m_SoundManager.m_SoundClips[3];
			//GetComponent<AudioSource>().Play();
        }
        else
        {
            m_EnemyColour = Colours.Colour.None;
        }

        switch (m_EnemyColour)
        {
            // Set the material colour to red.
            case Colours.Colour.Blue:
                {
                    if (m_Player1Lives > 0)
                    {
                        m_Player1Lives -= 1;
						// If player 1's lives are 0, increase the amount of players that have no lives.
						if (m_Player1Lives == 0)
						{
							m_PlayerDeathIterator++;
							GameObject m_Sailor = GameObject.Find("Character_sailor_001");
							Instantiate(m_DefeatedPlayerReticals[0], m_Sailor.transform.GetChild(0).position, new Quaternion());
							GameManager.AddDefeatedCharacter(0);
							Destroy(m_Sailor);
						}

						m_HealthBalloons[0].GetComponent<BalloonHealthUI>().TakeDamage();
					}
                    break;
                }

            // Set the material colour to blue.
            case Colours.Colour.Red:
                {
                    if (m_Player2Lives > 0)
                    {
                        m_Player2Lives -= 1;
                        // If player 2's lives are 0, increase the amount of players that have no lives.
                        if (m_Player2Lives == 0)
                        {
                            m_PlayerDeathIterator++;
                            GameObject m_Magic = GameObject.Find("Character_magic_001");
                            Instantiate(m_DefeatedPlayerReticals[1], m_Magic.transform.GetChild(0).position, new Quaternion());
							GameManager.AddDefeatedCharacter(1);
							Destroy(m_Magic);
                        }
                        m_HealthBalloons[1].GetComponent<BalloonHealthUI>().TakeDamage();
					}
                    break;
                }
            // Set the material colour to green.
            case Colours.Colour.Green:
                {
                    if (m_Player3Lives > 0)
                    {
                        m_Player3Lives -= 1;
                        // If player 3's lives are 0, increase the amount of players that have no lives.
                        if (m_Player3Lives == 0)
                        {
                            m_PlayerDeathIterator++;
                            GameObject m_Alien = GameObject.Find("Character_Alien_001");
                            Instantiate(m_DefeatedPlayerReticals[2], m_Alien.transform.GetChild(0).position, new Quaternion());
							GameManager.AddDefeatedCharacter(2);
							Destroy(m_Alien);
                        }
                        m_HealthBalloons[2].GetComponent<BalloonHealthUI>().TakeDamage();
					}
                    break;
                }
            // Set the material colour to yellow.
            case Colours.Colour.Yellow:
                {
                    if (m_Player4Lives > 0)
                    {
                        m_Player4Lives -= 1;
                        // If player 4's lives are 0, increase the amount of players that have no lives.
                        if (m_Player4Lives == 0)
                        {
                            m_PlayerDeathIterator++;
                            GameObject m_Cat = GameObject.Find("Character_Cat_001");
                            Instantiate(m_DefeatedPlayerReticals[3], m_Cat.transform.GetChild(0).position, new Quaternion());
							GameManager.AddDefeatedCharacter(3);
							Destroy(m_Cat);
                        }
                        m_HealthBalloons[3].GetComponent<BalloonHealthUI>().TakeDamage();
					}
                    break;
                }
        }

		// If 3 players have run out of lives, one player stands, check which one is alive and move on to the end screen.
		if (m_PlayerDeathIterator == 3)
		{
			if (m_Player1Lives > 0)
				GameManager.AddDefeatedCharacter(0);
			else if (m_Player2Lives > 0)
				GameManager.AddDefeatedCharacter(1);
			else if (m_Player3Lives > 0)
				GameManager.AddDefeatedCharacter(2);
			else if (m_Player4Lives > 0)
				GameManager.AddDefeatedCharacter(3);

			// Load the end scene.
			SceneManager.LoadScene(3);
		}
		Instantiate(m_EnemyHitParticleEffect, collision.transform.position, new Quaternion());
    }
 }
