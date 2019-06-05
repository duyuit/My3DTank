using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricShellExplosion : MonoBehaviour
{
    public ParticleSystem m_ExplosionParticles;
    public AudioSource m_ExplosionAudio;
    public float m_MaxLifeTime = 1f;
    bool isExplosed = false;

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
        m_ExplosionParticles.Play();
        m_ExplosionAudio.Play();

        Destroy(m_ExplosionParticles.gameObject, m_ExplosionParticles.duration);
        isExplosed = true;
        Destroy(gameObject, 0.1f);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (isExplosed)
            return;

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
