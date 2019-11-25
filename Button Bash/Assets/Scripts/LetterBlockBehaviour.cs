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
	public float m_BackForce = 20;
    private float m_fallTimer = 5;
    public float m_MaxFallTimer = 5;
    public float m_despawnTimer = 10;
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

	private float m_RollAnimationLength;

	public Transform m_LeftWall;

	public Transform m_RightWall;
    /// <summary>
    /// sets variables 
    /// </summary>
    private void Awake()
    {
        m_FlashTimer = m_MaxFlashTimer;
        m_Renderer = transform.GetChild(0).transform.GetChild(0).GetComponent<Renderer>();
        m_OriginalMat = m_Renderer.material;
		//get random fling values
        m_FlingRotation = Random.Range(0, 3);

		GetComponent<EnemyBehaviour>().AmLetterBlock();

		m_Speed = GetComponent<EnemyBehaviour>().m_Speed;

		m_Animator = transform.GetChild(0).GetComponent<Animator>();

		m_RollAnimationLength = m_Animator.GetCurrentAnimatorStateInfo(0).length;

		m_RollTimer = m_RollAnimationLength / 8;
    }
    /// <summary>
    /// fling when defeated
    /// </summary>
    private void FixedUpdate()
    {
		// Timer for each roll in the animation.
		if (m_RollTimer > 0)
			m_RollTimer -= Time.deltaTime;
		else
		{
			m_RollTimer = m_RollAnimationLength / 8;
		}

		// Letter block is chaning lanes. + rotate back to origional rotation
		if (m_ChangingLanes == true)
		{
			if (m_RollTimer <= 0.0f)
			{
                if (m_RandomRotateRoll == 0)
                {
                    transform.Rotate(0, -90, 0);
                }
                else
                {
                    transform.Rotate(0, 90, 0);
                }

                    m_ChangingLanes = false;
			}
		}

        //move forward
		transform.Translate(new Vector3(m_Speed, 0, 0) * Time.deltaTime);
        //throws the block behind the bed
        if (m_Health <= 0)
        {
            m_fallTimer -= Time.deltaTime;
            if (m_fallTimer > m_MaxFallTimer*0.5f)
            {
                transform.Translate(new Vector3(-m_BackForce, m_VerticalFling, 0) * Time.deltaTime, Space.World);
            }
            else
            {
                m_FlingRotation = 3;
                transform.Translate(new Vector3(-m_BackForce, -1, 0) * Time.deltaTime, Space.World);
            }
            //destroy self after a certain amount of time
            Destroy(transform.parent.gameObject, m_despawnTimer);
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
                case 3:
                    {
                        transform.Rotate(0, 0, 0);
                        break;
                    }
            }
        }
        //flashing 
		m_FlashTimer--;
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
            Destroy(collision.transform.parent.gameObject);
            //soud trest alex
            SoundManager sm = GameObject.Find("Sound bucket ").GetComponent<SoundManager>();
            AudioSource ac = GetComponent<AudioSource>();
            ac.clip = sm.m_SoundClips[1];
            ac.pitch = Random.Range(1, 3);
            ac.Play();
            if (m_Health == 0)
            {
                m_fallTimer = m_MaxFallTimer;
                m_Speed = 0;
                Instantiate(m_DeathPA, transform.position,transform.rotation);
            }


                m_RandomRotateRoll = Random.Range(0, 2);
                switch (m_RandomRotateRoll)
                {
                    // Rotate left for the camera.
                    case 0:
                        if (m_LeftWall.position.z + 13 < transform.position.z)
                            transform.Rotate(new Vector3(0, 90, 0));
                        else
                        {
                            transform.Rotate(new Vector3(0, -90, 0));
                            m_RandomRotateRoll = 1;
                        }
                        break;
                    // Rotate right for the camera.
                    case 1:
                        if (m_RightWall.position.z - 13 > transform.position.z)
                            transform.Rotate(new Vector3(0, -90, 0));
                        else
                        {
                            transform.Rotate(new Vector3(0, 90, 0));
                            m_RandomRotateRoll = 0;
                        }
                        break;
                }
                m_ChangingLanes = true;
                m_Animator.Play("Roll", 0, 0);
            m_RollTimer = m_RollAnimationLength / 8;
        }
    }
}
