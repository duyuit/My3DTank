using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankShellShootControl : MonoBehaviour
{
    Transform m_FireTransform;
    public GameObject fireEffect;

    Vector3 directionBullet;
    private TankManagerment tankManagerment;
    float lastFireTime = 0;
    ParticleSystem[] listEffect;

    void OnDisable()
    {
        fireEffect.GetComponent<ParticleSystem>().Stop();
        fireEffect.SetActive(false);

    }

    void OnEnable()
    {
        fireEffect.GetComponent<ParticleSystem>().Play();
        fireEffect.SetActive(true);
    }

    void Start()
    {
        tankManagerment = GetComponent<TankManagerment>();
        m_FireTransform = tankManagerment.m_FireTransform;
        listEffect = fireEffect.GetComponentsInChildren<ParticleSystem>();
    }

    void CheckRayCastToMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            hit.point.Set(hit.point.x, 0.5f, hit.point.z);
            directionBullet = (hit.point - m_FireTransform.position).normalized;
        }
    }
    void GenerateShell()
    {
        Rigidbody shell = Instantiate(tankManagerment.currentBullet.prefab, m_FireTransform.position, m_FireTransform.rotation).GetComponent<Rigidbody>();
        Vector3 velo = tankManagerment.currentBullet.velocity * directionBullet;
        shell.velocity = velo;

    }
    void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            if (Time.time - lastFireTime > tankManagerment.currentBullet.timeReload)
            {
                Fire();
                lastFireTime = Time.time;
                HandleMusic(true);
            }
        }
        else
        {
            HandleMusic(false);
        }
    }

    void HandleMusic(bool isPlay)
    {
        if (isPlay)
        {
            tankManagerment.fireAudioSource.Play();
        }
        else
        {
            tankManagerment.fireAudioSource.Stop();
        }
    }
    private void Fire()
    {
        foreach (var effect in listEffect)
        {
            effect.Play();
        }
        CheckRayCastToMouse();
        GenerateShell();
    }
}
