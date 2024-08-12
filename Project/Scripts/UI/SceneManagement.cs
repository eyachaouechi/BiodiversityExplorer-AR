using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{

    public void BackToHome()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ToParc()
    {
        SceneManager.LoadScene("Parc");
    }

    public void ToFreePaint()
    {
        SceneManager.LoadScene("FreePaint");
    }


    
}
