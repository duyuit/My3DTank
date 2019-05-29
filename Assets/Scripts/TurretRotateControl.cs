using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;



public class TurretRotateControl : NetworkBehaviour
{
    [SyncVar] Quaternion syncRotation;
    public Transform m_turret;
    public float lerpRate = 15;
    void Start()
    {
        //GetComponent<NetworkIdentity>().AssignClientAuthority(parentPlayer.GetComponent<NetworkIdentity>().connectionToClient);
    }
    void FixedUpdate()
    {
        TranmitRotation();
        LerpRotation();
    }
    void Update()
    {
        if(isLocalPlayer)
            CheckAndRotate();
    }
    void LerpRotation()
    {
        if(!isLocalPlayer)
        {
            m_turret.rotation = Quaternion.Lerp(m_turret.rotation, syncRotation, Time.deltaTime * lerpRate);
        }
    }
    [Command]
    void CmdProvideRotationToServer(Quaternion turretRotation)
    {
        syncRotation = turretRotation;
    }
    [Client]
    void TranmitRotation()
    {
        if(isLocalPlayer)
        {
            CmdProvideRotationToServer(m_turret.rotation);
        }
    }
    public void CheckAndRotate()
    {
        //Get the Screen positions of the object
        Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(m_turret.position);

        //Get the Screen position of the mouse
        Vector2 mouseOnScreen;
#if UNITY_ANDROID
                mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(GameGlobal.Instance.lastFirePosition);
#endif

#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);
#endif

        //Get the angle between the points
        //if (mouseOnScreen.x < 0.3f && mouseOnScreen.y < 0.5f)
        //    return;

        float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);
        m_turret.rotation = Quaternion.Euler(new Vector3(0f, -angle - 90, 0f));
        //Ta Daaa
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

}

