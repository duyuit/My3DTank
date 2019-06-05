using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TankShellShootControl : NetworkBehaviour
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
        Ray ray = Camera.main.ScreenPointToRay(tankManagerment.firePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            hit.point.Set(hit.point.x, 0.5f, hit.point.z);
            directionBullet = (hit.point - m_FireTransform.position).normalized;
        }
    }
    [Command]
    void CmdGenerateShell(Vector3 direction,Vector3 position, Quaternion rotate)
    {
        GameObject shell = Instantiate(tankManagerment.currentBullet.prefab, position, rotate);
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
        if (tankManagerment.canFire)
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
        CmdGenerateShell(directionBullet,m_FireTransform.position,m_FireTransform.rotation);
    }
}
