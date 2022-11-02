using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleButton : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isRight;
    void Start()
    {
        float offSet = 0;
        if (isRight)
            offSet = Screen.width / 2;
        float width = Screen.width;
        float height = Screen.height;
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(width / 2, height);
        gameObject.GetComponent<RectTransform>().position = new Vector3(width / 4 + offSet, height / 2, 0);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
