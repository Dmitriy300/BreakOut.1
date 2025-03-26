using DG.Tweening;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
[System.Serializable]
public class RewardItem
{
    [Header("Components")]
    public Image Icon;
    public TMP_Text RewardText;

    [Header("Settings")]
    [SerializeField] private float _textAppearDelay = 0.2f;

    // свойство для доступа к задержке
    public float TextAppearDelay => _textAppearDelay;
}