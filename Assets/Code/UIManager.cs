using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System.Text;

public class UIManager : MonoBehaviour
{
    [Header("Text Elements")]
    [SerializeField] private TextMeshProUGUI _livesText;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _blocksText;

    [Header("Game Over UI")]
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _quitButton;
    [SerializeField] private TMP_Text _winText;
    [SerializeField] private TMP_Text _loseText;

    private int _currentScore;
    private int _currentLives;
    private int _currentBlocks;
    private int _score;

    private string _livesFormat;
    private string _scoreFormat;
    private string _blocksFormat;

    private readonly StringBuilder _sb = new StringBuilder(32);

    private void Start()
    {
        _livesFormat = _livesText.text;
        _scoreFormat = _scoreText.text;
        _blocksFormat = _blocksText.text;

        _restartButton.onClick.AddListener(RestartGame);
        _quitButton.onClick.AddListener(QuitGame);
        _gameOverPanel.SetActive(false);
        _winText.gameObject.SetActive(false);
        _loseText.gameObject.SetActive(false);

        UpdateAllUI();
    }

    public void Initialize(int startLives, int totalBlocks)
    {
        _currentLives = startLives;
        _currentBlocks = totalBlocks;
        _currentScore = 0;
        UpdateAllUI();
    }

    public void UpdateLives(int lives)
    {
        if (_currentLives == lives) return;

        _currentLives = lives;
        _livesText.text = string.Format(_livesFormat, _currentLives);

        if (_currentLives <= 0) ShowGameOver();
    }

    public void AddScore(int points)
    {
        if (points == 0) return;

        _currentScore += points;
        _scoreText.text = string.Format(_scoreFormat, _currentScore);
    }

    public void UpdateBlocks(int blocks)
    {
        if (_currentBlocks == blocks) return;

        _currentBlocks = blocks;
        _blocksText.text = string.Format(_blocksFormat, _currentBlocks);

        if (_currentBlocks <= 0) ShowLevelComplete();
    }

    private void UpdateAllUI()
    {
        _livesText.text = string.Format(_livesFormat, _currentLives);
        _scoreText.text = string.Format(_scoreFormat, _currentScore);
        _blocksText.text = string.Format(_blocksFormat, _currentBlocks);
    }

    //private void UpdateText(TextMeshProUGUI textField, string prefix, int value)
    //{
    //    _sb.Clear();
    //    _sb.Append(prefix).Append(value);
    //    textField.SetText(_sb);
    //}

    public void ShowGameOver()
    {
        SetGameOverState(false);
    }

    private void ShowLevelComplete()
    {
        SetGameOverState(true);
    }

    private void SetGameOverState(bool isWin)
    {
        Time.timeScale = 0f;
        _gameOverPanel.SetActive(true);
        _winText.gameObject.SetActive(isWin);
        _loseText.gameObject.SetActive(!isWin);

    }

    public void HideGameOver() 
    {   
        _gameOverPanel.SetActive(false);
        _winText.gameObject.SetActive(false);
        _loseText.gameObject.SetActive(false);
    }

    private void RestartGame()
    {
        HideGameOver();
        GameManager.Instance.RestartGame();

    }

    private void OnDestroy()
    {
        _restartButton.onClick.RemoveListener(RestartGame);
        _quitButton.onClick.RemoveListener(QuitGame);
    }

    public int GetCurrentScore()
    {
        return _score;
    }

    private void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}
