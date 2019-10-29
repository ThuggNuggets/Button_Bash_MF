using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using XboxCtrlrInput;

public class DeadPlayerReticle : MonoBehaviour
{
	/// <summary>
	/// The player number of the reticle.
	/// </summary>
	public int m_PlayerNumber;

	/// <summary>
	/// The speed at which the reticle moves.
	/// </summary>
	public float m_MoveSpeed;

	/// <summary>
	/// The button for the reticle to shoot.
	/// </summary>
	public GameObject m_Button;

	/// <summary>
	/// Where the projectile's are shot from.
	/// </summary>
	public Transform m_ProjectileSpawnPoint;

	/// <summary>
	/// The x axis of the controller input.
	/// </summary>
	private float m_XAxis;

	/// <summary>
	/// The y axis of the controller input.
	/// </summary>
	private float m_YAxis;

	/// <summary>
	/// The cooldown for shooting.
	/// </summary>
	public float m_ShootingCooldown;

	/// <summary>
	/// The cooldown to use in code.
	/// </summary>
	private float m_CodeShootingCooldown;

	/// <summary>
	/// On startup.
	/// </summary>
    void Awake()
    {
		// Assign the shooting cooldown to be used in code.
		m_CodeShootingCooldown = m_ShootingCooldown;

		// Add the scene loading event, so if the player's return to the main game scene,
		// the reticals won't be there from the last round.
		SceneManager.sceneLoaded += OnSceneLoaded;

		// Change the colour of the reticle to represent which player's reticle it is.
		Renderer renderer = GetComponent<Renderer>();
		switch (m_PlayerNumber)
		{
			case 0:
				renderer.material.color = Color.blue;
				break;
			case 1:
				renderer.material.color = Color.red;
				break;
			case 2:
				renderer.material.color = Color.green;
				break;
			case 3:
				renderer.material.color = Color.yellow;
				break;
		}
	}

	/// <summary>
	/// Update.
	/// </summary>
    void Update()
    {
		// Get the x and y axis of the left stick of the controller.
		m_XAxis = XCI.GetAxis(XboxAxis.LeftStickX, (XboxController)m_PlayerNumber + 1);
		m_YAxis = XCI.GetAxis(XboxAxis.LeftStickY, (XboxController)m_PlayerNumber + 1);

		// Move the reticle by the x and y of the left stick of the controller.
		Vector3 translation = new Vector3(-m_YAxis, 0, m_XAxis);
		translation *= m_MoveSpeed;
		translation *= Time.deltaTime;
		transform.Translate(translation);

		// If the shooting cooldown is less than or equal to 0, check if the A button is pressed to shoot.
		if (m_CodeShootingCooldown <= 0.0f)
		{
			// Check if the A button is pressed, if it is, shoot a projectile and reset the shooting cooldown.
			if (XCI.GetButton(XboxButton.A, (XboxController)m_PlayerNumber + 1))
			{
				ShootProjectile();
				m_CodeShootingCooldown = m_ShootingCooldown;
			}
		}
		// Else, decrease the cooldown by deltaTime.
		else
			m_CodeShootingCooldown -= Time.deltaTime;
    }

	/// <summary>
	/// Shoot a projectile.
	/// </summary>
	private void ShootProjectile()
	{
		// Instantiate the projectile at the point for where the projectiles come from.
		GameObject button = Instantiate(m_Button, m_ProjectileSpawnPoint.position, new Quaternion());

		// Rotate so the button looks at the reticle's transform, so it will shoot straight at it.
		button.transform.LookAt(transform);

		// Rotate 90 degrees on the y axis to make up for the game being along the x axis rather than the z.
		button.transform.Rotate(0.0f, 90, 0.0f);
	}

	/// <summary>
	/// When the game scene is loaded and if the reticle is active, destroy the reticle.
	/// </summary>
	/// <param name="scene">The scene that was loaded.</param>
	/// <param name="mode">Needed for the function, don't know what it does.</param>
	void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		if (scene == SceneManager.GetSceneAt(2) && gameObject.activeSelf == true)
			Destroy(gameObject);
	}
}