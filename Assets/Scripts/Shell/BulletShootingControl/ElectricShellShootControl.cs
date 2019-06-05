using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ElectricShellShootControl : NetworkBehaviour
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
        Ray ray = Camera.main.ScreenPointToRay(tankManagerment.firePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            hit.point.Set(hit.point.x, 0.5f, hit.point.z);
            mouseHitPosition = hit.point;
        }
    }
    [Command]
    void CmdGenerateShell(Vector3 position, Quaternion rotate,Vector3 mouseHit)
    {
        GameObject shell = Instantiate(tankManagerment.currentBullet.prefab, position, rotate);
        NetworkServer.Spawn(shell);
        RpcFire(shell, mouseHit);
    }
    [ClientRpc]
    void RpcFire(GameObject shell,Vector3 mouseHit)
    {
        shell.GetComponent<FireToATarget>().Fire(mouseHit);
    }

    void FixedUpdate()
    {
        if (tankManagerment.canFire)
        {
            if (Time.time - lastFireTime > tankManagerment.currentBullet.timeReload)
            {
                Fire();
                lastFireTime = Time.time;
                tankManagerment.HandleMusic(true);
            } else
        {
            tankManagerment.HandleMusic(false);
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
        CmdGenerateShell(m_FireTransform.position, m_FireTransform.rotation,mouseHitPosition);
    }
}
