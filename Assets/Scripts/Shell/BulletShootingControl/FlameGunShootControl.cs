using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FlameGunShootControl : NetworkBehaviour
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
        CmdFire(tankManagerment.canFire);
    }
    [Command]
    void CmdFire(bool isFire)
    {
        RpcFire(isFire);
    }
    [ClientRpc]
    void RpcFire(bool isFire)
    {
        animator.SetBool("FTOn", isFire);
        damageBox.SetActive(isFire);
        tankManagerment.HandleMusic(isFire);
    }
    // Update is called once per frame

}
