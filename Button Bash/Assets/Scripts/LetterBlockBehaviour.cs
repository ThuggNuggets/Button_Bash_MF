using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//removes self and bullet on hit
/*put this code on the Letter Block enemy*/
public class LetterBlockBehaviour : MonoBehaviour
{
    public int health = 2;
    //flinging the enemy when they have no health
    public float verticalFling = 50;
    public float xFling = 10;
    public float zFling = 10;

    //flinging
    int flingRotation;
    /// <summary>
    /// sets variables 
    /// </summary>
    private void Awake()
    {
        //get random fling values
        float minXFling = gameObject.GetComponent<EnemyBehaviour>().m_Speed;
        xFling = Random.Range(-xFling, -minXFling);
        zFling = Random.Range(-zFling, zFling);
        flingRotation = Random.Range(0, 3);
    }
    /// <summary>
    /// fling when defeated
    /// </summary>
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
    /// <summary>
    /// when it collides with an object if it has the "bullet" tag reduce health and if no health destroy self
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            health--;
            Destroy(collision.gameObject);
            //soud trest alex
            {

                SoundManager sm = GameObject.Find("Sound bucket ").GetComponent<SoundManager>();
                AudioSource ac = GetComponent<AudioSource>();
                ac.clip = sm.m_SoundClips[1];
                ac.pitch = Random.Range(0, 3);
                ac.Play();
            }
        }
    }
}
