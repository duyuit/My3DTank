using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameGunDamageCount : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag.Equals("Tank"))
        {
            Rigidbody targetRigidbody = other.GetComponent<Rigidbody>();
            if (targetRigidbody != null)
            {
                TankHealth targetTankHeal = targetRigidbody.GetComponent<TankHealth>();

                if (targetTankHeal)
                {
                    targetTankHeal.TakeDamage(0.5f);
                }
            }
        }
    }
}
