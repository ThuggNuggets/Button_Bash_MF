using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TEMPORARYPLAYERCONTROLS : MonoBehaviour
{
	// Enum of the lanes.
	public enum Lane
	{
		FrontLane,
		BackLane
	}
    public string movementAxis;
    public string fireAxis;
    public string laneChangingAxis;

    // the material the player is
    private Material m_Material;
    // The character's speed.
    public float m_CharacterSpeed = 1.0f;

	// The bullets the player shoots.
	public GameObject m_Bullet = null;

    // The cooldown to shooting.
    public float m_ShootingCooldown = 0.0f;

	// The cooldown, for reseting the cooldown.
	private float m_MaxShootingCooldown = 0.0f;

	// The character's current lane.
	private Lane m_CurrentLane = Lane.FrontLane;

	// The z position of the target lane.
	private Vector3 m_TargetLane = new Vector3();

	// If the character is currently changing lanes.
	private bool m_ChangingLanes = false;
    float laneChangingSpeed = 0.0f;

    // The player's colour.
    public int playerNumber;
	public Colours.Colour m_Colour;

    private float halfWidth = 1.01f;
    GameObject[] PlayerList;

    private float leftHand;
    //how far ahead the button will spawn
    public float buttonSpawnDistance = 0.5f;
    public float buttonSpawnHeight = 0.5f;


    private void Start()
    {
        //switch (playerNumber)
        //{
        //    case 1:
        //        {
        //            m_Colour = Colours.Colour.Blue;
        //            m_Material.color = Color.blue;
        //            break;
        //        }
        //    case 2:
        //        {
        //            m_Colour = Colours.Colour.Red;
        //            m_Material.color = Color.red;
        //            break;
        //        }
        //    case 3:
        //        {
        //            m_Colour = Colours.Colour.Green;
        //            m_Material.color = Color.green;
        //            break;
        //        }
        //    case 4:
        //        {
        //            m_Colour = Colours.Colour.Yellow;
        //            m_Material.color = Color.yellow;
        //            break;
        //        }
        //    default:
        //        {
        //            break;
        //        }
       // }

    }
    // Constructor.
    void Awake()
    {
        m_Material = GetComponent<Renderer>().material;
        m_MaxShootingCooldown = m_ShootingCooldown;
		m_ShootingCooldown = 0.0f;
        PlayerList = GameObject.FindGameObjectsWithTag("Player1");
    }

    // Update the player.
    void FixedUpdate()
    {
        //checks if there is input to the selected controls
        float translation = Input.GetAxis(movementAxis) * m_CharacterSpeed;
        //so the player moves at playerSpeed units per sec not per frame
        translation *= Time.deltaTime;
        //moves the player
        transform.Translate(0, 0, translation);


        leftHand = transform.position.z - halfWidth;

        // Move up one lane.
        if (Input.GetAxis(laneChangingAxis) <0)
        { 
                changeLanes(Lane.FrontLane, "Rail");

        }
		// Move up one lane.
		if (Input.GetAxis(laneChangingAxis) > 0)
        {
            //foreach (GameObject otherPlayer in PlayerList)
            //{
            //    float otherLefthand = otherPlayer.transform.position.z - halfWidth;
            //    float otherRightHand = otherPlayer.transform.position.z + halfWidth;
            //    if (otherLefthand < leftHand &&  otherRightHand > leftHand)
            //    {
            //       // Debug.Log("Collision forward");
            //    }
            //}
            changeLanes(Lane.BackLane, "Rail (1)");
        }
        // If the character is currently moving between lanes.
        if(laneChangingSpeed > 1)
        {
            laneChangingSpeed = 1;
        }

        if (m_ChangingLanes)
		{
            if(laneChangingSpeed < 1)
            {
                laneChangingSpeed +=  Time.deltaTime * m_CharacterSpeed;
                m_TargetLane.z = transform.position.z;
                m_TargetLane.y = transform.position.y;
                transform.position = Vector3.Lerp(transform.position, m_TargetLane, laneChangingSpeed);
            }
            else 
            {
              laneChangingSpeed = 0.0f;
		      m_ChangingLanes = false;
            }
		}

		
		// If the current lane is lane 1, check for shooting.
		if (m_CurrentLane == Lane.FrontLane)
		{
			// Shoot a bullet.
			if (Input.GetAxis(fireAxis) > 0 && m_ShootingCooldown <= 0.0f)
			{
				ShootBullet();
				m_ShootingCooldown = m_MaxShootingCooldown;
			}
		}
        // Else if the cooldown is greater than 0, decrease the cooldown.
        if (m_ShootingCooldown > 0.0f)
        {
            m_ShootingCooldown -= Time.deltaTime;
        }

    }


	// Shoot a bullet.
	private void ShootBullet()
	{
		// The spawn point of the bullet.
        Vector3 bulletSpawnPoint = new Vector3((transform.position.x - buttonSpawnDistance), (transform.position.y + buttonSpawnHeight), transform.position.z);

		// Clone the bullet at the bullet spawn point.
		GameObject bullet = Instantiate(m_Bullet, bulletSpawnPoint, transform.rotation);

		// Set the bullet's colour to this player's colour.
		bullet.GetComponent<PlayerProjectile>().SetColour(m_Colour);
	}

    private void changeLanes(Lane targetLane, string railName)
    {
        if (m_CurrentLane != targetLane)
        {
            m_CurrentLane = targetLane;
            GameObject newLane = GameObject.Find(railName);

            m_TargetLane = transform.position;

            m_TargetLane.x = newLane.transform.position.x;

            m_ChangingLanes = true;
        }
    }
}