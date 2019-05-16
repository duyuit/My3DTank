using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class TankShooting : MonoBehaviour
{
    public int m_PlayerNumber = 1;
    public Rigidbody m_Shell;
    public Transform m_FireTransform;
    public Slider m_AimSlider;
    public AudioSource m_ShootingAudio;
    public float m_MaxChargeTime = 0.75f;
    public Rotator rotator;

    private TankManagerment tankManagerment;
    float lastUpdate = 0;

    void Start()
    {
        tankManagerment = GetComponent<TankManagerment>();
    }
    Vector3 CalculateForce()
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
            //Debug.DrawLine(m_FireTransform.position, hit.point);
            Vector3 recoilDelta = (hit.point - m_FireTransform.position).normalized;
            recoilDelta.x = recoilDelta.x + Random.Range(-1, 1) * 0.1f;
            recoilDelta.z = recoilDelta.z + Random.Range(-1, 1) * 0.1f;
            return recoilDelta;
        }
        return Vector3.up;


    }
    void GenerateShell()
    {
        rotator.CheckAndRotate();
        Rigidbody shell = Instantiate(tankManagerment.currentBullet.prefab, m_FireTransform.position, m_FireTransform.rotation).GetComponent<Rigidbody>();
        Vector3 velo = tankManagerment.currentBullet.force * CalculateForce();
        shell.velocity = velo;
    }
    void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            if (Time.time - lastUpdate > tankManagerment.currentBullet.timeReload)
            {
                //Vector2 mousePosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
                //if (mousePosition.x < 0.3f && mousePosition.y < 0.5f) //Check mouse on Joystick
                //    return;
                GenerateShell();
                lastUpdate = Time.time;
                HandleMusic(true);
            }
        }else
        {
            HandleMusic(false);
        }
     }
    void HandleMusic(bool isFiring)
    {
        if (isFiring)
        {
            if (!m_ShootingAudio.isPlaying)
            {
                m_ShootingAudio.Play();
            }else
            {
                //m_ShootingAudio.pitch = Random.Range(1.5f, 1.8f); ;
            }
        }
        else
        {
            m_ShootingAudio.Stop();
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