using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // speed of moving along a rail
    public float railMoveSpeed;
    // speed of switching between rails
    public float railSwitchSpeed;
    // animator now exists
    private Animator animator;
    // rigidBody now exists
    private Rigidbody rigidBody;
    // position of the first rail
    public Transform railOne;
    // position of the second rail
    public Transform railTwo;

    // when movement started
    private float startTime;
    // distance between rails
    private float distance;

    void Awake()
    {
        // rigidBody now has properties of RigidBody
        rigidBody = GetComponent<Rigidbody>();
        // animator now has properties of Animator
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        
    }

    void Update()
    {
        //switch (GameManager.GetGameState())
        {
            // execute on Playing state
           // case GameManager.GameStates.Playing:
                Vector3 movement = Vector3.zero;

                movement += transform.right * Input.GetAxis("Horizontal") * railMoveSpeed;
                //movement += transform.forward * Input.GetAxis("Vertical") * railSwitchSpeed;
        }
    }
}
