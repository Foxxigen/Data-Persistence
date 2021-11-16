using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UISettings : MonoBehaviour
{
    [SerializeField]
    Slider ballSpeedSlider;
    [SerializeField]
    Slider paddleSpeedSlider;

	private void Awake()
	{
        GameManager.Settings currentSettings = GameManager.Instance.LoadSettings();
        if (currentSettings != null)
		{
            ballSpeedSlider.SetValueWithoutNotify(currentSettings.ballSpeed);
            paddleSpeedSlider.SetValueWithoutNotify(currentSettings.paddleSpeed);
        }
        else
		{
            ballSpeedSlider.SetValueWithoutNotify(3);
            paddleSpeedSlider.SetValueWithoutNotify(2);
        }
    }


	public void SetSettingsAndExit()
	{
        GameManager.Instance.SaveSettings(ballSpeedSlider.value, paddleSpeedSlider.value);
        SceneManager.LoadScene(0);
    }
}
