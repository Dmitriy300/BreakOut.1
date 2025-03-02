using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBaffController : MonoBehaviour
{
    [SerializeField] private float _platformSizeIncrease = 1.5f; // �� ������� ��������� ���������
    [SerializeField] private float _buffDuration = 10f; // ������������ �����
    [SerializeField] private GameSounds _gameSounds;
    [SerializeField] private float _soundVolume;

    public void OnTriggerEnter(Collider other)
    {
        // ���������, ��� ���� ��������� ���������
        if (other.CompareTag("Platform"))
        {
            // �������� ��������� ���������
            PaddleController platform = other.GetComponent<PaddleController>();
            if (platform != null)
            {
                // ��������� ����
                platform.ApplyBuff(_platformSizeIncrease, _buffDuration);

                // ������������� ���� ���������� �����
                if (_gameSounds != null && _gameSounds.buffApplySound != null)
                {
                    AudioSource.PlayClipAtPoint(_gameSounds.buffApplySound, Camera.main.transform.position, _soundVolume);
                    Debug.Log("���� ���������� ����� �������������"); // �������� ��������������� �����
                }
                else
                {
                    Debug.LogError("���� ���������� ����� �� ��������!"); // �������� ������
                }
            }

            // ���������� ���� ����� �������
            Destroy(gameObject);
        }
    }
}
