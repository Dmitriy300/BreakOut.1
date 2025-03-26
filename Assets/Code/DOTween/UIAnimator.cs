using DG.Tweening;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class UIAnimator : MonoBehaviour
{
    [Header("�������� ��������")]
    [SerializeField] private CanvasGroup _mainCanvas;
    [SerializeField] private RectTransform _title;
    [SerializeField] private RewardItem[] _rewards;
    [SerializeField] private Button _collectButton;
    [SerializeField] private Button _adButton;

    [Header("��������� ��������")]
    [SerializeField] private float _fadeDuration = 0.5f;
    [SerializeField] private Ease _easeType = Ease.OutBack;

    private Vector2 _titleStartPos;

    private void Start()
    {
        InitializeElements();
        StartScreenAnimation();
    }

    private void InitializeElements()
    {
        _titleStartPos = _title.anchoredPosition;
        _mainCanvas.alpha = 0;

        // ��������� ���������
        _title.anchoredPosition += Vector2.up * 200;
        CanvasGroup titleCanvas = _title.GetComponent<CanvasGroup>();
        if (titleCanvas != null) titleCanvas.alpha = 0;

        // ��������� ������
        foreach (var button in new[] { _collectButton, _adButton })
        {
            button.transform.localScale = Vector3.zero;
            CanvasGroup cg = button.GetComponent<CanvasGroup>();
            if (cg != null) cg.alpha = 0;
        }

        // ��������� ������
        foreach (var reward in _rewards)
        {
            reward.Icon.transform.localScale = Vector3.zero;
            reward.RewardText.alpha = 0;
        }
    }

    private void StartScreenAnimation()
    {
        Sequence mainSequence = DOTween.Sequence();

        // 1. ��������� ��������� ������
        mainSequence.Append(_mainCanvas.DOFade(1, _fadeDuration));

        // 2. �������� ���������
        CanvasGroup titleCanvas = _title.GetComponent<CanvasGroup>();
        mainSequence.Append(_title.DOAnchorPosY(_titleStartPos.y, _fadeDuration).SetEase(_easeType));
        if (titleCanvas != null)
            mainSequence.Join(titleCanvas.DOFade(1, _fadeDuration));

        // 3. �������� ������
        foreach (var reward in _rewards)
        {
            // �������� ������ � �������� ������
            mainSequence.Append(
                reward.Icon.transform.DOScale(1, 0.5f)
                    .SetEase(Ease.OutBounce) // �������� �� ����� "��������" ������
                    .OnStart(() => reward.Icon.transform.localScale = Vector3.zero)
            );

            // �������� ������
            Sequence textSequence = DOTween.Sequence();
            textSequence.Append(reward.RewardText.DOFade(1, 0.3f).SetDelay(reward.TextAppearDelay)); // ���������� ��������� ��������
            textSequence.Join(reward.RewardText.transform.DOScale(1, 0.3f)
                .SetEase(Ease.OutBack)
                .From(1.3f) // ��������� ������� ��� ������� "������"
            );

            mainSequence.Join(textSequence);
        }

        // 4. �������� ������
        mainSequence.AppendCallback(() =>
        {
            AnimateButton(_collectButton);
            AnimateButton(_adButton);
        });

        _collectButton.onClick.AddListener(HideScreen);
        _adButton.onClick.AddListener(() => Debug.Log("����� �������..."));
    }

    private void AnimateButton(Button button)
    {
        button.transform.DOScale(1, _fadeDuration).SetEase(_easeType);
        CanvasGroup cg = button.GetComponent<CanvasGroup>();
        if (cg != null) cg.DOFade(1, _fadeDuration);
    }

    private void HideScreen()
    {
        _mainCanvas.DOFade(0, _fadeDuration)
            .OnComplete(() => gameObject.SetActive(false));
    }
}






