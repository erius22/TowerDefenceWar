using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public static class MainMenuController {

	public static void LoadPlayScene()
	{
		SceneManager.LoadScene("FieldScene");
	}

	public static void LoadMultiPlayerScene()
	{
		SceneManager.LoadScene("MultiPlayer");
	}

	public static void LoadOptionScene()
	{
		SceneManager.LoadScene("Option");
	}

	public static void QuitScene()
	{
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
	}

}
