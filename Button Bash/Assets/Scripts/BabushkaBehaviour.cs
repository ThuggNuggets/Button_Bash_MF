using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//allows splitting when hit with an object tagged with "bullet"
//splitting wil only occur if babushka is tagged with either "babushkaLarge" or "babushkaMedium"

/*put this code on all babushka enemies*/
public class BabushkaBehaviour : MonoBehaviour
{
    public GameObject m_NextLevel;
    //where the new babushka doll spawns
    private Vector3 m_AddedVector =new Vector3 (-3,0,0);
    //health for the babushka
    public float m_Health = 2;

    //walls
    public GameObject m_LeftWall;
    public GameObject m_RightWall;
    // the size reduction of the next level babushka
    public float m_Scale = 1;

    //flinging the enemy when they have no health
    public float m_VerticalFling = 50;
    public float m_XFling = 10;
    public float zm_ZFling = 10;

    //used to make sure the next babushka is a colour of the remaining players
    // The script that is storing all the player's lives
    private playerLives m_PlayerLives;
    // The collider that holds the player lives.
    private GameObject m_PlayerLivesCollider;
    
    //used to find the new colour for the next babushka level
    private Colours.Colour m_NewColour;
    private bool m_Valid = false;

    //renderer
    private Renderer m_Rend;
    //colour
    private Colours.Colour m_ChildTopColour;
    bool m_TopColourcorrect = false;
    //materials for other babushkas
    ///-------put them in the list blue,red,green,yellow ------
    public Material[] m_OtherBabushkas;
    //flinging
    int m_FlingRotation;

    /// <summary>
    /// sets all of the variables
    /// </summary>
    private void Awake()
    {
        //dictates where the next babushka will spawn based on how close to the side walls they are
        if(m_LeftWall.transform.position.z + 13 > transform.position.z)
        {
            m_AddedVector = new Vector3(0, 0, 3);
        }
        else  if (m_RightWall.transform.position.z - 13 < transform.position.z)
        {
            m_AddedVector = new Vector3(0, 0, -3);
        }
        //get random fling values
        float minXFling = gameObject.GetComponent<EnemyBehaviour>().m_Speed;
        m_XFling = Random.Range(-m_XFling, -minXFling);
        zm_ZFling = Random.Range(-zm_ZFling, zm_ZFling);

        //finds the object that hold the player lives 
        m_PlayerLivesCollider = GameObject.Find("Collider");
        m_PlayerLives = m_PlayerLivesCollider.GetComponent<playerLives>();

        m_FlingRotation = Random.Range(0, 2);

        m_ChildTopColour = GetComponent<EnemyBehaviour>().m_Colour;
            foreach (Transform child in transform)
            {
                switch (m_ChildTopColour)
                {
                    case Colours.Colour.Blue:
                        {
                            m_Rend = child.GetComponent<Renderer>();
                            m_Rend.material = m_OtherBabushkas[0];
                            break;
                        }
                    case Colours.Colour.Red:
                        {
                            m_Rend = child.GetComponent<Renderer>();
                            m_Rend.material = m_OtherBabushkas[1];
                            break;
                        }
                    case Colours.Colour.Green:
                        {
                            m_Rend = child.GetComponent<Renderer>();
                            m_Rend.material = m_OtherBabushkas[2];
                            break;
                        }
                    case Colours.Colour.Yellow:
                        {
                            m_Rend = child.GetComponent<Renderer>();
                            m_Rend.material = m_OtherBabushkas[3];
                            break;
                        }

                }
         }
    }

