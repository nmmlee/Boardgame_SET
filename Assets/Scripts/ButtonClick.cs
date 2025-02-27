using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ButtonClick : MonoBehaviour
{
    public static event Action<GameObject> onButtonClicked;

    public void HandleClick()
    {
        Transform border = transform.Find("border");

        if (border != null)
        {
            border.gameObject.SetActive(true);
        }

        onButtonClicked?.Invoke(gameObject);
    }
}
