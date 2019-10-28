using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
		m_CodeShootingCooldown = m_ShootingCooldown;
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

		// Rotate 90 degrees on the y axis to make up for the game being on a weird angle.
		button.transform.Rotate(0.0f, 90, 0.0f);
	}
}