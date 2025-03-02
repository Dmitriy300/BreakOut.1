using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    [SerializeField] private float _lifetime = 2f; // Время жизни объекта

    void Start()
    {
        // Уничтожаем объект через указанное время
        Destroy(gameObject, _lifetime);
    }
}
