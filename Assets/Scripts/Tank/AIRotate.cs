using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIRotate : MonoBehaviour
{
   Transform player;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if(player == null)
        {
            player = GameObject.Find("Player").transform;
        }
        CheckAndRotate();
    
    }
    public void CheckAndRotate()
    {
     
        float angle = AngleBetweenTwoPoints(Camera.main.WorldToViewportPoint(transform.position), Camera.main.WorldToViewportPoint(player.transform.position));

        //Ta Daaa
        transform.rotation = Quaternion.Euler(new Vector3(0f, -angle - 90, 0f));
    }
    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

}
