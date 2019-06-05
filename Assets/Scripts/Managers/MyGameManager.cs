using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MyGameManager : MonoBehaviour
{
    public Text nameText;
    public Button machineGunButton;
    public Button tankShellButton;
    public Button electricShellButton;
    public Button flameGunButton;
    // Start is called before the first frame update
    void Start()
    {
       
    }
    public void Restart()
    {
        var player = GetComponent<MyCameraControl>().player;
        player.GetComponent<TankManagerment>().Reset();

        //SceneManager.LoadScene(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
  
}
