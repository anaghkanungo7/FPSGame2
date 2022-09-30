using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class StartGame : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None; 
    }

    public void GameStart()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void GameHome()
    {
        SceneManager.LoadScene("StartPage");
    }

    public void Quit()
    {
        Application.Quit();
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }
}