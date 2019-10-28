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

	private float m_InitialAngle;

	/// <summary>
	/// On startup.
	/// </summary>
    void Awake()
    {
		//gameObject.SetActive(false);
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

		// Check if the A button is pressed, if it is, shoot a projectile.
		if (XCI.GetButton(XboxButton.A, (XboxController)m_PlayerNumber + 1))
			ShootProjectile();
    }

	private void ShootProjectile()
	{
		// Instantiate the projectile at the point for where the projectiles come from.
		GameObject button = Instantiate(m_Button, m_ProjectileSpawnPoint.position, new Quaternion());
		button.GetComponent<PlayerProjectile>().enabled = false;

		Rigidbody rb = button.GetComponent<Rigidbody>();

		Vector3 p = transform.position;

		float gravity = Physics.gravity.magnitude;

		float angle = Mathf.Sqrt(Mathf.Tan((m_ProjectileSpawnPoint.transform.position.x - transform.position.x) / (m_ProjectileSpawnPoint.transform.position.y - transform.position.y)));
		//float angle = m_InitialAngle * Mathf.Deg2Rad;

		float distance = Vector3.Distance(p, m_ProjectileSpawnPoint.transform.position);
		float yOffset = p.y - m_ProjectileSpawnPoint.transform.position.y;

		float initialVelocity = (1 / Mathf.Cos(angle)) * Mathf.Sqrt((0.5f * gravity * Mathf.Pow(distance, 2)) / (distance * Mathf.Tan(angle) + yOffset));

		Vector3 vel = new Vector3(0, initialVelocity * Mathf.Sin(angle), initialVelocity * Mathf.Cos(angle));

		float angleBetweenObjects = Vector3.Angle(-Vector3.right, transform.position - m_ProjectileSpawnPoint.transform.position);

		Vector3 finalVel = Quaternion.AngleAxis(angleBetweenObjects, Vector3.up) * vel;

		rb.velocity = finalVel;
	}
}