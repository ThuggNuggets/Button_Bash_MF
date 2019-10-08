using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // speed of moving along a rail
    public float railMoveSpeed;
    // speed of switching between rails
    public float railSwitchSpeed;
    public string movementAxis;
    public string fireAxis;
    public string laneChangingAxis;

    private Material colourMaterial;
    public GameObject bullet = null;
    public float bulletSpeed = 0.0f;
    public float shootCoolDown = 0.0f;
    private float maxCoolDown = 0.0f;

    // current rail, defaulted to the first rail
    private Rails currentRail = Rails.rail_one;
    private Vector3 targetRail = new Vector3();
    private bool changingRail = false;

    // animator now exists
    private Animator animator;
    // rigidBody now exists
    private Rigidbody rigidBody;
    // position of the front rail
    public GameObject frontRail;
    // position of the back rail
    public GameObject backRail;

    public int playerNumber;
    private Colours.Colour colour;

    // gameState now exists and calls on existing GameManager script to get the current game state
    GameManager.GameStates gameState;

    enum Rails
    {
        rail_one,
        rail_two
    }

    // when movement started
    private float startTime;
    // distance between rails
    private float distance;

    void Awake()
    {
        gameState = GameManager.GetInstance().GetGameState();
        // rigidBody now has properties of RigidBody
        rigidBody = GetComponent<Rigidbody>();
        // animator now has properties of Animator
        animator = GetComponent<Animator>();
        // time when movement started
        startTime = Time.time;
        // calculate distance
        //distance = Vector3.Distance(frontRail.position, backRail.position);

    }

    private void Start()
    {
        switch (playerNumber)
        {
            case 1:
                {
                    colour = Colours.Colour.Blue;
                    colourMaterial.color = Color.blue;
                    break;
                }
            case 2:
                {
                    colour = Colours.Colour.Red;
                    colourMaterial.color = Color.red;
                    break;
                }
            case 3:
                {
                    colour = Colours.Colour.Green;
                    colourMaterial.color = Color.green;
                    break;
                }
            case 4:
                {
                    colour = Colours.Colour.Yellow;
                    colourMaterial.color = Color.yellow;
                    break;
                }
            default:
                {
                    break;
                }
        }
    }

    void Update()
    {
        switch (gameState)
        {
            // execute on Playing state
            case GameManager.GameStates.Playing:
                Vector3 movementStrafe = Vector3.zero;
                Vector3 movementForward = Vector3.zero;
                // distance moved = elapsed time * speed
                float distanceCovered = (Time.time - startTime) * railSwitchSpeed;
                // fraction of distance completed equals current distance divided by total distance
                float fractionOfDistance = distanceCovered / distance;

                // horizontal movement along a rail
                movementStrafe += transform.right * Input.GetAxis("Horizontal") * railMoveSpeed;

                break;
        }
    }

    // on call, change rails
    private void ChangeRail(Rails rail)
    {
        currentRail = rail;
        targetRail = transform.position;

        if(rail == Rails.rail_two)
        {
            targetRail.z = backRail.transform.position.z;
        }

        else if (rail == Rails.rail_one)
        {
            targetRail.z = frontRail.transform.position.z;
        }

        changingRail = true;

    }
}
