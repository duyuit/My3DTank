using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BulletInfomation
{
    public GameObject prefab;
    public BulletType type;
    public float timeReload;
    public float force;
}
public enum BulletType
{
    TankShell = 0,
    MachineGunBullet = 1,
}
public class TankManagerment : MonoBehaviour
{
    public List<BulletInfomation> listBullet;
    public BulletInfomation currentBullet;

    // Start is called before the first frame update
    void Start()
    {
        currentBullet = listBullet[1];
    }

    // Update is called once per frame
    void Update()
    {

    }
  
    public void BulletSelect(int typeID)
    {
       
        foreach(var bullet in listBullet)
        {
            if (bullet.type == (BulletType) typeID)
            {
                currentBullet = bullet;
                return;
            }
        }
    }
}

