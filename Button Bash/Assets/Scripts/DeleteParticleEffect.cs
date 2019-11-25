using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteParticleEffect : MonoBehaviour
{
    public float m_SelfDestructTimer = 5;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        m_SelfDestructTimer -= Time.deltaTime;
        if(m_SelfDestructTimer < 0)
        {
            Destroy(gameObject);
        }
    }
}
