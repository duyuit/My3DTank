using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIShooting : MonoBehaviour
{
    public Transform m_FireTransform;
    public AudioSource m_ShootingAudio;
    public AIRotate rotator;

    Transform player;
    Vector3 directionBullet;
    float lastFireTime = 0;
    float lastRayCast = 0;
    bool isRayhitPlayer = false;
    private TankManagerment tankManagerment;

    void Start()
    {
        tankManagerment = GetComponent<TankManagerment>();
        m_ShootingAudio.clip = tankManagerment.currentBullet.soundFire;
    }
    void CheckRecoil(ref Vector3 direction)
    {
        float recoilMagnitude = tankManagerment.currentBullet.recoil;
        direction.x = direction.x + Random.Range(-recoilMagnitude, recoilMagnitude) * 0.1f;
        direction.z = direction.z + Random.Range(-recoilMagnitude, recoilMagnitude) * 0.1f;
    }
    void GenerateShell()
    {
        Rigidbody shell = Instantiate(tankManagerment.currentBullet.prefab, m_FireTransform.position, m_FireTransform.rotation).GetComponent<Rigidbody>();

        CheckRecoil(ref directionBullet);
        Vector3 velo = tankManagerment.currentBullet.velocity * directionBullet;
        shell.velocity = velo;

    }
    bool CheckRayCastToPlayer()
    {
        if (Vector3.Distance(transform.position, player.transform.position) > 30)
            return false;

        Vector3 targetPosition = player.position;
        targetPosition.y = 0.5f; //Center tank by Y-axis.

        Vector3 rayDelta = (targetPosition - m_FireTransform.position).normalized;

        if (Time.time - lastRayCast > 0.4f)
        {
            Ray ray = new Ray(m_FireTransform.position, rayDelta * 30);
            RaycastHit raycastHit;
            lastRayCast = Time.time;

            if (Physics.Raycast(ray, out raycastHit))
            {
                if (raycastHit.collider.transform == player || raycastHit.collider.name == "Shield")
                {
                    isRayhitPlayer = true;
                    directionBullet = rayDelta;
                }
                else
                {
                    isRayhitPlayer = false;
                }
            }
        }
        return isRayhitPlayer;
    }
    void FixedUpdate()
    {
        if (player == null)
        {
            player = GameObject.Find("Player").transform;
        }
        else
        {
            if (CheckRayCastToPlayer())
            {
                if (Time.time - lastFireTime > tankManagerment.currentBullet.timeReload)
                {
                    GenerateShell();
                    lastFireTime = Time.time;
                    HandleMusic(true);
                }
            }
            else
            {
                HandleMusic(false);
            }
        }
    }
    void HandleMusic(bool isPlay)
    {
        if (isPlay)
        {
            if (!m_ShootingAudio.isPlaying)
                m_ShootingAudio.Play();
        }
        else
        {
            m_ShootingAudio.Stop();
        }
    }
 
}
