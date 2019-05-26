using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandMineControl : MonoBehaviour
{
    float timeActive = 1;
    bool isActive = false;
    float countActive = 0;
    public ParticleSystem explosionPS;
    public LayerMask m_TankMask;
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
            if (Time.time - countActive > 0.5f)
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
    void OnCollisionStay(Collision other)
    {
        OnCollisionEnter(other);
    }
    void OnCollisionEnter(Collision other)
    {
        if (isActive)
        {
            if(other.gameObject.tag.Equals("Tank"))
            {
                Collider[] listCollider = Physics.OverlapSphere(transform.position, 2.4f, m_TankMask);
                for (int i = 0; i < listCollider.Length; i++)
                {
                    TankHealth targetTankHeal = listCollider[i].GetComponent<TankHealth>();
                    if (!targetTankHeal)
                        continue;
                    targetTankHeal.TakeDamage(25f);
                }

                explosionPS.transform.parent = null;
                explosionPS.Play();
                Destroy(explosionPS.gameObject, explosionPS.duration);
                Destroy(gameObject);
            }
        }
    }
}
