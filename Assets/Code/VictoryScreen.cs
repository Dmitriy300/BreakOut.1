using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private Button _collectButton;

    private int _currentScore;

    private void Start()
    {
        _currentScore = PlayerPrefs.GetInt("CurrentScore", 0);
        _scoreText.text = _currentScore.ToString();
        _collectButton.onClick.AddListener(CollectCoins);
    }

    private void CollectCoins()
    {
        int totalCoins = PlayerPrefs.GetInt("RewardCoin", 0);
        totalCoins += _currentScore;
        PlayerPrefs.SetInt("RewardCoin", totalCoins);
        PlayerPrefs.DeleteKey("CurrentScore");
        PlayerPrefs.Save();

        SceneManager.LoadScene("SampleScene"); // Замените на вашу игровую сцену
    }
}
