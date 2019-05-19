using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricShellExplosion : MonoBehaviour
{
    public ParticleSystem m_ExplosionParticles;
    public AudioSource m_ExplosionAudio;
    public float m_MaxLifeTime = 1f;

    private float currentLifeTime = 0;
    private void Start()
    {
        Destroy(gameObject, m_MaxLifeTime);
    }
    void Update()
    {
        currentLifeTime += Time.deltaTime;
        if(currentLifeTime > m_MaxLifeTime)
        {
            Explosion();
        }
    }
    void Explosion()
    {
        m_ExplosionParticles.transform.parent = null;
        m_ExplosionAudio.Play();
        m_ExplosionParticles.Play();


        Destroy(m_ExplosionParticles.gameObject, m_ExplosionParticles.duration);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag.Equals("Tank"))
        {
            Rigidbody targetRigidbody = other.gameObject.GetComponent<Rigidbody>();
            if (targetRigidbody != null)
            {
                TankHealth targetTankHeal = targetRigidbody.GetComponent<TankHealth>();

                if (targetTankHeal)
                {
                    targetTankHeal.TakeDamage(25f);
                    Explosion();
                }
            }
        }
    }
}
