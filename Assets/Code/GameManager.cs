using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _brickPrefab;
    [SerializeField] private BrickRowColors _brickRowColors;
    [SerializeField] private GameSounds _gameSounds;
    [SerializeField] private float _spacingX = 1.2f;
    [SerializeField] private float _spacingY = 0.6f;
    [SerializeField] private int rows = 5;
    [SerializeField] private int columns = 8;
    

    private int _totalBricks;
    private int _destroyedBricks;

    private void Start()
    {
        BrickController.ResetBuffsDropped();

        GenerateBricks();
    }

    public void GenerateBricks()
    {
        _destroyedBricks = 0;
        _totalBricks = rows * columns;

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                // Позиция кирпича
                Vector3 position = new Vector3(col * _spacingX - (columns * _spacingX) / 2, row * _spacingY, 0);

                // Создаем кирпич
                GameObject brick = Instantiate(_brickPrefab, position, Quaternion.identity);

                // Устанавливаем цвет кирпича в зависимости от ряда
                if (row < _brickRowColors.rowColors.Length)
                {
                    Renderer brickRenderer = brick.GetComponent<Renderer>();
                    if (brickRenderer != null)
                    {
                        brickRenderer.material.color = _brickRowColors.rowColors[row];
                    }
                }

                BrickController brickController = brick.GetComponent<BrickController>();
                if (brickController != null)
                {
                    brickController._gameSounds = _gameSounds;
                    brickController.OnBrickDestroyed += HandleBrickDestroyed;
                }
            }
        }


    }

    private void HandleBrickDestroyed()
    {
        _destroyedBricks++; 

       
        if (_destroyedBricks >= _totalBricks)
        {
            RestartLevel();
        }
    }

    void RestartLevel()
    {
        Debug.Log("Все кирпичи уничтожены! Перезапуск уровня...");

       
        foreach (var brick in GameObject.FindGameObjectsWithTag("Brick"))
        {
            Destroy(brick);
        }

        GenerateBricks();

        BallController ball = FindObjectOfType<BallController>();
        if (ball != null)
        {
            ball.ResetBall();
        }
    }
}

