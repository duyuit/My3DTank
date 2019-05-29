using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject uiParent;
    private CustomNetworkManager networkManager;
    private CustomNetworkDiscovery networkDiscovery;
    // Start is called before the first frame update
    void Start()
    {
        networkDiscovery = CustomNetworkDiscovery.Instance;
        networkManager = CustomNetworkManager.Instance;
    }
    public void StartHost()
    {
        networkManager.StartHost();
        networkDiscovery.StartAsServer();
        DisableButton();

    }
    public void StartClient()
    {
        networkDiscovery.StartAsClient();
        DisableButton();
    }
    void DisableButton()
    {
        uiParent.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
