using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaloonBehaviour : MonoBehaviour
{
   
    //destroy self after this many seconds on collision (allow expanding)
    public float m_TimerToDestroy;

    //speed increase (multiplacative)
    public float m_speedIncrease = 2.0f;
    //expanding when hit
    private float timer = 0.0f;
    private bool expand = false;

    void Awake()
    {
      
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If the bullet collides with an enemy and the enemy shares a colour with the bullet, destroy the bullet.
        if (collision.gameObject.tag == "bullet")
        {
            Destroy(collision.gameObject);
      
            Destroy(gameObject, 10);
            //speeds up enemies based on tags
            GameObject[] enemies;
            List<GameObject> m_enemiesToSpeedUp = new List<GameObject>();
            //GameObject.FindGameObjectsWithTag("babushkaLarge")
            enemies = GameObject.FindGameObjectsWithTag("babushkaLarge");
            m_enemiesToSpeedUp.AddRange(enemies);

            enemies = GameObject.FindGameObjectsWithTag("babushkaMedium");
            m_enemiesToSpeedUp.AddRange(enemies);

            enemies = GameObject.FindGameObjectsWithTag("babushkaSmall");
            m_enemiesToSpeedUp.AddRange(enemies);

            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            m_enemiesToSpeedUp.AddRange(enemies);

            enemies = GameObject.FindGameObjectsWithTag("TeddyBear");
            m_enemiesToSpeedUp.AddRange(enemies);

            enemies = GameObject.FindGameObjectsWithTag("Rubix");
            m_enemiesToSpeedUp.AddRange(enemies);
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
            timer += Time.deltaTime;
            transform.localScale += new Vector3(0.05F, 0.05F, 0.05F);
            if (timer > m_TimerToDestroy)
            {
                Destroy(gameObject);
            }
        }
       
    }
}

