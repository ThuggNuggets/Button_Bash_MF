using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//removes self and bullet on hit
/*put this code on the Letter Block enemy*/
public class LetterBlockBehaviour : MonoBehaviour
{
    public GameObject m_DeathPA;
    public int m_Health = 2;
    //flinging the enemy when they have no health
    public float m_VerticalFling = 40;

    //flinging
    int m_FlingRotation;

    //flashing when hit
    private Renderer m_Renderer;
    public Material m_WhiteMaterial;
    bool m_Flash = false;
    private int m_FlashTimer = 10;
    public int m_MaxFlashTimer = 10;
    Material m_OriginalMat;

	private float m_Speed;

	private Animator m_Animator;

	private int m_RandomRotateRoll;

	private bool m_ChangingLanes = false;

	private float m_RollTimer;
    /// <summary>
    /// sets variables 
    /// </summary>
    private void Awake()
    {
        m_FlashTimer = m_MaxFlashTimer;
        m_Renderer = transform.GetChild(0).transform.GetChild(0).GetComponent<Renderer>();
        m_OriginalMat = m_Renderer.material;
		//get random fling values
		float minXFling = gameObject.GetComponent<EnemyBehaviour>().m_Speed;
        m_FlingRotation = Random.Range(0, 3);

		GetComponent<EnemyBehaviour>().AmLetterBlock();
		m_Speed = GetComponent<EnemyBehaviour>().m_Speed;
		m_Animator = transform.GetChild(0).GetComponent<Animator>();
    }
    /// <summary>
    /// fling when defeated
    /// </summary>
    private void FixedUpdate()
    {
		if (m_ChangingLanes == true)
		{
			if (m_RollTimer <= 0.0f)
			{
				if (m_RandomRotateRoll == 0)
					transform.Rotate(0, -90, 0);
				else
					transform.Rotate(0, 90, 0);

				m_ChangingLanes = false;
			}
			else
				m_RollTimer -= Time.deltaTime;
		}

		transform.Translate(new Vector3(m_Speed, 0, 0) * Time.deltaTime);

		m_FlashTimer--;
        if (m_Health <= 0)
        {
            transform.Translate(new Vector3(-50, m_VerticalFling, 0) * Time.deltaTime, Space.World);
            //destroy self and bullet on collision
            Destroy(gameObject, 2);
            switch (m_FlingRotation)
            {
                case 0:
                    {
                        transform.Rotate(10, 0, 0);
                        break;
                    }
                case 1:
                    {
                        transform.Rotate(0, 10, 0);
                        break;
                    }
                case 2:
                    {
                        transform.Rotate(0, 0, 10);
                        break;
                    }
            }
        }
        if (m_Flash)
        {
            m_Renderer.material = m_WhiteMaterial;
            if (m_FlashTimer < 0)
            {
                m_Flash = false;
            }
        }
        else
        {
            m_Renderer.material = m_OriginalMat;
        }
    }
    /// <summary>
    /// when it collides with an object if it has the "bullet" tag reduce health and if no health destroy self
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            m_Health--;
            m_Flash = true;
            m_FlashTimer = m_MaxFlashTimer;
            Destroy(collision.gameObject);
            //soud trest alex
            SoundManager sm = GameObject.Find("Sound bucket ").GetComponent<SoundManager>();
            AudioSource ac = GetComponent<AudioSource>();
            ac.clip = sm.m_SoundClips[1];
            ac.pitch = Random.Range(1, 3);
            ac.Play();
            if (m_Health == 0)
            {
                Instantiate(m_DeathPA, transform.position,transform.rotation);
            }
			else
			{
				m_RandomRotateRoll = Random.Range(0, 2);
				switch(m_RandomRotateRoll)
				{
					case 0:
						transform.Rotate(new Vector3(0, 90, 0));
						break;
					case 1:
						transform.Rotate(new Vector3(0, -90, 0));
						break;
				}
				m_ChangingLanes = true;
				//m_Animator.Play("Roll");
				m_RollTimer = m_Animator.GetCurrentAnimatorStateInfo(0).length / 8;
			}
        }
    }
}
