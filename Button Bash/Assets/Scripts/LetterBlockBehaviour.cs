using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
