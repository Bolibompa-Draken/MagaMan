using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.ComponentModel;
using System;



public class SceneManager : MonoBehaviour
{
    public string SceneName;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeScene()
    {
        SceneManager.LoadScene(SceneName);
    }

    private static void LoadScene(string sceneName)
    {
        throw new NotImplementedException();
    }
}
