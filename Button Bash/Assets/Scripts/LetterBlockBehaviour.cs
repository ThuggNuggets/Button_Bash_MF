using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//removes self and bullet on hit
/*put this code on the Letter Block enemy*/
public class LetterBlockBehaviour : MonoBehaviour
{
    public int m_Health = 2;
    //flinging the enemy when they have no health
    public float m_VerticalFling = 85;
    public float m_XFling = 80;
    public float m_ZFling = 50;

    //flinging
    int m_FlingRotation;

    //flashing when hit
    private Renderer m_Renderer;
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
        m_Renderer = transform.GetChild(0).transform.GetChild(0).GetComponent<Renderer>();
        m_OriginalMat = m_Renderer.material;
		//get random fling values
		float minXFling = gameObject.GetComponent<EnemyBehaviour>().m_Speed;
        m_XFling = Random.Range(-m_XFling, -minXFling);
        m_ZFling = Random.Range(-m_ZFling, m_ZFling);
        m_FlingRotation = Random.Range(0, 3);
    }
    /// <summary>
    /// fling when defeated
    /// </summary>
    private void FixedUpdate()
    {
        m_FlashTimer--;
        if (m_Health <= 0)
        {
            transform.Translate(new Vector3(m_XFling, m_VerticalFling, m_ZFling) * Time.deltaTime, Space.World);
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
        }
    }
}
