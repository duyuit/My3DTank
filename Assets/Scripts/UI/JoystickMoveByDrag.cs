using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickMoveByDrag : MonoBehaviour
{
    RectTransform rectTransform;


    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        GameGlobal.Instance.joystickMoveByDrag = gameObject;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rectTransform.anchoredPosition = GameGlobal.Instance.lastJoystickPosition;
    }
}
