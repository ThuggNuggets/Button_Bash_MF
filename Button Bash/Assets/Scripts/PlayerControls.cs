using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;
using UnityEngine.UI;

public class TEMPORARYPLAYERCONTROLS : MonoBehaviour
{
	// Enum of the lanes.
	public enum Lane
	{
		FrontLane,
		BackLane
	}
    // the material the player is
    private Material m_Material;
    // The character's speed.
    public float m_CharacterSpeed = 1.0f;

    // character's personal ammunition text
    public Text playerAmmoText;

    // self-explanatory
    public int maxAmmo = 5;
    private int currentAmmo;
    private bool canShoot;


    // The bullets the player shoots.
    public GameObject m_Bullet;

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

    //how far ahead the button will spawn
    public float buttonSpawnDistance = 0.5f;
    public float buttonSpawnHeight = 0.5f;

    //input from controllers
    private float xAxis;
    private float yAxis;
    private float deadZone = 0.5f;

    LineRenderer line = new LineRenderer();
    //stop running into the back of other players
    private Vector3 leftHand = new Vector3 (0, 0, -1);
    private Vector3 rightHand = new Vector3(0, 0, 1);
    //rigidbody
    private Rigidbody body;
    //game manager
    GameManager gm;
    private void Start()
    {
        switch (playerNumber)
        {
            case 0:
                {
                    m_Colour = Colours.Colour.Blue;
                    m_Material.color = Color.blue;
                    break;
                }
            case 1:
                {
                    m_Colour = Colours.Colour.Red;
                    m_Material.color = Color.red;
                    break;
                }
            case 2:
                {
                    m_Colour = Colours.Colour.Green;
                    m_Material.color = Color.green;
                    break;
                }
            case 3:
                {
                    m_Colour = Colours.Colour.Yellow;
                    m_Material.color = Color.yellow;
                    break;
                }
            default:
                {
                    break;
                }
        }

    }
    // Constructor.
    void Awake()
    {
        m_Material = GetComponent<Renderer>().material;
        m_MaxShootingCooldown = m_ShootingCooldown;
		m_ShootingCooldown = 0.0f;
        line = GetComponent<LineRenderer>();
        currentAmmo = maxAmmo;
        body = GetComponent<Rigidbody>();
        gm = GameManager.GetInstance();
        switch(gameObject.tag)
        {
                case "Player1":
                {
                    playerNumber = gm.GetPlayerCharacter(0);
                    
                    break;
                }
            case "Player2":
                {
                    playerNumber = gm.GetPlayerCharacter(1);
                    break;
                }
            case "Player3":
                {
                    playerNumber = gm.GetPlayerCharacter(2);
                    break;
                }
            case "Player4":
                {
                    playerNumber = gm.GetPlayerCharacter(3);
                    break;
                }
        }
        playerNumber++;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ammopile")
        {
            currentAmmo = maxAmmo;
            playerAmmoText.text = maxAmmo.ToString();
        }
    }

