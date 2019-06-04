using UnityEngine;

public class ShellExplosion : MonoBehaviour
{
    public LayerMask m_TankMask;
    public ParticleSystem m_ExplosionParticles;       
    public AudioSource m_ExplosionAudio;              
    public float m_MaxDamage = 25f;                  
    public float m_ExplosionForce = 1000f;            
    public float m_MaxLifeTime = 2f;                  
    public float m_ExplosionRadius = 5f;
    bool isExplosed = false;


    private void Start()
    {
        Destroy(gameObject, m_MaxLifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isExplosed)
            return;
        Collider[] listCollider = Physics.OverlapSphere(transform.position, m_ExplosionRadius, m_TankMask);
        for (int i = 0; i < listCollider.Length; i++)
        {
            Rigidbody targetRigidbody = listCollider[i].GetComponent<Rigidbody>();
            if (!targetRigidbody)
                continue;
            targetRigidbody.AddExplosionForce(m_ExplosionForce, transform.position, m_ExplosionRadius);
            TankHealth targetTankHeal = targetRigidbody.GetComponent<TankHealth>();
            if (!targetTankHeal)
                continue;
            targetTankHeal.TakeDamage(CalculateDamage(targetRigidbody.position));
        }
           
        m_ExplosionParticles.transform.parent = null;
        m_ExplosionParticles.Play();
        m_ExplosionAudio.Play();

        Destroy(m_ExplosionParticles.gameObject, m_ExplosionParticles.duration);
        isExplosed = true;
        Destroy(gameObject,0.1f);

        //GameObject.Find("Main Camera").GetComponent<ShockWave>().GenerateShockWave(transform.localPosition);

        // Find all the tanks in an area around the shell and damage them.

    }


    private float CalculateDamage(Vector3 targetPosition)
    {
        // Calculate the amount of damage a target should take based on it's position.
        Vector3 explosionToTarget = targetPosition - transform.position;
        float explosionDistance = explosionToTarget.magnitude;
        float relativeDistance = (m_ExplosionRadius - explosionDistance) / m_ExplosionRadius;
        float damage = relativeDistance * m_MaxDamage;
        damage = Mathf.Max(0f, damage);
        return damage;
    }
}