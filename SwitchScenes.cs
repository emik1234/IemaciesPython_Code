using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScenes : MonoBehaviour
{
    public void TurnOff()
    {
        SceneManager.LoadScene("TurnedOff");
    }

    public void TurnOn()
    {
        SceneManager.LoadScene("PC");
    }

}
