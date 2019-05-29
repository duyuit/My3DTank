using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CustomNetworkDiscovery : NetworkDiscovery
{
    private static CustomNetworkDiscovery instace;
    public static CustomNetworkDiscovery Instance
    {
        get
        {
            if (instace == null)
                instace = FindObjectOfType<CustomNetworkDiscovery>();
            return instace;
        }
    }
    void Start()
    {
        broadcastData = CustomNetworkManager.Instance.GenerateNetworkBroadcastData();
        Initialize();
    }
    public override void OnReceivedBroadcast(string fromAddress, string data)
    {
        // Debug.LogError("Receive broadcast from address" + fromAddress + " ,data: " + data);
        string[] items = data.Split(':');
        if (items.Length == 2 && items[0] == "ConnectionBroadcastMessage")
        {
            if(CustomNetworkManager.Instance != null && CustomNetworkManager.Instance.client == null)
            {
                Debug.LogError("Attempting to connect to: " + fromAddress);
                CustomNetworkManager.Instance.networkAddress = fromAddress;
                CustomNetworkManager.Instance.networkPort = int.Parse(items[1]);
                CustomNetworkManager.Instance.StartClient(); 
            }
        }
    }
}
