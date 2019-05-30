using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ShellSyncControl : NetworkBehaviour
{
    [SyncVar] Vector3 Syncvelocity;
    Rigidbody rigidbody;
    public float lerpRate = 15;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        TranmitPosition();
        LerpPosition();
    }
    void LerpPosition()
    {
        if (!isLocalPlayer)
        {
            rigidbody.velocity = Vector3.Lerp(rigidbody.velocity, Syncvelocity, Time.deltaTime * lerpRate);
        }
    }

    [Command]
    void CmdProvideRotationToServer(Vector3 velo)
    {
        Syncvelocity = velo;
    }
    [Client]
    void TranmitPosition()
    {
        if (isLocalPlayer)
        {
            CmdProvideRotationToServer(rigidbody.velocity);
        }
    }
}
