using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//increases object size the more it gets hit with objects tagged with "bullet"
/*put this code on the Teddy bear enemy*/
public class TeddyBearBehaviour : MonoBehaviour
{
    //hits until it is defeated
    public float health = 5;
    // amount its size increased each time
    public float scale = 0.5f;
    //flinging the enemy when they have no health
    public float verticalFling = 50;
    public float xFling = 10;
    public float zFling = 10;

    private void Awake()
    {
        //get random fling values
       float minXFling = gameObject.GetComponent<EnemyBehaviour>().m_Speed;
        xFling = Random.Range(-xFling, -minXFling);
        zFling = Random.Range(-zFling, zFling);
    }
    private void FixedUpdate()
    {
        if (health <= 0)
        {
            transform.Translate(new Vector3(xFling, verticalFling, zFling) * Time.deltaTime, Space.World);
            //destroy self and bullet on collision
            Destroy(gameObject, 2);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        // If the bullet collides with an enemy and the enemy shares a colour with the bullet, destroy the bullet.
        if (collision.gameObject.tag == "bullet")
        {
            //destroy the bullet
                Destroy(collision.gameObject);
                health--;
                if (health > 0)
                {
                //increases size of bear
                transform.localScale += new Vector3(scale, scale, scale);
                }
                //soud trest alex
                {
                GetComponent<AudioSource>().Play();
                }
        }
    }

}