    // Update the player.
    void FixedUpdate()
    {//movement on the Z-axis
        switch (playerNumber)
        {
            case 0:
                {
                    xAxis = XCI.GetAxis(XboxAxis.LeftStickX, XboxController.First);
                    yAxis = XCI.GetAxis(XboxAxis.LeftStickY, XboxController.First);
                    break;
                }
            case 1:
                {
                    xAxis = XCI.GetAxis(XboxAxis.LeftStickX, XboxController.Second);
                    yAxis = XCI.GetAxis(XboxAxis.LeftStickY, XboxController.Second);
                    break;
                }
            case 2:
                {
                    xAxis = XCI.GetAxis(XboxAxis.LeftStickX, XboxController.Third);
                    yAxis = XCI.GetAxis(XboxAxis.LeftStickY, XboxController.Third);
                    break;
                }
            case 3:
                {
                    xAxis = XCI.GetAxis(XboxAxis.LeftStickX, XboxController.Fourth);
                    yAxis = XCI.GetAxis(XboxAxis.LeftStickY, XboxController.Fourth);
                    break;
                }
        }

        //checks if there is input to the selected controls
        float translation = xAxis * m_CharacterSpeed;
        //so the player moves at playerSpeed units per sec not per frame
        translation *= Time.deltaTime;
        //moves the player
        transform.Translate(0, 0, translation);
        //removes drifting after colliding with other players
        body.velocity = new Vector3(0, 0, 0);
        //draw a raycast so players can see where the button will go


       


        // Move up a lane.
        if (yAxis > deadZone)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position+leftHand, transform.TransformDirection(-Vector3.right), out hit, 3, 1) || Physics.Raycast(transform.position +rightHand, transform.TransformDirection(-Vector3.right), out hit, 3, 1))
            {
                Debug.Log("HIT forwards");
            }
            else
            {
            changeLanes(Lane.FrontLane, "Rail");
            }
        }
		// Move down a lane.
		if (yAxis < -deadZone)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position + leftHand, transform.TransformDirection(Vector3.right), out hit, 3, 1) || Physics.Raycast(transform.position + rightHand, transform.TransformDirection(Vector3.right), out hit, 3, 1))
            {
                Debug.Log("HIT forwards");
            }
            else
            {
                Debug.Log("NO HIT FORWARDS");
            changeLanes(Lane.BackLane, "Rail (1)");
            }
        }
       
        // If the character is currently moving between lanes.Z
        if (laneChangingSpeed > 1)
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

        RaycastHit aim;
        Vector3 start = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        var ray = -transform.right;
        if (Physics.Raycast(start, ray, out aim, 100))
        {
            Vector3[] points = new Vector3[2];
            points[0] = transform.position;
            points[1] = aim.point;
            line.SetPositions(points);
        }
        // If the current lane is lane 1, check for shooting.
        if (m_CurrentLane == Lane.FrontLane)
		{
           
            // Shoot a bullet.
            switch (playerNumber)
            {
                case 0:
                    {
		            if (XCI.GetButton(XboxButton.A, XboxController.First) && m_ShootingCooldown <= 0.0f)
		            {
                            // check if ammo is not zero
                            AmmoCheck();
                            if (canShoot == true)
                            {
                                ShootBullet();
                                m_ShootingCooldown = m_MaxShootingCooldown;
                                // decrement this player's ammo upon shooting
                                --currentAmmo;
                                playerAmmoText.text = currentAmmo.ToString();
                            }
		            }
                        break;
                    }
                case 1:
                    {
                        if (XCI.GetButton(XboxButton.A, XboxController.Second) && m_ShootingCooldown <= 0.0f)
                        {
                            AmmoCheck();
                            if (canShoot == true)
                            {
                                ShootBullet();
                                m_ShootingCooldown = m_MaxShootingCooldown;
                                --currentAmmo;
                                playerAmmoText.text = currentAmmo.ToString();
                            }
                        }
                        break;
                    }
                case 2:
                    {
                        if (XCI.GetButton(XboxButton.A, XboxController.Third) && m_ShootingCooldown <= 0.0f)
                        {
                            AmmoCheck();
                            if (canShoot == true)
                            {
                                ShootBullet();
                                m_ShootingCooldown = m_MaxShootingCooldown;
                                --currentAmmo;
                                playerAmmoText.text = currentAmmo.ToString();
                            }
                        }
                        break;
                    }
                case 3:
                    {
                        if (XCI.GetButton(XboxButton.A, XboxController.Fourth) && m_ShootingCooldown <= 0.0f)
                        {
                            AmmoCheck();
                            if (canShoot == true)
                            {
                                ShootBullet();
                                m_ShootingCooldown = m_MaxShootingCooldown;
                                --currentAmmo;
                                playerAmmoText.text = currentAmmo.ToString();
                            }
                        }
                        break;
                    }
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

    // checks if current ammo is not zero, sets canShoot variable to false if at 0
    private void AmmoCheck()
    {
        if (currentAmmo > 0)
        {
            canShoot = true;
        }
        else
        {
            canShoot = false;
        }
    }
}