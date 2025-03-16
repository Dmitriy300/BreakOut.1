using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private GameSounds _gameSounds;
    
    private bool _isLaunched = false; 
    private Vector3 _initialPosition; 
    private Rigidbody _rigidbody;
    private AudioSource _audioSource;
    private GameManager _gameManager;
    private bool _firstLaunch = true;
    
    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();

        _rigidbody = GetComponent<Rigidbody>();

        _audioSource = GetComponent<AudioSource>();

        if (_audioSource != null)
        {
            _audioSource.enabled = true;
        }
               
        _initialPosition = transform.position;
    }

    private void Update()
    {

        if (!GameManager.Instance.IsGameActive) return;

        if (!_isLaunched && _firstLaunch)
        {
            FollowPlatform();

           
            if (Input.GetKeyDown(KeyCode.Space))
            {
                LaunchBall();
                _firstLaunch = false;
            }
        }
    }

    public void Initialize(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    private void FollowPlatform()
    {
        Transform platform = GameObject.FindGameObjectWithTag("Platform").transform;
        transform.position = new Vector3(platform.position.x, _initialPosition.y, _initialPosition.z);
    }

    private void LaunchBall()
    {
       
        Vector3 direction = new Vector3(Random.Range(-1f, 1f), 1, 0).normalized;

        _rigidbody.velocity = direction * _speed;

        if (_gameSounds.launchSound != null)
        {
            _audioSource.PlayOneShot(_gameSounds.launchSound);
        }
                
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
            if (_gameSounds.ballLostSound != null)
            {
                _audioSource.PlayOneShot(_gameSounds.ballLostSound);
            }
            _gameManager.LoseLife();
            ResetBall();
        }
    }

    public void ResetBall()
    {
        _firstLaunch = true;
        _isLaunched = false;
        _rigidbody.velocity = Vector3.zero;
        transform.position = _initialPosition;
        _isLaunched = false;
    }

    
}
