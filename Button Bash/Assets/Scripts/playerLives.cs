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
    public int maxHealth = 5;

    public int player1Lives;
    public int player2Lives;
    public int player3Lives;
    public int player4Lives;
    //holds colour variable
    Colours.Colour enemyColour = Colours.Colour.None;

	public GameObject[] m_HealthBalloons;

	public GameObject[] m_DefeatedPlayerReticals;

	// How many players have lost all their lives.
	private int m_PlayerDeathIterator = 0;

	private SoundManager m_SoundManager;

    private void Awake()
    {
        player1Lives = maxHealth;
        player2Lives = maxHealth;
        player3Lives = maxHealth;
        player4Lives = maxHealth;

		m_SoundManager = GameObject.Find("Sound bucket ").GetComponent<SoundManager>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        // If the enemy reaches the end trigger deal damage to specific player
        if(collision.gameObject.tag == "letterBlock" || collision.gameObject.tag == "teddyBear" || collision.gameObject.tag == "rubix" || collision.gameObject.tag == "babushkaSmall" || collision.gameObject.tag == "babushkaMedium" || collision.gameObject.tag == "babushkaLarge" )
        {
			enemyColour = collision.gameObject.GetComponent<EnemyBehaviour>().GetColour();

			// alex sound
			GetComponent<AudioSource>().clip = m_SoundManager.m_SoundClips[3];
			GetComponent<AudioSource>().Play();
        }
        else
        {
            enemyColour = Colours.Colour.None;
        }

        switch (enemyColour)
        {
            // Set the material colour to red.
            case Colours.Colour.Blue:
                {
                    if (player1Lives > 0)
                    {
                        player1Lives -= 1;
						// If player 1's lives are 0, increase the amount of players that have no lives.
						if (player1Lives == 0)
						{
							m_PlayerDeathIterator++;
							GameObject m_Sailor = GameObject.Find("Character_sailor_001");
							Instantiate(m_DefeatedPlayerReticals[0], m_Sailor.transform.GetChild(0).position, new Quaternion());
							Destroy(m_Sailor);
						}

						m_HealthBalloons[0].GetComponent<BalloonHealthUI>().TakeDamage();

						GetComponent<AudioSource>().Play();
					}
                    break;
                }

            // Set the material colour to blue.
            case Colours.Colour.Red:
                {
                    if (player2Lives > 0)
                    {
                        player2Lives -= 1;
						// If player 2's lives are 0, increase the amount of players that have no lives.
						if (player2Lives == 0)
							m_PlayerDeathIterator++;

						m_HealthBalloons[1].GetComponent<BalloonHealthUI>().TakeDamage();

						GetComponent<AudioSource>().Play();
					}
                    break;
                }
            // Set the material colour to green.
            case Colours.Colour.Green:
                {
                    if (player3Lives > 0)
                    {
                        player3Lives -= 1;
						// If player 3's lives are 0, increase the amount of players that have no lives.
						if (player3Lives == 0)
							m_PlayerDeathIterator++;

						m_HealthBalloons[2].GetComponent<BalloonHealthUI>().TakeDamage();

						GetComponent<AudioSource>().Play();
					}
                    break;
                }
            // Set the material colour to yellow.
            case Colours.Colour.Yellow:
                {
                    if (player4Lives > 0)
                    {
                        player4Lives -= 1;
						// If player 4's lives are 0, increase the amount of players that have no lives.
						if (player4Lives == 0)
							m_PlayerDeathIterator++;

						m_HealthBalloons[3].GetComponent<BalloonHealthUI>().TakeDamage();

						GetComponent<AudioSource>().Play();
					}
                    break;
                }
            // Set the material colour to magenta, something went WRONG!
            default:
                {
                    break;
                }
        }

        if (player2Lives <= 0)
        {
            GameObject[] noLivesLeft = GameObject.FindGameObjectsWithTag("Player2");
            foreach (GameObject nolife in noLivesLeft)
            {
                Destroy(nolife);
            }
			Instantiate(m_DefeatedPlayerReticals[1], GameObject.Find("Character_magic_001").transform.GetChild(0).transform.position, new Quaternion());
		}
        if (player3Lives <= 0)
        {
            GameObject[] noLivesLeft = GameObject.FindGameObjectsWithTag("Player3");
            foreach (GameObject nolife in noLivesLeft)
            {
                Destroy(nolife);
            }
			Instantiate(m_DefeatedPlayerReticals[2], GameObject.Find("Character_Alien_001").transform.GetChild(0).transform.position, new Quaternion());
		}
        if (player4Lives <= 0)
        {
            GameObject[] noLivesLeft = GameObject.FindGameObjectsWithTag("Player4");
            foreach (GameObject nolife in noLivesLeft)
            {
                Destroy(nolife);
            }
			Instantiate(m_DefeatedPlayerReticals[3], GameObject.Find("Character_Cat_001").transform.GetChild(0).transform.position, new Quaternion());
		}
		// If 3 players have run out of lives, one player stands, check which one is alive and move on to the end screen.
		if (m_PlayerDeathIterator == 3)
		{
			// Which player is alive.
			int alivePlayer = 0;

			// If player 1 has more than 0 lives, player 1 is alive.
			if (player1Lives > 0)
				alivePlayer = 1;

			// If player 2 has more than 0 lives, player 1 is alive.
			else if (player2Lives > 0)
				alivePlayer = 2;

			// If player 3 has more than 0 lives, player 1 is alive.
			else if (player3Lives > 0)
				alivePlayer = 3;

			// If player 4 has more than 0 lives, player 1 is alive.
			else if (player4Lives > 0)
				alivePlayer = 4;

            // Store the final player in the game manager.
            GameManager.SetWinningPlayer(alivePlayer);

			// Load the end scene.
			SceneManager.LoadScene(3);
		}

    }
 }
