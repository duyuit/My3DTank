using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIShooting : MonoBehaviour
{
    public int m_PlayerNumber = 1;
    public Rigidbody m_Shell;
    public Transform m_FireTransform;
    public AudioSource m_ShootingAudio;
    public float m_MaxChargeTime = 0.75f;
    public AIRotate rotator;
    public Transform player;
    float lastUpdate = 0;
    void Start()
    {
    }
    float CalculateForce()
    {
        var distanceBetween = Vector3.Distance(player.position, transform.position) * 2;
        //Debug.Log(distanceBetween);
        return Mathf.Clamp(distanceBetween, 6, 28);
    }
    void GenerateShell()
    {
        rotator.CheckAndRotate();
        Rigidbody shell = Instantiate(m_Shell, m_FireTransform.position, m_FireTransform.rotation);
        Vector3 velo = m_FireTransform.forward * CalculateForce();
        shell.velocity = new Vector3(velo.x, 0, velo.z);

    }
    bool CheckRayCastToPlayer()
    {
        if (Vector3.Distance(transform.position, player.transform.position) > 30)
            return false;

        Vector3 foward = m_FireTransform.forward;
        foward.y = -0.05f;
        Ray ray = new Ray(m_FireTransform.position, foward);
        Debug.DrawRay(ray.origin, ray.direction * 50, Color.red);
        RaycastHit raycastHit;

        if (Physics.Raycast(ray, out raycastHit))
        {
            if (raycastHit.collider.transform == player || raycastHit.collider.name =="Shield")
            {
                return true;
            }
        }
        return false;
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
                if (Time.time - lastUpdate > m_MaxChargeTime)
                {
                    GenerateShell();
                    lastUpdate = Time.time;
                    HandleMusic();
                }
            }
        }
    }
    void HandleMusic()
    {
        if (!m_ShootingAudio.isPlaying)
        {
            m_ShootingAudio.Play();
        }
    }
 
}
