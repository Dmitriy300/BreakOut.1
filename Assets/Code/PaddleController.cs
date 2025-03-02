using UnityEngine;

public class PaddleController : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float maxX;
    [SerializeField] private float minX;
    
    private Vector3 _originalScale; 
    private float _buffEndTime; 
    private void Start()
    {
        _originalScale = transform.localScale;
    }
    void Update()
    {
        float move = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        
        Vector3 newPosition = transform.position + Vector3.right * move;
        
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        transform.position = newPosition;

        if (Time.time > _buffEndTime)
        {            
            transform.localScale = _originalScale;
        }
    }
    public void ApplyBuff(float sizeIncrease, float duration)
    {
        transform.localScale = new Vector3(_originalScale.x * sizeIncrease, _originalScale.y, _originalScale.z);

        _buffEndTime = Time.time + duration;
    }
}

