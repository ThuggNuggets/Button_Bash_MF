 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;
using UnityEngine.UI;

public class PlayerControls : MonoBehaviour
{
	// Enum of the lanes.
	public enum Lane
	{
		FrontLane,
		BackLane
	}

    // The character's speed.
    public float m_CharacterSpeed = 1.0f;

    // character's personal ammunition text
    public Text playerAmmoText;

    // self-explanatory
    public int m_maxAmmo = 5;
    private int m_currentAmmo;
    private bool canShoot;


    // The bullets the player shoots.
    public GameObject m_Bullet;

    // The cooldown to shooting.
    public float m_ShootingCooldown = 0.0f;

	// The cooldown, for reseting the cooldown.
	private float m_MaxShootingCooldown = 0.0f;

	// The z position of the target lane.
	private Vector3 m_TargetLane = new Vector3();

	// The character's current lane.
	private Lane m_CurrentLane = Lane.FrontLane;

	// If the character is currently changing lanes.
	private bool m_ChangingLanes = false;
    private float m_LaneChangingDistance = 0.0f;

    // The player's colour.
    public int m_playerNumber;

    //how far ahead the button will spawn
    public float m_buttonSpawnDistance = 0.5f;
    public float m_buttonSpawnHeight = 0.5f;

    //input from controllers
    private float m_xAxis;
    private float m_yAxis;
    private float m_deadZone = 0.5f;

