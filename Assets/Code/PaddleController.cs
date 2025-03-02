using UnityEngine;

public class PaddleController : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float maxX;
    [SerializeField] private float minX;
    
    private Vector3 _originalScale; // Исходный размер платформы
    private float _buffEndTime; // Время окончания баффа
    private void Start()
    {
        _originalScale = transform.localScale;
    }
    void Update()
    {
        float move = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        
        Vector3 newPosition = transform.position + Vector3.right * move;
        
        // Ограничиваем движение платформы в пределах экрана
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        transform.position = newPosition;

        // Проверяем, истек ли бафф
        if (Time.time > _buffEndTime)
        {
            // Возвращаем платформе исходный размер
            transform.localScale = _originalScale;
        }
    }
    public void ApplyBuff(float sizeIncrease, float duration)
    {
        // Увеличиваем размер платформы
        transform.localScale = new Vector3(_originalScale.x * sizeIncrease, _originalScale.y, _originalScale.z);

        // Устанавливаем время окончания баффа
        _buffEndTime = Time.time + duration;
    }
}

