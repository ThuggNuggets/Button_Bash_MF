using UnityEngine;
//increases object size the more it gets hit with objects tagged with "bullet"
/*put this code on the Teddy bear enemy*/
public class TeddyBearBehaviour : MonoBehaviour
{
    public GameObject m_DeathPA;
    //hits until it is defeated
    public float m_Health = 5;
    // amount its size increased each time
    public float m_Scale = 0.5f;
    //flinging the enemy when they have no health
    public float m_VerticalFling = 85;
    public float m_BackForce = 20;
    private float m_fallTimer = 5;
    public float m_MaxFallTimer = 5;
    public float m_despawnTimer = 10;
    private Rigidbody m_Rb;
    private Renderer m_Renderer;
    BoxCollider m_Collider;
    //flinging
    int m_FlingRotation;
    //white material
    public Material m_WhiteMaterial;

    bool m_Flash = false;
    private int m_FlashTimer = 10;
    public int m_MaxFlashTimer = 10;
    Material m_OriginalMat;
    /// <summary>
    /// sets variables
    /// </summary>
    private void Awake()
    {
        m_FlashTimer = m_MaxFlashTimer;
        m_OriginalMat = transform.GetChild(0).GetComponent<Renderer>().material;
        m_Renderer = transform.GetChild(0).GetComponent<Renderer>();
        //get random fling values
        m_Rb = gameObject.GetComponent<Rigidbody>();
        m_FlingRotation = Random.Range(0, 2);
        m_Collider = GetComponent<BoxCollider>();
    }
    /// <summary>
    /// turns collisions on object off and flings it
    /// </summary>
    private void FixedUpdate()
    {
        m_FlashTimer--;
        if (m_Health <= 0)
        {            
            m_fallTimer -= Time.deltaTime;
            if (m_fallTimer > m_MaxFallTimer * 0.5f)
            {
                transform.Translate(new Vector3(-m_BackForce, m_VerticalFling, 0) * Time.deltaTime, Space.World);
            }
            else
            {
                m_Rb.detectCollisions = true;
                m_FlingRotation = 3;
                transform.Translate(new Vector3(-m_BackForce, -1, 0) * Time.deltaTime, Space.World);
            }
            //destroy self and bullet on collision
            Destroy(gameObject, m_despawnTimer);
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
        if(m_Flash)
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
    /// if collision with object with "bullet" tag then reduce health and grow bigger
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        // If the bullet collides with an enemy and the enemy shares a colour with the bullet, destroy the bullet.
        if (collision.gameObject.tag == "bullet")
        {
            m_Flash = true;
            m_FlashTimer = m_MaxFlashTimer;
            //destroy the bullet
            Destroy(collision.gameObject);
                m_Health--;
                if (m_Health > 0)
                {
                //increases size of bear
                transform.localScale += new Vector3(m_Scale, m_Scale, m_Scale);
                }
                //soud trest alex
                {
                SoundManager sm = GameObject.Find("Sound bucket ").GetComponent<SoundManager>();
                AudioSource ac = GetComponent<AudioSource>();
                ac.pitch = Random.Range(1, 3);
                ac.clip = sm.m_SoundClips[0];
                ac.Play();
                    if(m_Health ==0)
                    {
                    m_Collider.size = new Vector3(m_Collider.size.x, 0.5f, 0.5f);
                    m_fallTimer = m_MaxFallTimer;
                       Instantiate(m_DeathPA, transform.position, transform.rotation);
                    }
            }
        }
    }

}