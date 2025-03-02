using UnityEngine;

public class BrickController : MonoBehaviour
{
    private int _health;
    [SerializeField] private GameObject _explosionEffect;
    [SerializeField] private int _maxHealth = 3;
    [SerializeField] private GameObject _platformBuffPrefab;
    [SerializeField] private float _buffDropChance = 0.2f; 
    [SerializeField] private int _maxBuffsPerLevel = 2; 
    private static int _buffsDropped = 0; 
    public GameSounds _gameSounds;

    private AudioSource _audioSource;

    public delegate void BrickDestroyed();
    public event BrickDestroyed OnBrickDestroyed;

    private void Start()
    {
        _health = _maxHealth;
               
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            _health--;

            if (_health <= 0)
            {
                if (_audioSource != null && _audioSource.enabled && _gameSounds != null && _gameSounds.brickBreakSound != null)
                {
                    _audioSource.PlayOneShot(_gameSounds.brickBreakSound);
                }

                if (_explosionEffect != null)
                {
                    Instantiate(_explosionEffect, transform.position, Quaternion.identity);
                }

                OnBrickDestroyed?.Invoke();

                if (_buffsDropped < _maxBuffsPerLevel && Random.value < _buffDropChance)
                {
                    DropBuff();
                    _buffsDropped++;
                }

                Destroy(gameObject);
            }
            else
            {
                if (_audioSource != null && _audioSource.enabled && _gameSounds != null && _gameSounds.brickBreakSound != null)
                {
                   _audioSource.PlayOneShot(_gameSounds.brickBreakSound);
                }

                ChangeColorBasedOnHealth();
            }
        }
    }
    private void ChangeColorBasedOnHealth()
    {
        // Изменяем цвет кирпича в зависимости от оставшегося здоровья
        Renderer brickRenderer = GetComponent<Renderer>();
        if (brickRenderer != null)
        {
            float healthPercentage = (float)_health / _maxHealth;
            brickRenderer.material.color = Color.Lerp(Color.red, Color.green, healthPercentage);
        }
    }

    private void DropBuff()
    {        
        if (_platformBuffPrefab != null)
        {
            Instantiate(_platformBuffPrefab, transform.position, Quaternion.identity);
        }
    }

    public static void ResetBuffsDropped()
    {        
        _buffsDropped = 0;
    }
}
