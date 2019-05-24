using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandMineControl : MonoBehaviour
{
    float timeActive = 1;
    bool isActive = false;
    float countActive = 0;
    public ParticleSystem explosionPS;
    // Start is called before the first frame update
    void Start()
    {
        countActive = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isActive)
        {
            if (Time.time - countActive > 1)
            {
                isActive = true;
            }
        }
     
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
     //Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
        Gizmos.DrawWireSphere(transform.position, 2.4f);
    }
    void OnCollisionEnter(Collision other)
    {
        if (isActive)
        {
            if(other.gameObject.tag.Equals("Tank"))
            {
                //Collider[] listCollider = Physics.OverlapSphere(transform.position, m_ExplosionRadius, m_TankMask);
                //for (int i = 0; i < listCollider.Length; i++)
                //{
                //    Rigidbody targetRigidbody = listCollider[i].GetComponent<Rigidbody>();
                //    if (!targetRigidbody)
                //        continue;
                //    targetRigidbody.AddExplosionForce(m_ExplosionForce, transform.position, m_ExplosionRadius);
                //    TankHealth targetTankHeal = targetRigidbody.GetComponent<TankHealth>();
                //    if (!targetTankHeal)
                //        continue;
                //    targetTankHeal.TakeDamage(CalculateDamage(targetRigidbody.position));
                //}



                explosionPS.transform.parent = null;
                explosionPS.Play();
                Destroy(explosionPS.gameObject, explosionPS.duration);
                Destroy(gameObject);
            }
        }
    }
}
