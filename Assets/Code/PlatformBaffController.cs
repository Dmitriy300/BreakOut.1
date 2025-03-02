using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBaffController : MonoBehaviour
{
    [SerializeField] private float _platformSizeIncrease = 1.5f; // На сколько увеличить платформу
    [SerializeField] private float _buffDuration = 10f; // Длительность баффа
    [SerializeField] private GameSounds _gameSounds;
    [SerializeField] private float _soundVolume;

    public void OnTriggerEnter(Collider other)
    {
        // Проверяем, что бафф подобрала платформа
        if (other.CompareTag("Platform"))
        {
            // Получаем компонент платформы
            PaddleController platform = other.GetComponent<PaddleController>();
            if (platform != null)
            {
                // Применяем бафф
                platform.ApplyBuff(_platformSizeIncrease, _buffDuration);

                // Воспроизводим звук применения баффа
                if (_gameSounds != null && _gameSounds.buffApplySound != null)
                {
                    AudioSource.PlayClipAtPoint(_gameSounds.buffApplySound, Camera.main.transform.position, _soundVolume);
                    Debug.Log("Звук применения баффа воспроизведен"); // Логируем воспроизведение звука
                }
                else
                {
                    Debug.LogError("Звук применения баффа не назначен!"); // Логируем ошибку
                }
            }

            // Уничтожаем бафф после подбора
            Destroy(gameObject);
        }
    }
}
