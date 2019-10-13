using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//This code is used to write to 4 different texts on a UI and hold the remaining lives each player has
//REQUIRES 4 Text inputs one for each player
// player 1 - blue
// player 2 - red
// player 3 - green
// player 4 - yellow

/*Place this code on the fence that the enemies collide with to deal damage*/
public class playerLives : MonoBehaviour
{
    //starting lives for the players
    public int maxHealth = 5;

    private int player1Lives;
    private int player2Lives;
    private int player3Lives;
    private int player4Lives;
    //stores the text that displayes lives
    public Text player1;
    public Text player2;
    public Text player3;
    public Text player4;
    //using for reference on how much damage has been taken

    private void Awake()
    {
        player1Lives = maxHealth;
        player2Lives = maxHealth;
        player3Lives = maxHealth;
        player4Lives = maxHealth;
    }
    private void OnCollisionEnter(Collision collision)
    {
        // If the enemy reaches the end trigger deal damage to specific player
        Colours.Colour enemyColour = collision.gameObject.GetComponent<EnemyBehaviour>().GetColour();

        switch (enemyColour)
        {
            // Set the material colour to red.
            case Colours.Colour.Blue:
                {
                    if (player1Lives > 0)
                    {
                        player1Lives -= 1;
                        player1.text =  player1Lives.ToString();
                    }
                    break;
                }

            // Set the material colour to blue.
            case Colours.Colour.Red:
                {
                    if (player2Lives > 0)
                    {
                        player2Lives -= 1;
                        player2.text = player2Lives.ToString();
                    }
                    break;
                }
            // Set the material colour to green.
            case Colours.Colour.Green:
                {
                    if (player3Lives > 0)
                    {
                        player3Lives -= 1;
                        player3.text = player3Lives.ToString();
                    }
                    break;
                }
            // Set the material colour to yellow.
            case Colours.Colour.Yellow:
                {
                    if (player4Lives > 0)
                    {
                        player4Lives -= 1;
                        player4.text = player4Lives.ToString();
                    }
                    break;
                }
            // Set the material colour to magenta, something went WRONG!
            default:
                {
                    break;
                }
        }


    }
 }
