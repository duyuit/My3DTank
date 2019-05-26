using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxGunDamageCount : MonoBehaviour
{
    public float damage;
    bool isActive = false;
    // Start is called before the first frame update
    void Start()
    {

    }
    void OnDisable()
    {
        isActive = false;
    }
    void OnEnable()
    {
        StartCoroutine(waiter());
        isActive = true;
    }
    IEnumerator waiter()
    {
        yield return new WaitForSeconds(0.5f);
    }
    // Update is called once per frame
    void Update()
    {
    }
    private void OnTriggerStay(Collider other)
    {
        if (!isActive)
            return;
        if (other.gameObject.tag.Equals("Tank"))
        {
            Rigidbody targetRigidbody = other.GetComponent<Rigidbody>();
            if (targetRigidbody != null)
            {
                TankHealth targetTankHeal = targetRigidbody.GetComponent<TankHealth>();

                if (targetTankHeal)
                {
                    targetTankHeal.TakeDamage(damage);
                }
            }
        }
    }
}
