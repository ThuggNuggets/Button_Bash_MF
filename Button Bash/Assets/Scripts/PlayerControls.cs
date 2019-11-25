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

    //rigidbody
    private Rigidbody m_Body;
    //horizontal movement
    float m_Translation;
    //lane changing speed
    public float m_laneChangingSpeed = 1;
    //right Vector
    Vector3 m_Right;
    //shunting - power of pushing players when over lapping
    public float m_Shunt = 10.0f;
   

	private GameObject m_AmmoRing;
    private GameObject m_AimingLine;
    private GameObject m_BaseMesh;
    //if the player i fhittin another player
    bool m_Colliding = false;
    private GameObject m_CollidingObject;
    RaycastHit m_Hit;
    //variables for the players bouncing off of each other
    float m_BounceTimer = 0.2f;
    public float m_BounceSpeed = 2;
    public float m_MaxBounceTimer = 0.2f;
    private float m_CollisionWaitTimer = 0.5f;
    public float m_MaxCollisionWaitTimer = 0.5f;
    //collision if they were hit on the left or right side
    bool left = false;
    bool right = false;

    //is player in throwing animation
    private float m_ThrowingTimer = -1;
    private bool m_IsShooting = false;
    // Constructor.
    /// <summary>
    /// sets player number and set up variables
    /// </summary>
    void Awake()
    {
        m_Right = transform.TransformDirection(Vector3.right);
        m_AimingLine = transform.GetChild(0).gameObject;
        m_AmmoRing = transform.GetChild(1).gameObject;
        m_BaseMesh = transform.GetChild(2).gameObject;
        m_Animator = m_BaseMesh.GetComponent<Animator>();
        m_MaxShootingCooldown = m_ShootingCooldown;
        m_Body = GetComponent<Rigidbody>();
        m_currentAmmo = m_maxAmmo;
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
            SoundManager sm = GameObject.Find("Sound bucket ").GetComponent<SoundManager>();
            AudioSource ac = GetComponent<AudioSource>();
            ac.clip = sm.m_SoundClips[13];
            ac.pitch = Random.Range(1, 3);
            ac.Play();
            m_currentAmmo = m_maxAmmo;
        }
    }

    //player bounce
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player1" || collision.gameObject.tag == "Player2" || collision.gameObject.tag == "Player3" || collision.gameObject.tag == "Player4")
        {
            SoundManager sm = GameObject.Find("Sound bucket ").GetComponent<SoundManager>();
            AudioSource ac = GetComponent<AudioSource>();
            ac.clip = sm.m_SoundClips[12];
            ac.pitch = Random.Range(1, 3);
            ac.Play();
            m_CollidingObject = collision.gameObject;
            m_CollisionWaitTimer = m_MaxCollisionWaitTimer;
            m_Colliding = true;
            if (transform.position.z > m_CollidingObject.transform.position.z)
            {
                right = true;
                left = false;
            }
            else
            {
                left = true;
                right = false;
            }
        }
    }

    //not colliding anymore
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
    {
        //movement on the Z-axis
        m_xAxis = XCI.GetAxis(XboxAxis.LeftStickX, (XboxController)m_playerNumber + 1);
        m_yAxis = XCI.GetAxis(XboxAxis.LeftStickY, (XboxController)m_playerNumber + 1);
        //bounces the player when they hit another player
        if(m_BounceTimer >= 0)
        {
            m_BounceTimer -= Time.deltaTime;
			m_Translation = 0;
        }
        //removes drifting after colliding with other players
       else  if (m_BounceTimer < 0)
        {
            m_Body.velocity = new Vector3(0, 0, 0);
        }
  
        //gets the player player input and makes sure the throwing animation is not playering then rotates the player
        if (m_Translation > 0 && m_ThrowingTimer < 0)
        {
            m_BaseMesh.transform.eulerAngles = new Vector3(0, 90, 0);
            m_BaseMesh.transform.position = transform.position + new Vector3(0.2f, -1.4f, 0);
            m_BaseMesh.GetComponent<Animator>().Play("Run");
        }
        else if (m_Translation < 0 && m_ThrowingTimer < 0)
        {
            m_BaseMesh.transform.eulerAngles = new Vector3(0, -90, 0);
            m_BaseMesh.transform.position = transform.position + new Vector3(-0.6f, -1.4f, 0);
            m_BaseMesh.GetComponent<Animator>().Play("Run");
        }
        else if (m_Translation == 0 && m_ThrowingTimer < 0)
        {
            m_BaseMesh.transform.eulerAngles = new Vector3(0, 0, 0);
            m_BaseMesh.transform.position = transform.position + new Vector3(0, -1.4f, 0.5f);
            m_BaseMesh.GetComponent<Animator>().Play("Idle");
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
            transform.position += new Vector3(0, 0, -m_BounceSpeed * Time.deltaTime);
        }
        else if (right && m_CollisionWaitTimer > 0)
        {
            transform.position += new Vector3(0, 0, m_BounceSpeed * Time.deltaTime);
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
                    m_AimingLine.SetActive(true);
                }
        }
        // Move to back lane
        if (m_yAxis < -m_deadZone)
        {
                if (!m_ChangingLanes)
                {
                    changeLanes(Lane.BackLane, "Rail (1)");
                    m_AimingLine.SetActive(false);
                    m_IsShooting = false;
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

            if(XCI.GetButton(XboxButton.A, (XboxController)m_playerNumber + 1) && m_currentAmmo > 0 && m_ShootingCooldown <= 0)
            {
                m_IsShooting = true;
            }
            if (m_IsShooting && m_ShootingCooldown <= 0)
            {
                m_BaseMesh.transform.eulerAngles = new Vector3(0, 0, 0);
                m_BaseMesh.transform.position = transform.position + new Vector3(0, -1.4f, 0.5f);
                ///----------------------------------------animation--------------------------------------------------
                m_BaseMesh.transform.eulerAngles = new Vector3(0, 0, 0);
                m_Animator.Play("Throw");
                m_ThrowingTimer = m_Animator.GetCurrentAnimatorClipInfo(0).Length;
                m_ThrowingTimer /= 2;
                ///--------------------------------------end animation -----------------------------------------------
                // Shoot a bullet
                if (m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.3f && m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.6f)
                {
                    ShootBullet();
                    --m_currentAmmo;
                    m_AmmoRing.transform.GetChild(m_currentAmmo).gameObject.SetActive(false);
                    m_ShootingCooldown = m_MaxShootingCooldown;
                    m_IsShooting = false;
                }
            }
                
        }
        //throwing cooldown
        if (m_ThrowingTimer > 0.0f)
        {
            m_ThrowingTimer -= Time.deltaTime;
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

}