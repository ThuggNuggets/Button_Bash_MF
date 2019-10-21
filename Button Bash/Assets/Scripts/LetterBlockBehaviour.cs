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
    private Rigidbody rb;

    private void Awake()
    {
        //get random fling values
        xFling = Random.Range(-xFling, xFling);
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
        if (collision.gameObject.tag == "bullet")
        {
            health--;
            Destroy(collision.gameObject);
        }
    }
}
