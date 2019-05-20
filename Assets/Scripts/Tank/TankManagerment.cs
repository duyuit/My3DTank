﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TankManagerment : MonoBehaviour
{
    public BulletInfomation bulletInfomation;
    [HideInInspector]
    public BulletInfo currentBullet;
    public AudioSource fireAudioSource;
    public Transform m_FireTransform;
    // Start is called before the first frame update
    void Start()
    {
        currentBullet = bulletInfomation.listBullet[1];
        UpdateAudioClip();

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
            case BulletType.FlameGun:
                fireAudioSource.volume = 0.2f;
                fireAudioSource.pitch = 0.6f;
                fireAudioSource.loop = true;
                break;

            case BulletType.TankShell:
            case BulletType.ElectricBullet:
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
                ActiveComponent(currentBullet.type);
                return;
            }
        }
    }
    public void HandleMusic(bool isPlay)
    {

        if (isPlay)
        {
            if (!fireAudioSource.isPlaying)
                fireAudioSource.Play();
        }
        else
        {
            fireAudioSource.Stop();
        }
    }
    void ActiveComponent(BulletType type)
    {
        switch (type)
        {
            case BulletType.ElectricBullet:
                GetComponent<MachinaGunShootControl>().enabled = false;
                GetComponent<ElectricShellShootControl>().enabled = true;
                GetComponent<TankShellShootControl>().enabled = false;
                GetComponent<FlameGunShootControl>().enabled = false;
                break;
            case BulletType.FlameGun:
                GetComponent<MachinaGunShootControl>().enabled = false;
                GetComponent<ElectricShellShootControl>().enabled = false;
                GetComponent<TankShellShootControl>().enabled = false;
                GetComponent<FlameGunShootControl>().enabled = true;
                break;
            case BulletType.MachineGunBullet:
                GetComponent<MachinaGunShootControl>().enabled = true;
                GetComponent<ElectricShellShootControl>().enabled = false;
                GetComponent<TankShellShootControl>().enabled = false;
                GetComponent<FlameGunShootControl>().enabled = false;
                break;
            case BulletType.TankShell:
                GetComponent<MachinaGunShootControl>().enabled = false;
                GetComponent<ElectricShellShootControl>().enabled = false;
                GetComponent<TankShellShootControl>().enabled = true;
                GetComponent<FlameGunShootControl>().enabled = false;
                break;
        }
    }
}

