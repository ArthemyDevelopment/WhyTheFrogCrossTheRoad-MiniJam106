using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public LevelList GoToScene;


    public void changeScene()
    {
        SceneManager.LoadScene((int)GoToScene);
    }
}
