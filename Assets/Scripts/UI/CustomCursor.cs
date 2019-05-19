using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomCursor : MonoBehaviour
{
    public Image center; 
    public Image near; 
    public Image triangle;
    RectTransform rectTransform;
    public float rotateSpeed = 90;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        
       // Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        rectTransform.anchoredPosition = Input.mousePosition;
        if(Input.GetMouseButton(0))
        {
            near.transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime, Space.Self);
            triangle.transform.Rotate(Vector3.back * rotateSpeed * Time.deltaTime, Space.Self);
        }
    }
}
