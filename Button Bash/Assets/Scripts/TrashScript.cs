using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashScript : MonoBehaviour
{
    // The rigidbody.
    private Rigidbody m_Rigidbody;
    // Speed of the enemy.
    public float m_Speed = 2;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("up"))
        {
            // Move fowards at it's speed.
            m_Rigidbody.MovePosition(transform.position + transform.right * m_Speed * Time.deltaTime);
        }
    }
}
