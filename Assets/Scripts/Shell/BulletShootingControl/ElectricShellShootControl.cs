using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricShellShootControl : MonoBehaviour
{
    Transform m_FireTransform;

    private TankManagerment tankManagerment;
    float lastFireTime = 0;
    private Vector3 mouseHitPosition;

    void Start()
    {
        tankManagerment = GetComponent<TankManagerment>();
        m_FireTransform = tankManagerment.m_FireTransform;
    }

    void CheckRayCastToMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            hit.point.Set(hit.point.x, 0.5f, hit.point.z);
            mouseHitPosition = hit.point;
        }
    }
    void GenerateShell()
    {
        Rigidbody shell = Instantiate(tankManagerment.currentBullet.prefab, m_FireTransform.position, m_FireTransform.rotation).GetComponent<Rigidbody>();
        shell.GetComponent<FireToATarget>().Fire(mouseHitPosition);

    }
    void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            if (Time.time - lastFireTime > tankManagerment.currentBullet.timeReload)
            {
                Fire();
                lastFireTime = Time.time;
                tankManagerment.HandleMusic(true);
            }
        }
        else
        {
            tankManagerment.HandleMusic(false);
        }
    }

    private void Fire()
    {
        CheckRayCastToMouse();
        GenerateShell();
    }
}
