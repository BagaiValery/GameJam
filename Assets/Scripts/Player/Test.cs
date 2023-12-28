using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void OnEnable()
    {
        ControlManager.OnTapOverFan += ChangeColor;
    }

    private void OnDisable()
    {
        ControlManager.OnTapOverFan -= ChangeColor;
    }

    void ChangeColor(GameObject obj)
    {
        GetComponent<Renderer>().material.color = Color.blue;
    }
}
