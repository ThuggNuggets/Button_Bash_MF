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
    private Rails currentRail = Rails.frontRail;
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
        frontRail,
        backRail
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
        // colourMaterial now has properties of Renderer
        colourMaterial = GetComponent<Renderer>().material;

        maxCoolDown = shootCoolDown;
        shootCoolDown = 0.0f;

        // time when movement started
        //startTime = Time.time;
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

    private void FixedUpdate()
    {
        if(Input.GetAxis(laneChangingAxis) > 0)
        {
            if (currentRail != Rails.frontRail)
            {
                currentRail = Rails.frontRail;

                GameObject newRail = GameObject.Find("Rail");

                targetRail = transform.position;

                targetRail.x = newRail.transform.position.x;

                changingRail = true;
            }
        }

        else if (Input.GetAxis(laneChangingAxis) < 0)
        {
            if (currentRail != Rails.backRail)
            {
                currentRail = Rails.backRail;
                GameObject newRail = GameObject.Find("Rail (1)");

                targetRail = transform.position;

                targetRail.x = newRail.transform.position.x;

                changingRail = true;
            }
        }

        float translation = Input.GetAxis(movementAxis) * railMoveSpeed;
        translation *= Time.deltaTime;
        transform.Translate(0, 0, translation);

        if(railSwitchSpeed > 1)
        {
            railSwitchSpeed = 1;
        }
        if (changingRail)
        {
            if (railSwitchSpeed < 1)
            {
                railSwitchSpeed += Time.deltaTime * railMoveSpeed;
                targetRail.z = transform.position.z;
                targetRail.y = transform.position.y;
                transform.position = Vector3.Lerp(transform.position, targetRail, railSwitchSpeed);
            }
            else
            {
                railSwitchSpeed = 0.0f;
                changingRail = false;
            }
        }

        if (currentRail == Rails.frontRail)
        {
            if (Input.GetAxis)
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

        if(rail == Rails.backRail)
        {
            targetRail.z = backRail.transform.position.z;
        }

        else if (rail == Rails.frontRail)
        {
            targetRail.z = frontRail.transform.position.z;
        }

        changingRail = true;

    }
}
