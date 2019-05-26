using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropLandMine : MonoBehaviour
{
    public GameObject landMinePrefab;
    float lastDrop = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Drop()
    {
        if(Time.time - lastDrop > 1)
        {
            Vector3 position = transform.position - transform.forward * 2;
            position.y = 1;
            GameObject mine = Instantiate(landMinePrefab, position, Quaternion.identity);
            Vector3 temp = -transform.forward * 2; ;
            if(Input.GetAxis("Vertical1") < 0)
            {
                temp = -transform.forward *15;
            }
            temp.y = 5;
            mine.GetComponent<Rigidbody>().velocity = temp;

            lastDrop = Time.time;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            Drop();
        }
    }
}
