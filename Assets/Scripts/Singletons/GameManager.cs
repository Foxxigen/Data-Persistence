using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Serializable]
    public class SaveData
    {
        public string username;
        public int score;
    }

    [Serializable]
    public class Settings
    {
        public float paddleSpeed;
        public float ballSpeed;
    }


    public static GameManager Instance;

    public string CurrentUsername { get { return username; } set { OnUsernameSet(value); } }

    private string username;

    void Awake()
    {
        if (Instance != null)
		{
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void OnUsernameSet(string value)
	{
        username = value;
    }

    public void SaveSettings(float maxBallSpeed, float paddleSpeed)
	{
        Settings currentSettings = LoadSettings();
        if (currentSettings != null)
		{
            currentSettings.ballSpeed = maxBallSpeed;
            currentSettings.paddleSpeed = paddleSpeed;
        }
        else
		{
            currentSettings = new Settings();
            currentSettings.ballSpeed = maxBallSpeed;
            currentSettings.paddleSpeed = paddleSpeed;
        }

        string json = JsonUtility.ToJson(currentSettings, true);
        File.WriteAllText(Application.persistentDataPath + "/settings.json", json);
    }

    public Settings LoadSettings()
	{
        string path = Application.persistentDataPath + "/settings.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            Settings settings = JsonUtility.FromJson<Settings>(json);
            return settings;
        }
        return null;
    }

    public void SaveScore(int score)
	{
        List<SaveData> currentScores = LoadScores();
        SaveData existingUsername = currentScores.Find(x => x.username == CurrentUsername);
        if (currentScores.Count > 0 && existingUsername != null)
        {
            if (existingUsername.score < score)
            {
                existingUsername.score = score;
            }
		}
        else
		{
            SaveData saveData = new SaveData();
            saveData.username = CurrentUsername;
            saveData.score = score;
            currentScores.Add(saveData);
        }


		string json = JsonHelper.ToJson(currentScores.ToArray(), true);
        File.WriteAllText(Application.persistentDataPath + "/scores.json", json);
    }

    public List<SaveData> LoadScores()
	{
        string path = Application.persistentDataPath + "/scores.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData[] arrayData = JsonHelper.FromJson<SaveData>(json);
            if (arrayData != null)
            {
                List<SaveData> data = new List<SaveData>(arrayData);
                return data;
            }  
        }

        return new List<SaveData>();
    }

    public SaveData GetHighScore()
	{
        List<SaveData> currentScores = LoadScores();
        if (currentScores.Count > 0)
        {
            SaveData saveDataToReturn = currentScores[0];
            foreach (SaveData currentSaveData in currentScores)
            {
                if (currentSaveData.score > saveDataToReturn.score)
                {
                    saveDataToReturn = currentSaveData;
                }
            }
            return saveDataToReturn;
        }
        return null;
    }


    // ------------ HELPERS FUNCTIONS ---------------

    public static class JsonHelper
    {
        public static T[] FromJson<T>(string json)
        {
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.Items;
        }

        public static string ToJson<T>(T[] array)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper);
        }

        public static string ToJson<T>(T[] array, bool prettyPrint)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper, prettyPrint);
        }

        [Serializable]
        private class Wrapper<T>
        {
            public T[] Items;
        }
    }
}
