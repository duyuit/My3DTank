using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameGunShootControl : MonoBehaviour
{
    public GameObject flameGunParticle;
    public Animator animator;
    public GameObject damageBox;
    TankManagerment tankManagerment;
    // Start is called before the first frame update
    void OnDisable()
    {
        flameGunParticle.GetComponent<ParticleSystem>().Stop();
        flameGunParticle.SetActive(false);
        
    }

    void OnEnable()
    {
        flameGunParticle.GetComponent<ParticleSystem>().Play();
        flameGunParticle.SetActive(true);
    }

   
    // Start is called before the first frame update
    void Start()
    {
        tankManagerment = GetComponent<TankManagerment>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (tankManagerment.canFire)
        {
            animator.SetBool("FTOn", true);
            damageBox.SetActive(true);
            tankManagerment.HandleMusic(true);
        } else
        {
            animator.SetBool("FTOn", false);
            damageBox.SetActive(false);
            tankManagerment.HandleMusic(false);
        }

    }

    // Update is called once per frame

}
