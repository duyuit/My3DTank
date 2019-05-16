using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingButton : MonoBehaviour
{
    public List<Button> listButton;

    private List<Vector2> listStartPositionButton = new List<Vector2>();
    private bool isShow = true;
    private RectTransform rectTransform;
    private Image currentImage;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        currentImage = GetComponent<Image>();
        rectTransform.SetSiblingIndex(10);
        foreach (var button in listButton)
        {
            button.GetComponent<RectTransform>().SetSiblingIndex(0);
            listStartPositionButton.Add(button.GetComponent<RectTransform>().anchoredPosition);
        }
        OnClick();
    }

    // Update is called once per frame
    float step = 0;
    public void ResetStep()
    {
        step = 0;
        var rt = listButton[0].GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector2(67f, 63f);
    }
    void ShowChildrenButton(bool isShow)
    {
        foreach (var button in listButton)
        {
            button.gameObject.SetActive(isShow);
        }
    }
    public void OnBulletButtonSelectedClick(Image image)
    {
        currentImage.sprite = image.sprite;
        OnClick();
    }
    public void OnClick()
    {
        if (isShow)
        {
            isShow = false;
            for (int i = 0; i < listButton.Count; i++)
            {
                var rectTrans = listButton[i].GetComponent<RectTransform>();
                Move(rectTrans, rectTransform.anchoredPosition,isShow);
            }
        }
        else
        {
            isShow = true;
            ShowChildrenButton(isShow);
            for (int i = 0; i < listButton.Count; i++)
            {
                var rectTrans = listButton[i].GetComponent<RectTransform>();
                rectTrans.anchoredPosition = rectTransform.anchoredPosition;
                Move(rectTrans, listStartPositionButton[i],isShow);
            }

        }
    }
    void Update()
    {
    }

    void Move(RectTransform panel, Vector2 destination,bool isShow)
    {
        StartCoroutine(MoveIEnumerator(panel, destination));
    }

    IEnumerator MoveIEnumerator(RectTransform rt, Vector2 destination)
    {
        float step = 0;
        Color color = rt.GetComponent<Image>().color;
        while (step < 1)
        {
            float x = Mathf.Lerp(rt.anchoredPosition.x, destination.x, step += Time.deltaTime / 1);
            float y = Mathf.Lerp(rt.anchoredPosition.y, destination.y, step += Time.deltaTime / 1);

            if (isShow)
            {
                color.a = step;
                rt.GetComponent<Image>().color = color;
            }
            else
            {
                color.a = 1 - step;
                rt.GetComponent<Image>().color = color;
            }

            rt.anchoredPosition = new Vector2(x, y);
            yield return 1f;
        }
        ShowChildrenButton(isShow);

    }

}