    LineRenderer line = new LineRenderer();
    //stop running into the back of other players
    private Vector3 leftHand = new Vector3 (0, 0, -1);
    private Vector3 rightHand = new Vector3(0, 0, 1);
    //rigidbody
    private Rigidbody body;
    //horizontal movement
    float translation;
    //lane changing speed
    public float m_laneChangingSpeed = 1;
    // Constructor.
    /// <summary>
    /// sets player number and set up variables
    /// </summary>
    void Awake()
    {
        m_MaxShootingCooldown = m_ShootingCooldown;
		m_ShootingCooldown = 0.0f;
        line = GetComponent<LineRenderer>();
        m_currentAmmo = m_maxAmmo;
        body = GetComponent<Rigidbody>();
        switch(m_playerNumber)
        {
            case 0:
                {
                    m_playerNumber = GameManager.GetPlayerCharacter(0);
                    
                    break;
                }
            case 1:
                {
                    m_playerNumber = GameManager.GetPlayerCharacter(1);
                    break;
                }
            case 2:
                {
                    m_playerNumber = GameManager.GetPlayerCharacter(2);
                    break;
                }
            case 3:
                {
                    m_playerNumber = GameManager.GetPlayerCharacter(3);
                    break;
                }
        }

    }
    /// <summary>
    /// when players collide with the ammo piles refil ammo to max
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "ammopile")
        {
            m_currentAmmo = m_maxAmmo;
            playerAmmoText.text = m_maxAmmo.ToString();
        }
    }

    // Update the player.
    /// <summary>
    /// controls movement and button throwing
    /// </summary>
    void FixedUpdate()
    {//movement on the Z-axis

        m_xAxis = XCI.GetAxis(XboxAxis.LeftStickX, (XboxController)m_playerNumber + 1);
        m_yAxis = XCI.GetAxis(XboxAxis.LeftStickY, (XboxController)m_playerNumber + 1);


        Debug.DrawRay(transform.position, new Vector3(-m_yAxis * 1.1f, 0, m_xAxis * 1.1f));
        //if (m_ChangingLanes)
        //{
        //    if (m_yAxis < -m_deadZone || m_yAxis > m_deadZone)
        //    {
        //        RaycastHit check;
        //        if (Physics.Raycast(transform.position, new Vector3(-m_yAxis * 1.1f, 0, m_xAxis * 1.1f), out check, 3, 1))
        //        {
        //            //checks if there is input to the selected controls
        //            //translation = 0;
        //            translation = m_xAxis * m_CharacterSpeed;
        //        }
        //        else
        //        {
        //        }
        //    }
        //}
        //else
        //{
        //    translation = m_xAxis * m_CharacterSpeed;
        //}
        //so the player moves at playerSpeed units per sec not per frame

        translation = m_xAxis * m_CharacterSpeed;
        translation *= Time.deltaTime;
        //moves the player
        transform.Translate(0, 0, translation);
        //removes drifting after colliding with other players
        body.velocity = new Vector3(0, 0, 0);
        //draw a raycast so players can see where the button will go




        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y, transform.position.z-1), new Vector3(transform.position.x+1, transform.position.y, transform.position.z - 1));
        // Move to front lane
        if (m_yAxis > m_deadZone)
        {
            RaycastHit hit;
            if (!Physics.Raycast(new Vector3(transform.position.x , transform.position.y, transform.position.z - 1) , transform.TransformDirection(-Vector3.right), out hit, 3, 1) && !Physics.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z + 1), transform.TransformDirection(-Vector3.right), out hit, 3, 1))
            {
                if (!m_ChangingLanes)
                {
                    changeLanes(Lane.FrontLane, "Rail");
                }
            }
        }
        // Move to back lane
        if (m_yAxis < -m_deadZone)
        {
            RaycastHit hit;
            if (!Physics.Raycast(transform.position + leftHand, transform.TransformDirection(Vector3.right), out hit, 3, 1) && !Physics.Raycast(transform.position + rightHand, transform.TransformDirection(Vector3.right), out hit, 3, 1))
            {
                if (!m_ChangingLanes)
                {
                    changeLanes(Lane.BackLane, "Rail (1)");
                }
            }
        }

        // If the character is currently moving between lanes.x

        if (m_ChangingLanes)
        {
            if (m_LaneChangingDistance < 1)
            {
                m_LaneChangingDistance += Time.deltaTime * m_laneChangingSpeed;
                m_TargetLane.z = transform.position.z;
                // m_TargetLane.y = transform.position.y;
                transform.position = Vector3.Lerp(transform.position, m_TargetLane, m_LaneChangingDistance);
                if(m_LaneChangingDistance > .65f)
                {
                    m_LaneChangingDistance = 1;
                }
            }
            else
            {
                m_LaneChangingDistance = 0.0f;
                m_ChangingLanes = false;
            }
        }

        if (m_LaneChangingDistance > 1)
        {
            m_LaneChangingDistance = 1;
        }
        // If the current lane is lane 1, check for shooting.
        if (m_CurrentLane == Lane.FrontLane)
        {
            RaycastHit aim;
            Vector3 start = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
            var ray = -transform.right;
            if (Physics.Raycast(start, ray, out aim, 350))
            {
                Vector3[] points = new Vector3[2];
                points[0] = transform.position;
                points[1] = new Vector3(aim.point.x, transform.position.y, aim.point.z);
                line.SetPositions(points);
            }
            // Shoot a bullet.

            if (XCI.GetButton(XboxButton.A, (XboxController)m_playerNumber + 1) && m_ShootingCooldown <= 0.0f)
            {
                // check if ammo is not zero
                AmmoCheck();
                if (canShoot == true)
                {
                    ShootBullet();
                    m_ShootingCooldown = m_MaxShootingCooldown;
                    // decrement this player's ammo upon shooting
                    --m_currentAmmo;
                    playerAmmoText.text = m_currentAmmo.ToString();
                }
            }
        }
        else if (m_CurrentLane == Lane.BackLane)
        {
            Vector3[] points = new Vector3[2];
            points[0] = transform.position;
            points[1] = transform.position;
            line.SetPositions(points);
        }

        //if the cooldown is greater than 0, decrease the cooldown.
        if (m_ShootingCooldown > 0.0f)
        {
            m_ShootingCooldown -= Time.deltaTime;
        }
    }

	// Shoot a bullet.
    /// <summary>
    /// throws a button
    /// </summary>
	private void ShootBullet()
	{
            // The spawn point of the bullet.
            Vector3 bulletSpawnPoint = new Vector3((transform.position.x - m_buttonSpawnDistance), (transform.position.y + m_buttonSpawnHeight), transform.position.z);

            // Clone the bullet at the bullet spawn point.
            GameObject bullet = Instantiate(m_Bullet, bulletSpawnPoint, transform.rotation);
	}
    /// <summary>
    /// changes the lane the player will move towards
    /// </summary>
    /// <param name="targetLane"></param>
    /// <param name="railName"></param>
    private void changeLanes(Lane targetLane, string railName)
    {
        if (m_CurrentLane != targetLane)
        {
            m_CurrentLane = targetLane;
            GameObject newLane = GameObject.Find(railName);

            m_TargetLane = transform.position;

            m_TargetLane.x = newLane.transform.position.x;
            m_TargetLane.y = newLane.transform.position.y;
            m_ChangingLanes = true;

        }
    }

    // checks if current ammo is not zero, sets canShoot variable to false if at 0
    /// <summary>
    /// checks current ammo if the player can throw a button
    /// </summary>
    private void AmmoCheck()
    {
        if (m_currentAmmo > 0)
        {
            canShoot = true;
        }
        else
        {
            canShoot = false;
        }
    }
}