using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShellExplosion : MonoBehaviour
{
    public ParticleSystem m_ExplosionParticles;
    public AudioSource m_ExplosionAudio;
    public float m_MaxLifeTime = 2f;


    private void Start()
    {
        Destroy(gameObject, m_MaxLifeTime);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Tank"))
        {
            Rigidbody targetRigidbody = other.GetComponent<Rigidbody>();
            if (targetRigidbody != null)
            {
                TankHealth targetTankHeal = targetRigidbody.GetComponent<TankHealth>();

                if (targetTankHeal)
                {
                    targetTankHeal.TakeDamage(1.5f);
                }
            }
        }
       


        m_ExplosionParticles.transform.parent = null;
        m_ExplosionAudio.Play();
        m_ExplosionParticles.Play();
        

        Destroy(m_ExplosionParticles.gameObject, m_ExplosionParticles.duration);
        Destroy(gameObject);
    }
    
}
