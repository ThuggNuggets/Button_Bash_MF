using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using XInputDotNetPure;


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

    //are players flashing
    bool m_Player1Flash, m_Player2Flash, m_Player3Flash, m_Player4Flash = false;
    //how long the flash is
    private int m_P1FlashTimer, m_P2FlashTimer, m_P3FlashTimer, m_P4FlashTimer = 0;
    //origional materials
    Material m_OriginalP1Mat, m_OriginalP2Mat, m_OriginalP3Mat, m_OriginalP4Mat;
    //origional hat materials
    Material m_P1HatMat, m_P4HatMat, m_P3HatMat, m_P2HatMat;

    //renderer of the players
    Renderer m_Player1Rend, m_Player2Rend, m_Player3Rend, m_Player4Rend;
    // renderer of the players hats
    Renderer m_P1HatRend, m_P2HatRend,m_P3HatRend,m_P4HatRend;
    //player array
    public GameObject[] m_players;
    //player hat array
    public GameObject[] m_Hats;
    //flash material
    public Material m_WhiteMaterial;

	public GameObject m_EnemyHitParticleEffect;

    //make flashing occur multiple times
    public int m_MaxFlashTimer = 10;
    public int m_flashAmount = 3;
    public int m_MaxFlashDelay = 5;

    // how long to stay unflashed
    private int m_P1FlashDelay, m_P2FlashDelay, m_P3FlashDelay, m_P4FlashDelay;
    //how many times each player has flashed
   private int m_P1FlashIterator, m_P2FlashIterator, m_P3FlashIterator, m_P4FlashIterator;
    //have the players been hit
    private bool m_P1Hit, m_P2Hit, m_P3Hit, m_P4Hit = false;
    // How many players have lost all their lives.
    private int m_PlayerDeathIterator = 0;

    private void Awake()
    {
        m_Player1Lives = m_MaxHealth;
        m_Player2Lives = m_MaxHealth;
        m_Player3Lives = m_MaxHealth;
        m_Player4Lives = m_MaxHealth;

        m_Player1Rend = m_players[0].GetComponent<Renderer>();
        m_Player2Rend = m_players[1].GetComponent<Renderer>();
        m_Player3Rend = m_players[2].GetComponent<Renderer>();
        m_Player4Rend = m_players[3].GetComponent<Renderer>();

        m_P1HatRend = m_Hats[0].GetComponent<Renderer>();
        m_P2HatRend = m_Hats[1].GetComponent<Renderer>();
        m_P3HatRend = m_Hats[2].GetComponent<Renderer>();
        m_P4HatRend = m_Hats[3].GetComponent<Renderer>();

        m_OriginalP1Mat = m_Player1Rend.material;
        m_OriginalP2Mat = m_Player2Rend.material;
        m_OriginalP3Mat = m_Player3Rend.material;
        m_OriginalP4Mat = m_Player4Rend.material;

        m_P1HatMat = m_P1HatRend.material;
        m_P2HatMat = m_P2HatRend.material;
        m_P3HatMat = m_P3HatRend.material;
        m_P4HatMat = m_P4HatRend.material;

        m_P1FlashDelay = m_MaxFlashDelay;
        m_P2FlashDelay = m_MaxFlashDelay;
        m_P3FlashDelay = m_MaxFlashDelay;
        m_P4FlashDelay = m_MaxFlashDelay;
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
                        m_P1Hit = true;
                        m_Player1Flash = true;
                        m_P1FlashTimer = m_MaxFlashTimer;
                        m_Player1Lives -= 1;
						GamePad.SetVibration(PlayerIndex.One, 2.0f, 2.0f);
						// If player 1's lives are 0, increase the amount of players that have no lives.
						if (m_Player1Lives == 0)
						{
							m_PlayerDeathIterator++;
							GameObject m_Sailor = GameObject.Find("Character_sailor_001");
							Instantiate(m_DefeatedPlayerReticals[0], m_Sailor.transform.GetChild(0).position, new Quaternion());
							GameManager.AddDefeatedCharacter(0);
							m_Sailor.SetActive(false);
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
                        m_P2Hit = true;
                        m_Player2Flash = true;
                        m_P2FlashTimer = m_MaxFlashTimer;
						GamePad.SetVibration(PlayerIndex.Two, 2.0f, 2.0f);
						// If player 2's lives are 0, increase the amount of players that have no lives.
						if (m_Player2Lives == 0)
                        {
                            m_PlayerDeathIterator++;
                            GameObject m_Magic = GameObject.Find("Character_magic_001");
                            Instantiate(m_DefeatedPlayerReticals[1], m_Magic.transform.GetChild(0).position, new Quaternion());
							GameManager.AddDefeatedCharacter(1);
							m_Magic.SetActive(false);
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
                        m_Player3Flash = true;
                        m_P3Hit = true;
                        m_P3FlashTimer = m_MaxFlashTimer;
						GamePad.SetVibration(PlayerIndex.Three, 2.0f, 2.0f);
						// If player 3's lives are 0, increase the amount of players that have no lives.
						if (m_Player3Lives == 0)
                        {
                            m_PlayerDeathIterator++;
                            GameObject m_Alien = GameObject.Find("Character_Alien_001");
                            Instantiate(m_DefeatedPlayerReticals[2], m_Alien.transform.GetChild(0).position, new Quaternion());
							GameManager.AddDefeatedCharacter(2);
							m_Alien.SetActive(false);
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
                        m_Player4Flash = true;
                        m_P4Hit = true;
                        m_P4FlashTimer = m_MaxFlashTimer;
						GamePad.SetVibration(PlayerIndex.Four, 2.0f, 2.0f);
						// If player 4's lives are 0, increase the amount of players that have no lives.
						if (m_Player4Lives == 0)
                        {
                            m_PlayerDeathIterator++;
                            GameObject m_Cat = GameObject.Find("Character_Cat_001");
                            Instantiate(m_DefeatedPlayerReticals[3], m_Cat.transform.GetChild(0).position, new Quaternion());
							GameManager.AddDefeatedCharacter(3);
							m_Cat.SetActive(false);
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
			SceneManager.LoadScene(4);
		}
		Instantiate(m_EnemyHitParticleEffect, collision.transform.position, new Quaternion());
    }

    private void Update()
    {
        //player 1 flash
        if (m_P1Hit)
        {
            if (m_Player1Flash)
            {
                m_Player1Rend.material = m_WhiteMaterial;
                m_P1HatRend.material = m_WhiteMaterial;
                m_P1FlashTimer--;
                if (m_P1FlashTimer < 0)
                {
                    m_Player1Flash = false;
                    m_P1FlashDelay = m_MaxFlashDelay;
                }
            }
            else
            {
                m_Player1Rend.material = m_OriginalP1Mat;
                m_P1HatRend.material = m_P1HatMat;
                m_P1FlashDelay--;
                if (m_P1FlashDelay < 0)
                {
                    m_P1FlashIterator++;
                    m_Player1Flash = true;
                    m_P1FlashTimer = m_MaxFlashTimer;
                }
                if(m_P1FlashIterator >= m_flashAmount)
                {
                    m_P1FlashIterator = 0;
                    m_Player1Flash = false;
                    m_P1Hit = false;
					GamePad.SetVibration(PlayerIndex.One, 0.0f, 0.0f);
                }
            }
        }

        //player 2 flash
        if (m_P2Hit)
        {
            if (m_Player2Flash)
            {
                m_Player2Rend.material = m_WhiteMaterial;
                m_P2HatRend.material = m_WhiteMaterial;
                m_P2FlashTimer--;
                if (m_P2FlashTimer < 0)
                {
                    m_Player2Flash = false;
                    m_P2FlashDelay = m_MaxFlashDelay;
                }
            }
            else
            {
                m_Player2Rend.material = m_OriginalP2Mat;
                m_P2HatRend.material = m_P2HatMat;
                m_P2FlashDelay--;
                if (m_P2FlashDelay < 0)
                {
                    m_P2FlashIterator++;
                    m_Player2Flash = true;
                    m_P2FlashTimer = m_MaxFlashTimer;
                }
                if (m_P2FlashIterator >= m_flashAmount)
                {
                    m_P2FlashIterator = 0;
                    m_Player2Flash = false;
                    m_P2Hit = false;
					GamePad.SetVibration(PlayerIndex.Two, 0.0f, 0.0f);
				}
            }
        }

        //player 3 flash
        if (m_P3Hit)
        {
            if (m_Player3Flash)
            {
                m_Player3Rend.material = m_WhiteMaterial;
                m_P3HatRend.material = m_WhiteMaterial;
                m_P3FlashTimer--;
                if (m_P3FlashTimer < 0)
                {
                    m_Player3Flash = false;
                    m_P3FlashDelay = m_MaxFlashDelay;
                }
            }
            else
            {
                m_Player3Rend.material = m_OriginalP3Mat;
                m_P3HatRend.material = m_P3HatMat;
                m_P3FlashDelay--;
                if (m_P3FlashDelay < 0)
                {
                    m_P3FlashIterator++;
                    m_Player3Flash = true;
                    m_P3FlashTimer = m_MaxFlashTimer;
                }
                if (m_P3FlashIterator >= m_flashAmount)
                {
                    m_P3FlashIterator = 0;
                    m_Player3Flash = false;
                    m_P3Hit = false;
					GamePad.SetVibration(PlayerIndex.Three, 0.0f, 0.0f);
				}
            }
        }

        //Player 4 flash
        if (m_P4Hit)
        {
            if (m_Player4Flash)
            {
                m_Player4Rend.material = m_WhiteMaterial;
                m_P4HatRend.material = m_WhiteMaterial;
                m_P4FlashTimer--;
                if (m_P4FlashTimer < 0)
                {
                    m_Player4Flash = false;
                    m_P4FlashDelay = m_MaxFlashDelay;
                }
            }
            else
            {
                m_Player4Rend.material = m_OriginalP4Mat;
                m_P4HatRend.material = m_P4HatMat;
                m_P4FlashDelay--;
                if (m_P4FlashDelay < 0)
                {
                    m_P4FlashIterator++;
                    m_Player4Flash = true;
                    m_P4FlashTimer = m_MaxFlashTimer;
                }
                if (m_P4FlashIterator >= m_flashAmount)
                {
                    m_P4FlashIterator = 0;
                    m_Player4Flash = false;
                    m_P4Hit = false;
					GamePad.SetVibration(PlayerIndex.Four, 0.0f, 0.0f);
				}
            }
        }
    }

}
