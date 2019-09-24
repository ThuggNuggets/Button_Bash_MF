using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterBlockBehaviour : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
