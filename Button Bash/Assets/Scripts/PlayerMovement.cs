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
    // gameState now exists and calls on existing GameManager script to get the current game state
    GameManager.GameStates gameState = GameManager.GetInstance().GetGameState();

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
        // time when movement started
        startTime = Time.time;
        // calculate distance
        distance = Vector3.Distance(railOne.position, railTwo.position);

    }

    void FixedUpdate()
    {
        
    }

    void Update()
    {
        switch (gameState)
        {
            // execute on Playing state
           case GameManager.GameStates.Playing:
                Vector3 movement = Vector3.zero;
                // distance moved = elapsed time * speed
                float distanceCovered = (Time.time - startTime) * railSwitchSpeed;
                // fraction of distance completed equals current distance divided by total distance
                float fractionOfDistance = distanceCovered / distance;

                // set position as a fraction of the distance between the rails
                transform.position = Vector3.Lerp(railOne.position, railTwo.position, fractionOfDistance);

                // horizontal movement along a rail
                movement += transform.right * Input.GetAxis("Horizontal") * railMoveSpeed;

                break;
        }
    }
}
