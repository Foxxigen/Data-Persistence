using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIBestScoresScreen : MonoBehaviour
{
    [SerializeField] GameObject grid;

	[SerializeField] TextMeshProUGUI textPrefab;

	private void Start()
	{
		List<GameManager.SaveData> sd = GameManager.Instance.LoadScores();
		if (sd.Count > 0)
		{
			sd.Sort((x, y) => y.score.CompareTo(x.score));
			for (int i = 0; i < sd.Count; i++)
			{
				TextMeshProUGUI text = Instantiate(textPrefab, grid.transform);
				text.text = $"{i + 1}. {sd[i].username}: {sd[i].score}";
			}
		}
		else
		{
			TextMeshProUGUI text = Instantiate(textPrefab, grid.transform);
			text.text = "No scores yet!";
		}
	}

	public void Exit()
	{
		SceneManager.LoadScene(0);
	}
}
