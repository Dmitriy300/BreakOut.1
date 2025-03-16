using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _brickPrefab;
    [SerializeField] private BrickRowColors _brickRowColors;
    [SerializeField] private GameSounds _gameSounds;
    [SerializeField] private float _spacingX = 1.2f;
    [SerializeField] private float _spacingY = 0.6f;
    [SerializeField] private int _rows = 5;
    [SerializeField] private int _columns = 8;
    [SerializeField] private int _startLives = 3;
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private BallController _ballController;

    
    private int _totalBricks;
    private int _destroyedBricks;
    private int _currentLives;

    private List<GameObject> _bricks = new List<GameObject>();
    public int TotalBricks => _bricks.Count;
    public int DestroyedBricks => _destroyedBricks;
    public UIManager UIManager => _uiManager;

    public static GameManager Instance { get; private set; }
    public bool IsGameActive { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        IsGameActive = true;
        BrickController.ResetBuffsDropped();
        GenerateBricks();
        InitializeGame();
    }

    private void InitializeGame()
    {
        _currentLives = _startLives;
        _uiManager.Initialize(_startLives, _bricks.Count);
        _ballController.Initialize(this);
    }

    public void LoseLife()
    {
        if (--_currentLives <= 0) GameOver();
        _uiManager.UpdateLives(_currentLives);
    }

    private void GameOver() => _uiManager.ShowGameOver();

    public void GenerateBricks()
    {
        ClearBricks();
        _totalBricks = _rows * _columns;
        var halfWidth = (_columns * _spacingX) * 0.5f;

        for (int row = 0; row < _rows; row++)
        {
            var yPos = row * _spacingY;
            var rowColor = GetRowColor(row);

            for (int col = 0; col < _columns; col++)
            {
                var xPos = col * _spacingX - halfWidth;
                CreateBrick(new Vector3(xPos, yPos, 0), rowColor);
            }
        }
    }

    private Color GetRowColor(int row)
    {
        return row < _brickRowColors.rowColors.Length
            ? _brickRowColors.rowColors[row]
            : Color.white;
    }

    private void CreateBrick(Vector3 position, Color color)
    {
        var brick = Instantiate(_brickPrefab, position, Quaternion.identity);
        if (brick.TryGetComponent<Renderer>(out var renderer))
            renderer.material.color = color;

        if (brick.TryGetComponent<BrickController>(out var controller))
        {
            controller._gameSounds = _gameSounds;
            controller.OnBrickDestroyed += HandleBrickDestroyed;
        }

        _bricks.Add(brick);
    }

    private void HandleBrickDestroyed()
    {
        _uiManager.AddScore(100);
        _uiManager.UpdateBlocks(--_totalBricks);

        if (++_destroyedBricks >= _bricks.Count)
            RestartGame();
    }

    public void RestartGame()
    {
        IsGameActive = true;
        Time.timeScale = 1f;
        _ballController.ResetBall();
        _currentLives = _startLives;
        _destroyedBricks = 0;
        ClearBricks();
        GenerateBricks();
        _ballController.ResetBall();
        GenerateBricks();

        _uiManager.Initialize(_startLives, _bricks.Count);
        _uiManager.HideGameOver();
    }

    private void ClearBricks()
    {
        foreach (var brick in _bricks)
            if (brick != null) Destroy(brick);

        _bricks.Clear();
    }
}

