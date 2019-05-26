using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponChoose : MonoBehaviour
{
    public List<Image> listImage;
    Image currentImage;

    Color blackColor;
    Color whiteColor;
    // Start is called before the first frame update
    void Start()
    {
        currentImage = listImage[0];
        blackColor = new Color(0.5f,0.5f, 0.5f);
        whiteColor = new Color(1, 1, 1);

        currentImage.color = whiteColor;
    }
    public void OnWeaponChoose(Image imageWeapon)
    {
        currentImage.color = blackColor;
        currentImage = imageWeapon;
        currentImage.color =  whiteColor;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
