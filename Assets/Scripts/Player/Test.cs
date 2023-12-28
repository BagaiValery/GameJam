using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void OnEnable()
    {
        ControlManager.OnTapOverFan += ChangeColorInObject;
    }

    private void OnDisable()
    {
        ControlManager.OnTapOverFan -= ChangeColorInObject;
    }

    void ChangeColorInObject(GameObject obj)
    {
        TestColorChange test = obj.GetComponent<TestColorChange>();
        if (test != null)
        {
            test.ChangeColor();
        }
    }
}
