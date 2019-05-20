using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletType
{
    TankShell = 0,
    MachineGunBullet = 1,
    ElectricBullet = 2,
    FlameGun =3,
}
[Serializable]
public class BulletInfo
{
    public GameObject prefab;
    public BulletType type;
    public float timeReload;
    public float velocity;
    public AudioClip soundFire;
}

public class BulletInfomation : MonoBehaviour
{
    public List<BulletInfo> listBullet;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    //public void BulletSelect(int typeID)
    //{

    //    foreach (var bullet in listBullet)
    //    {
    //        if (bullet.type == (BulletType)typeID)
    //        {
    //            currentBullet = bullet;
    //            return;
    //        }
    //    }
    //}
}

