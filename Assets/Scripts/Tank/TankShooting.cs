using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class TankShooting : MonoBehaviour
{
    public Transform m_FireTransform;


    Vector3 directionBullet;
    private TankManagerment tankManagerment;
    float lastFireTime = 0;
    private Vector3 mouseHitPosition;

    void Start()
    {
        tankManagerment = GetComponent<TankManagerment>();
    }

    void CheckRecoil(ref Vector3 direction)
    {
        //float recoilMagnitude = tankManagerment.currentBullet.recoil;
        //direction.x = direction.x + Random.Range(-recoilMagnitude, recoilMagnitude) * 0.1f;
        //direction.z = direction.z + Random.Range(-recoilMagnitude, recoilMagnitude) * 0.1f;
    }
    void CheckRayCastToMouse()
    {
        //var dist = Vector3.Distance(Camera.main.transform.position, transform.position);
        //var v3Pos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dist);
        //v3Pos = Camera.main.ScreenToWorldPoint(v3Pos);
        //var distanceBetween = Vector3.Distance(v3Pos, transform.position) * 2;
        // return Mathf.Clamp(distanceBetween, 6, 28);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            hit.point.Set(hit.point.x, 0.5f, hit.point.z);
            mouseHitPosition = hit.point;
            directionBullet = (hit.point - m_FireTransform.position).normalized;
        }
    }
    void GenerateShell()
    {
        Rigidbody shell = Instantiate(tankManagerment.currentBullet.prefab, m_FireTransform.position, m_FireTransform.rotation).GetComponent<Rigidbody>();

        if(tankManagerment.currentBullet.type != BulletType.ElectricBullet)
        {
            CheckRecoil(ref directionBullet);
            Vector3 velo = tankManagerment.currentBullet.velocity * directionBullet;
            shell.velocity = velo;
        }else
        {
            shell.GetComponent<FireToATarget>().Fire(mouseHitPosition);
        }
   
    }
    void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            if (Time.time - lastFireTime > tankManagerment.currentBullet.timeReload)
            {
                //Vector2 mousePosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
                //if (mousePosition.x < 0.3f && mousePosition.y < 0.5f) //Check mouse on Joystick
                //    return;
                if(tankManagerment.currentBullet.type == BulletType.FlameGun)
                {

                }
                else
                {
                    //tankManagerment.currentBulletShootControl.Fire()
                    lastFireTime = Time.time;
                }
               
                tankManagerment.HandleMusic(true);
            }
        }else
        {
            tankManagerment.HandleMusic(false);
        }
     }
    private void Update()
    {
        // Track the current state of the fire button and make decisions based on the current launch force.

    }


    private void Fire()
    {
        // Instantiate and launch the shell.
    }
}