  /// <summary>
  /// changes the top part of the next babushka colour to match and flings it when damage is taken
  /// </summary>
    private void FixedUpdate()
    {
        if (!m_TopColourcorrect)
        {
            m_ChildTopColour = GetComponent<EnemyBehaviour>().m_Colour;
            m_TopColourcorrect = true;
            foreach (Transform hello in transform)
            {
                switch (m_ChildTopColour)
                {
                    case Colours.Colour.Blue:
                        {
                            m_Rend = hello.GetComponent<Renderer>();
                            m_Rend.material = m_OtherBabushkas[0];
                            break;
                        }
                    case Colours.Colour.Red:
                        {
                            m_Rend = hello.GetComponent<Renderer>();
                            m_Rend.material = m_OtherBabushkas[1];
                            break;
                        }
                    case Colours.Colour.Green:
                        {
                            m_Rend = hello.GetComponent<Renderer>();
                            m_Rend.material = m_OtherBabushkas[2];
                            break;
                        }
                    case Colours.Colour.Yellow:
                        {
                            m_Rend = hello.GetComponent<Renderer>();
                            m_Rend.material = m_OtherBabushkas[3];
                            break;
                        }

                }
            }
        }
            //flinging top half of babushka when hit
            foreach (Transform child in transform)
        {
            if (m_Health == 1)
            {
                //fling the top half of the babushka
                child.transform.Translate(new Vector3(m_XFling, m_VerticalFling, zm_ZFling) * Time.deltaTime, Space.World);
                Destroy(child.gameObject, 2);
                switch (m_FlingRotation)
                {
                    case 0:
                        {
                            child.transform.Rotate(10, 0, 0);
                            break;
                        }
                    case 1:
                        {
                            child.transform.Rotate(0, 0, 10);
                            break;
                        }

                }
            }
        }
        if (m_Health <= 0)
          {
            //fling the bottom half of the babushka
            transform.Translate(new Vector3(m_XFling, m_VerticalFling, zm_ZFling) * Time.deltaTime, Space.World);
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
                        transform.Rotate(0, 0, 10);
                        break;
                    }

            }
          }
    }

    /// <summary>
    /// when the doll hits something if the object it hit had the "bullet" tag then take damage/spawn next doll
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        // If the bullet collides with an enemy and the enemy shares a colour with the bullet, destroy the bullet.
        if (collision.gameObject.tag == "bullet")
        {
            Destroy(collision.gameObject);
            if (gameObject.tag == "babushkaLarge" )
            {
                if (m_Health > 1)
                {
                    createNextLevel("babushkaMedium");
                }
                m_Health--;
            }
            else if(gameObject.tag == "babushkaMedium")
            {
                if (m_Health > 1)
                {
                    createNextLevel("babushkaSmall");
                }
                m_Health--;
            }
            else
            {
                m_Health = 0;
            }
            //play sound when hit

            SoundManager sm = GameObject.Find("Sound bucket ").GetComponent<SoundManager>();
            AudioSource ac = GetComponent<AudioSource>();
            ac.clip = sm.m_SoundClips[3];
            ac.pitch = Random.Range(0, 3);
            ac.Play();


        }
    }
    /// <summary>
    /// 
    /// the creation of the next babushka doll 
    /// </summary>
    /// <param name="newTag"></param>
    private void createNextLevel(string newTag)
    {
        //creates next level babushka
        GameObject enemy = Instantiate(m_NextLevel, (transform.position + m_AddedVector), transform.rotation);
        enemy.transform.localScale -= new Vector3(m_Scale, m_Scale, m_Scale);
        enemy.gameObject.tag = newTag;

        m_Rend = enemy.GetComponent<Renderer>();
        //changes the enemy oclour based off who still has lives
        while (!m_Valid)
        {
                switch (Random.Range(0, 4))
                {
                    case 0:
                        {
                            if (m_PlayerLives.m_Player1Lives > 0)
                            {
                                m_Valid = true;
                                m_NewColour = Colours.Colour.Blue;
                                m_Rend.material = m_OtherBabushkas[0];
                        }
                            break;
                        }
                    case 1:
                        {
                            if (m_PlayerLives.m_Player2Lives > 0)
                            {
                                m_Valid = true;
                                m_NewColour = Colours.Colour.Red;
                                m_Rend.material = m_OtherBabushkas[1];
                        }
                            break;
                        }
                    case 2:
                        {
                            if (m_PlayerLives.m_Player3Lives > 0)
                            {
                                m_Valid = true;
                                m_NewColour = Colours.Colour.Green;
                                m_Rend.material = m_OtherBabushkas[2];
                        }    
                            break;
                        }
                    case 3:
                        {
                            if (m_PlayerLives.m_Player4Lives > 0)
                            {
                                m_Valid = true;
                                m_NewColour = Colours.Colour.Yellow;
                                m_Rend.material = m_OtherBabushkas[3];
                            }
                            break;
                        }
                }
        }
        
         enemy.GetComponent<EnemyBehaviour>().SetColour((Colours.Colour)m_NewColour);
        
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject,2);
        }
        
    }

}