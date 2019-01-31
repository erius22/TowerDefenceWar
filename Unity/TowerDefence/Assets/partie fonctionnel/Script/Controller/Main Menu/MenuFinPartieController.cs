using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class MenuFinPartieController : MonoBehaviour {

    public static void LoadMenuScene()
    {
        SceneManager.LoadScene("MainMenuScene");
        NetworkManager.Shutdown();
    }

    public static void LoadMenuMultijoueur()
    {
        SceneManager.LoadScene("MultiPlayer");
        NetworkManagerScript.instance.JoinGame();
    }
}
