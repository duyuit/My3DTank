using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TankManagerment : MonoBehaviour
{
    public BulletInfomation bulletInfomation;
    [HideInInspector]
    public BulletInfo currentBullet;
    public AudioSource fireAudioSource;
    public Transform m_FireTransform;
    public bool canFire;
    public Vector2 firePosition;
    // Start is called before the first frame update
    void Start()
    {
        currentBullet = bulletInfomation.listBullet[1];
        UpdateAudioClip();

        //currentBullet = bulletInfomation.listBullet[0];
    }

    private bool IsPointerOverUIObject(Vector2 mousePosition)
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        if (results.Count > 0 && results[0].gameObject.layer != 5)
            return false;

        return results.Count > 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount < 1)
            canFire = false;
        foreach (var touch in Input.touches)
        {
            Vector2 mousePos = new Vector2(touch.position.x, touch.position.y);
            if (!IsPointerOverUIObject(mousePos))
            {
                GameGlobal.Instance.lastFirePosition = mousePos;
                canFire = true;
                firePosition = mousePos;
                return;
            }
            else
            {
                canFire = false;
            }
        }
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
    public void UltimateButtonToggle()
    {
        var ultimateComponent = GetComponent<UltimateShootControl>();
        ultimateComponent.enabled = !ultimateComponent.enabled;
        TurnUltimate(ultimateComponent.enabled);
    }
    void TurnUltimate(bool isTurn)
    {
        GetComponent<UltimateShootControl>().enabled = isTurn;
        if (isTurn)
        {
            GetComponent<MachinaGunShootControl>().enabled = false;
            GetComponent<TankShellShootControl>().enabled = false;
            GetComponent<FlameGunShootControl>().enabled = false;
            GetComponent<ElectricShellShootControl>().enabled = false;
        }
        else
        {
            ActiveComponent(currentBullet.type);
        }
    }
    void ActiveComponent(BulletType type)
    {
        GetComponent<MachinaGunShootControl>().enabled = false;
        GetComponent<TankShellShootControl>().enabled = false;
        GetComponent<FlameGunShootControl>().enabled = false;
        GetComponent<ElectricShellShootControl>().enabled = false;

        switch (type)
        {
            case BulletType.ElectricBullet:
                GetComponent<ElectricShellShootControl>().enabled = true;
                break;
            case BulletType.FlameGun:
                GetComponent<FlameGunShootControl>().enabled = true;
                break;
            case BulletType.MachineGunBullet:
                GetComponent<MachinaGunShootControl>().enabled = true;
           
                break;
            case BulletType.TankShell:
                GetComponent<TankShellShootControl>().enabled = true;
                break;
        }
    }
}

