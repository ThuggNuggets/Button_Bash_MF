 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;
using UnityEngine.UI;
using UnityEngine.Animations;

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
    //animator
    private Animator m_Animator;
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

    

    private Vector3 m_LeftHand   = new Vector3 (0,0,-1);
    private Vector3 m_RightHand = new Vector3 (0, 0, 1);
    //rigidbody
    private Rigidbody m_Body;
    //horizontal movement
    float m_Translation;
    //lane changing speed
    public float m_laneChangingSpeed = 1;
    //right Vector
    Vector3 m_Right;
    //shunting
    public float m_Shunt = 10.0f;

	private GameObject m_AmmoRing;
	private int m_AmmoRingIterator;
    //----------------------------------------------------------------------------testing----------------------------------------------------------------------------
    bool m_Colliding = false;
    private GameObject m_CollidingObject;
    RaycastHit m_Hit;
    public float m_MaxBounceTimer = 0.2f;
    float m_BounceTimer = 0.2f;
    private float m_CollisionWaitTimer = 0.5f;
    public float m_MaxCollisionWaitTimer = 0.5f;
    bool left = false;
    bool right = false;
    // Constructor.
    /// <summary>
    /// sets player number and set up variables
    /// </summary>
    void Awake()
    {
		m_AmmoRing = transform.GetChild(1).gameObject;
		m_AmmoRingIterator = m_AmmoRing.transform.childCount - 1;
        m_Animator = GetComponent<Animator>();
        m_MaxShootingCooldown = m_ShootingCooldown;
		m_ShootingCooldown = 0.0f;
        m_currentAmmo = m_maxAmmo;
        m_Body = GetComponent<Rigidbody>();
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
        m_Right = transform.TransformDirection(Vector3.right);
    }
    /// <summary>
    /// when players collide with the ammo piles refill ammo to max
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "ammopile")
        {
			for (int i = m_currentAmmo; i < m_maxAmmo; ++i)
			{
				m_AmmoRing.transform.GetChild(i).gameObject.SetActive(true);
			}
            m_currentAmmo = m_maxAmmo;
            playerAmmoText.text = m_maxAmmo.ToString();

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player1" || collision.gameObject.tag == "Player2" || collision.gameObject.tag == "Player3" || collision.gameObject.tag == "Player4")
        {
            m_CollidingObject = collision.gameObject;
            m_CollisionWaitTimer = m_MaxCollisionWaitTimer;
            m_Colliding = true;
            if (transform.position.z > m_CollidingObject.transform.position.z)
            {
                right = true;
            }
            else
            {
                left = true;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player1" || collision.gameObject.tag == "Player2" || collision.gameObject.tag == "Player3" || collision.gameObject.tag == "Player4")
        {
            m_Colliding = false;
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
        //removes drifting after colliding with other players
        if(m_BounceTimer >= 0)
        {
            m_BounceTimer -= Time.deltaTime;
			m_Translation = 0;
        }
       else  if (m_BounceTimer < 0)
        {
			//m_Body.velocity -= new Vector3(0, 0, .1f);
			m_Body.velocity = new Vector3(0, 0, 0);
        }

        m_Translation = m_xAxis * m_CharacterSpeed;
        m_Translation *= Time.deltaTime;

        //move the players out of each other if overlapping
        if (m_Colliding)
        {
            //m_Translation = 0;
            if (transform.position.z < m_CollidingObject.transform.position.z)
            {
                transform.position += new Vector3(0, 0, -m_Shunt*Time.deltaTime);
            }
            else if (transform.position.z >= m_CollidingObject.transform.position.z)
            {
                 transform.position += new Vector3(0, 0, m_Shunt*Time.deltaTime);
            }
        }
        //stops player z input while timer is greater than 0
        if(m_CollisionWaitTimer > 0)
        {
            m_CollisionWaitTimer -= Time.deltaTime;
            m_Translation = 0;
        }
        else if(m_CollisionWaitTimer <= 0)
        {
            left = false;
            right = false;
        }
        //drift slightly
        if(left && m_CollisionWaitTimer > 0)
        {
            transform.position += new Vector3(0, 0, -2*Time.deltaTime);
        }
        else if (right && m_CollisionWaitTimer > 0)
        {
            transform.position += new Vector3(0, 0, 2*Time.deltaTime);
        }

        //moves the player
        transform.position += new Vector3(0, 0, m_Translation);
        m_Body.MovePosition(transform.position);


        // Move to front lane
        if (m_yAxis > m_deadZone)
        {
                if (!m_ChangingLanes)
                {
                    changeLanes(Lane.FrontLane, "Rail");
                }
        }
        // Move to back lane
        if (m_yAxis < -m_deadZone)
        {
                if (!m_ChangingLanes)
                {
                    changeLanes(Lane.BackLane, "Rail (1)");
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
            }
            // Shoot a bullet.

            if (XCI.GetButton(XboxButton.A, (XboxController)m_playerNumber + 1) && m_ShootingCooldown <= 0.0f)
            {
                // check if ammo is not zero
                if (m_currentAmmo > 0)
                {
                    ShootBullet();
                    m_ShootingCooldown = m_MaxShootingCooldown;
                    // decrement this player's ammo upon shooting
                    --m_currentAmmo;
					m_AmmoRing.transform.GetChild(m_currentAmmo).gameObject.SetActive(false);
                    playerAmmoText.text = m_currentAmmo.ToString();
				}
            }
        }
        else if (m_CurrentLane == Lane.BackLane)
        {
            Vector3[] points = new Vector3[2];
            points[0] = transform.position;
            points[1] = transform.position;
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
            //m_Animator.Play("IsThrow");
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

}