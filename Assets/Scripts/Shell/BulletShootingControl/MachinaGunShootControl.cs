using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MachinaGunShootControl : MonoBehaviour
{
    public float recoil;
    public GameObject fireEffect;
    Transform m_FireTransform;

    Vector3 directionBullet;
    private TankManagerment tankManagerment;
    float lastFireTime = 0;

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
    }

    void CheckRecoil(ref Vector3 direction)
    {
        float recoilMagnitude = recoil;
        direction.x = direction.x + Random.Range(-recoilMagnitude, recoilMagnitude) * 0.1f;
        direction.z = direction.z + Random.Range(-recoilMagnitude, recoilMagnitude) * 0.1f;
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
        CheckRecoil(ref directionBullet);
        Vector3 velo = tankManagerment.currentBullet.velocity * directionBullet;
        shell.velocity = velo;
     
    }
    private bool isMouseOnGui(int id)
    {
        return EventSystem.current.IsPointerOverGameObject(id);
    }
    void FixedUpdate()
    {
        if (Input.touchCount > 0)
        {
            foreach (var touch in Input.touches)
            {
                if(!isMouseOnGui(touch.fingerId))
                {
                    Fire();
                    lastFireTime = Time.time;

                }
            }
        }


        //if (Input.GetMouseButton(0))
        //{
        //    if (Time.time - lastFireTime > tankManagerment.currentBullet.timeReload)
        //    {
        //        Fire();
        //        lastFireTime = Time.time;
        //        tankManagerment.HandleMusic(true);
        //    }
        //}
        //else
        //{
        //    tankManagerment.HandleMusic(false);
        //    fireEffect.SetActive(false);
        //}
    }

    private void Fire()
    {
        fireEffect.SetActive(true);
        CheckRayCastToMouse();
        GenerateShell();
    }
}
