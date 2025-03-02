using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    private bool _isLaunched = false; // ‘лаг, указывающий, запущен ли м€ч
    private Vector3 _initialPosition; // Ќачальна€ позици€ м€ча
    private Rigidbody _rigidbody;

    [SerializeField] private GameSounds _gameSounds;
    private AudioSource _audioSource;

    private void Start()
    {
        // ѕолучаем компонент Rigidbody
        _rigidbody = GetComponent<Rigidbody>();

        _audioSource = GetComponent<AudioSource>();

        if (_audioSource != null)
        {
            _audioSource.enabled = true;
        }

        // —охран€ем начальную позицию м€ча
        _initialPosition = transform.position;
    }

    private void Update()
    {
        // ≈сли м€ч не запущен, он следует за платформой
        if (!_isLaunched)
        {
            FollowPlatform();
        }

        // «апуск м€ча при нажатии на пробел
        if (Input.GetKeyDown(KeyCode.Space) && !_isLaunched)
        {
            LaunchBall();
        }
    }

    private void FollowPlatform()
    {
        // ћ€ч следует за платформой
        Transform platform = GameObject.FindGameObjectWithTag("Platform").transform;
        transform.position = new Vector3(platform.position.x, _initialPosition.y, _initialPosition.z);
    }

    private void LaunchBall()
    {
        // «адаем случайное направление м€ча
        Vector3 direction = new Vector3(Random.Range(-1f, 1f), 1, 0).normalized;

        // ѕримен€ем импульс к м€чу
        _rigidbody.velocity = direction * _speed;

        if (_gameSounds.launchSound != null)
        {
            _audioSource.PlayOneShot(_gameSounds.launchSound);
        }

        // ”станавливаем флаг запуска
        _isLaunched = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_gameSounds.bounceSound != null)
        {
            _audioSource.PlayOneShot(_gameSounds.bounceSound);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DeathZone"))
        {
            // ¬оспроизводим звук потери м€ча
            if (_gameSounds.ballLostSound != null)
            {
                _audioSource.PlayOneShot(_gameSounds.ballLostSound);
            }

            ResetBall();
        }
    }

    public void ResetBall()
    {
        // ќстанавливаем м€ч
        _rigidbody.velocity = Vector3.zero;

        // ¬озвращаем м€ч на платформу и сбрасываем флаг запуска
        transform.position = _initialPosition;
        _isLaunched = false;
    }
}
