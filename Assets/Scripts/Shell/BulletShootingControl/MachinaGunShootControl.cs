using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;


public class MachinaGunShootControl : NetworkBehaviour
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


        Ray ray = Camera.main.ScreenPointToRay(tankManagerment.firePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            hit.point.Set(hit.point.x, 0.5f, hit.point.z);
            directionBullet = (hit.point - m_FireTransform.position).normalized;
        }
    }
   
   
    [Command]
    void CmdGenerateShell(Vector3 direction, Vector3 position, Quaternion rotate)
    {
        GameObject shell = Instantiate(tankManagerment.currentBullet.prefab, position, rotate);

        float recoilMagnitude = 1.0f;
        direction.x = direction.x + Random.Range(-recoilMagnitude, recoilMagnitude) * 0.1f;
        direction.z = direction.z + Random.Range(-recoilMagnitude, recoilMagnitude) * 0.1f;

        Vector3 velo = tankManagerment.currentBullet.velocity * direction;
        NetworkServer.Spawn(shell);
        RpcFire(shell, velo);
    }
    [ClientRpc]
    void RpcFire(GameObject shell, Vector3 velo)
    {
        shell.GetComponent<Rigidbody>().velocity = velo;
    }

    void FixedUpdate()
    {
        if(tankManagerment.canFire)
        {
            if (Time.time - lastFireTime > tankManagerment.currentBullet.timeReload)
            {
                Fire();
                lastFireTime = Time.time;
                tankManagerment.HandleMusic(true);
            }
            else
            {
                tankManagerment.HandleMusic(false);
                fireEffect.SetActive(false);
            }
        }
        else
        {
            tankManagerment.HandleMusic(false);
            fireEffect.SetActive(false);
        }
    }
             
        //if (Input.GetMouseButton(0))
        //{
        //    Vector2 mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        //    if(!IsPointerOverUIObject(mousePos))
        //    {
        //        if (Time.time - lastFireTime > tankManagerment.currentBullet.timeReload)
        //        {
                    
        //            Fire(mousePos);
        //            lastFireTime = Time.time;
        //            tankManagerment.HandleMusic(true);
        //        }
        //    }
        //    else
        //    {
        //        tankManagerment.HandleMusic(false);
        //        fireEffect.SetActive(false);
        //    }
        
        //}
        //else
        //{
        //    tankManagerment.HandleMusic(false);
        //    fireEffect.SetActive(false);
        //}

    private void Fire()
    {
        fireEffect.SetActive(true);
        CheckRayCastToMouse();
        CmdGenerateShell(directionBullet, m_FireTransform.position, m_FireTransform.rotation);
    }
}
