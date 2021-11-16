using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using static GameManager;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class UIMenu : MonoBehaviour
{
	[SerializeField]
	TMP_InputField inputField;
	[SerializeField]
	TextMeshProUGUI bestScoreText;

	private void Awake()
	{
		inputField.onEndEdit.AddListener(SetUsername);
	}

	private void Start()
	{
		SaveData saveData = GameManager.Instance.GetHighScore();
		if (saveData != null)
		{
			bestScoreText.text = $"Best score: {saveData.username} {saveData.score}";
		} else
		{
			bestScoreText.text = "No best score yet!";
		}
	}

	public void StartGame()
	{
		SceneManager.LoadScene(1);
	}

	public void QuitGame()
	{
#if UNITY_EDITOR
		EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
	}
	public void ShowScores()
	{
		SceneManager.LoadScene(2);
	}

	public void ShowSettings()
	{
		SceneManager.LoadScene(3);
	}

	public void SetUsername(string name)
	{
		GameManager.Instance.CurrentUsername = name;
	}
}
