using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChan : MonoBehaviour
{

    public string sc;


    public void change()
    {
        SceneManager.LoadScene(sc);

    }

    public void change(string sc)
    {
        SceneManager.LoadScene(sc);

    }
    public static void changeStatic(string sc)
    {
        SceneManager.LoadScene(sc);
    }
    
    public void reloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
