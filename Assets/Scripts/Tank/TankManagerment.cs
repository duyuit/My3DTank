using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TankManagerment : MonoBehaviour
{
    public BulletInfomation bulletInfomation;
    [HideInInspector]
    public BulletInfo currentBullet;
    public AudioSource fireAudioSource;
    // Start is called before the first frame update
    void Start()
    {
        BulletSelect(1);
        //currentBullet = bulletInfomation.listBullet[0];
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    void UpdateAudioClip()
    {
        fireAudioSource.clip = currentBullet.soundFire;

        switch (currentBullet.type)
        {
            case BulletType.MachineGunBullet:
                fireAudioSource.volume = 0.2f;
                fireAudioSource.pitch = 0.6f;
                fireAudioSource.loop = true;
                break;
            case BulletType.TankShell:
                fireAudioSource.volume = 1f;
                fireAudioSource.pitch = 1f;
                fireAudioSource.loop = false;
                break;
        }
    }
    public void BulletSelect(int typeID)
    {

        foreach (var bullet in bulletInfomation.listBullet)
        {
            if (bullet.type == (BulletType)typeID)
            {
                currentBullet = bullet;
                UpdateAudioClip();
                return;
            }
        }
    }
}

