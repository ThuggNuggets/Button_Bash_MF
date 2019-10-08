using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//removes self and bullet on hit
/*put this code on the Letter Block enemy*/
public class LetterBlockBehaviour : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            //destroy self and bullet on collision
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
