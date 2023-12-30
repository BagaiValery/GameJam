using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{
    public void StartScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
