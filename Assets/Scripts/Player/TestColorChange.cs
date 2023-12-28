using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestColorChange : MonoBehaviour
{
    public void ChangeColor()
    {
        GetComponent<Renderer>().material.color = Color.blue;
    }
}
