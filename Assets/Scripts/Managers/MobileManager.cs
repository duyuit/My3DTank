using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileManager : MonoBehaviour
{
    public List<GameObject> listMobileObject;
    public List<GameObject> listPCObject;
    // Start is called before the first frame update
    void Start()
    {
#if UNITY_ANDROID
        foreach (var go in listPCObject)
        {
            go.SetActive(false);
        }
#else

        foreach (var go in listMobileObject)
        {
            go.SetActive(false);
        }
#endif
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
