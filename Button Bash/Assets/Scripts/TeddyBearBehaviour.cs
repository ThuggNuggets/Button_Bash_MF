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
    private Rigidbody rb;
    //flinging
    int flingRotation;

    private void Awake()
    {
        //get random fling values
       float minXFling = gameObject.GetComponent<EnemyBehaviour>().m_Speed;
        xFling = Random.Range(-xFling, -minXFling);
        zFling = Random.Range(-zFling, zFling);
        rb = gameObject.GetComponent<Rigidbody>();
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
                else
                {
                rb.constraints = RigidbodyConstraints.None;
                }
                //soud trest alex
                {
                GetComponent<AudioSource>().Play();
                }
        }
    }

}