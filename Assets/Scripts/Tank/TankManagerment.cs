using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;
using UnityEngine.UI;

public class TankManagerment : NetworkBehaviour
{
    public int playerID;
    public BulletInfomation bulletInfomation;
    [HideInInspector]
    public BulletInfo currentBullet;
    public AudioSource fireAudioSource;
    public Transform m_FireTransform;
    public GameObject m_Turret;
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
        if (!isLocalPlayer)
        {
            canFire = false;
            return;
        }
#if UNITY_ANDROID
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
#endif


#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        if (Input.GetMouseButton(0))
        {
            canFire = true;
            firePosition = Input.mousePosition;
            GameGlobal.Instance.lastFirePosition = Input.mousePosition;
        }
        else canFire = false;
#endif

    }
    public void Reset()
    {
        if (isServer)
            RpcReset();
        else
        {
            CmdReset();
        }
    }
    [Command]
    void CmdReset()
    {
        RpcReset();
    }
    [ClientRpc]
    void RpcReset()
    {
        canFire = false;
        gameObject.SetActive(true);
        gameObject.GetComponent<TankHealth>().SetHealthUI(100);
        gameObject.GetComponent<TankHealth>().m_Dead = false;
        transform.position = new Vector3(Random.Range(1, 40), 0, Random.Range(1, 40));
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
    [Command]
    public void CmdBulletSelect(int typeID)
    {
        RpcBulletSelect(typeID);
    }

    [ClientRpc]
    public void RpcBulletSelect(int typeID)
    {
        foreach (var bullet in bulletInfomation.listBullet)
        {
            if (bullet.type == (BulletType)typeID)
            {
                currentBullet = bullet;
                UpdateAudioClip();
                ActiveComponent(currentBullet.type);
                Debug.LogError("he");
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
    public override void OnStartLocalPlayer()
    {
        var camera = Camera.main;
        camera.GetComponent<MyCameraControl>().SetPlayer(gameObject.transform);
        var managerComponent = camera.GetComponent<MyGameManager>();
        managerComponent.tankShellButton.onClick.AddListener(() => CmdBulletSelect(0));
        managerComponent.machineGunButton.onClick.AddListener(() => CmdBulletSelect(1));
        managerComponent.electricShellButton.onClick.AddListener(() => CmdBulletSelect(2));
        managerComponent.flameGunButton.onClick.AddListener(() => CmdBulletSelect(3));
        var listAnotherTank = GameObject.FindGameObjectsWithTag("Tank");
        var nameText = GetComponentInChildren<Text>();
        nameText.color = new Color(0.54f, 1f, 0.517f);

        var name = managerComponent.nameText.text;
        if (name == "")
        {
            name = "Player " + netId;
        }
        CmdSetMyName(name);
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        OnSyncMyNameHook(MyName);
    }
    [SyncVar(hook = "OnSyncMyNameHook")]
    public string MyName="";

    void OnSyncMyNameHook(string value)
    {
        // make sure we save the value. On remove clients if we have a hook the value isn't set for us!
        MyName = value;
        GetComponentInChildren<Text>().text = value;
    }

    [Command]
    void CmdSetMyName(string name)
    {
        MyName = name;
    }
}

   

