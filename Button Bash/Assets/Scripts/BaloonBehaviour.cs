using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//if the object is hit with somthing tagged with "bullet" will find all objects with the tags: "babushkaLarge", "babushkaMedium","babushkaSmall","teddyBear", "rubix" or "letterBlock"
/*put this code on the Balloon enemy*/
public class BaloonBehaviour : MonoBehaviour
{
   
    //destroy self after this many seconds on collision (allow expanding)
    public float m_TimerToDestroy;

    //speed increase (multiplacative)
    public float m_speedIncrease = 2.0f;
    //expanding when hit
    private float timer = 0.0f;
    //is the ballooon expanding (popping)
    private bool expand = false;

    private void OnCollisionEnter(Collision collision)
    {
        // If the bullet collides with an enemy and the enemy shares a colour with the bullet, destroy the bullet.
        if (collision.gameObject.tag == "bullet")
        {
            //destroy bullet
            Destroy(collision.gameObject);
            //creates an array of enemies
            GameObject[] enemies;
           //creates a list of enemies
            List<GameObject> m_enemiesToSpeedUp = new List<GameObject>();
        //puts all enemies into the array then puts the contents of the array into the list
            enemies = GameObject.FindGameObjectsWithTag("babushkaLarge");
            m_enemiesToSpeedUp.AddRange(enemies);

            enemies = GameObject.FindGameObjectsWithTag("babushkaMedium");
            m_enemiesToSpeedUp.AddRange(enemies);

            enemies = GameObject.FindGameObjectsWithTag("babushkaSmall");
            m_enemiesToSpeedUp.AddRange(enemies);

            enemies = GameObject.FindGameObjectsWithTag("letterBlock");
            m_enemiesToSpeedUp.AddRange(enemies);

            enemies = GameObject.FindGameObjectsWithTag("teddyBear");
            m_enemiesToSpeedUp.AddRange(enemies);

            enemies = GameObject.FindGameObjectsWithTag("rubix");
            m_enemiesToSpeedUp.AddRange(enemies);
            //speeds up enemies in the list
            if (expand == false)
            {
                foreach (GameObject enemy in m_enemiesToSpeedUp)
                {
                    enemy.GetComponent<EnemyBehaviour>().m_Speed *= m_speedIncrease;
                }
            }
            expand = true;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (expand)
        {
            //increases size of self (popping)
            timer += Time.deltaTime;
            transform.localScale += new Vector3(0.05F, 0.05F, 0.05F);
            //kill self
            if (timer > m_TimerToDestroy)
            {
                Destroy(gameObject);
            }
        }
       
    }
}

