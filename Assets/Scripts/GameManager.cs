using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public TMP_InputField userNameInput;
    public TextMeshProUGUI bestScore;

    private string userName;
    private string highScoreUser;
    private int userScore = 0;

    private void Start()
    {
        LoadScore();
        if (userScore != 0)
        {
            bestScore.text = "Best Score : " + UpdateHighScoreText();
        }
        userNameInput.text = userName;
    }

    public void SetUserName()
    {
        userName = userNameInput.text;
        Debug.Log(userName);
    }

    public void StartGame()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        SceneManager.LoadScene(1);

    }

    public void SetScore(int score)
    {
        userScore = score;
    }

    public int GetScore()
    {
        return userScore;
    }

    public void UpdateUser()
    {
        highScoreUser = userName;
    }

    public string UpdateHighScoreText()
    {
        return highScoreUser + " : " + userScore;
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
    Application.Quit();
#endif
    }

    [System.Serializable]
    class User
    {
        public string name;
        public int score;
    }

    public void SaveScore()
    {
        User user = new User();
        user.name = highScoreUser;
        user.score = userScore;

        string json = JsonUtility.ToJson(user);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            User user = JsonUtility.FromJson<User>(json);

            highScoreUser = user.name;
            userScore = user.score;
        }
    }
}
