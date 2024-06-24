using UniRx;
using UnityEngine;
using DG.Tweening;
using System.Collections;
using UnityEngine.UI;

public class PlayerSize : MonoBehaviour
{
    private CompositeDisposable subscriptions = new CompositeDisposable();
    [SerializeField] private Slider playerSizeSlider;
    [SerializeField] private Text statusText;
    [SerializeField] private Text countValue;
    [SerializeField] private Color poorColor = new Color(1, 0.6f, 0); 
    [SerializeField] private Color wealthyColor = new Color(1, 1, 0); 
    [SerializeField] private Color richColor = new Color(0, 1, 0);
    [SerializeField] private Transform playerTransform;

    private void OnEnable()
    {
        StartCoroutine(Subscribe());
    }

    private IEnumerator Subscribe()
    {
        yield return new WaitUntil(() => GameEvents.instance != null);

        GameEvents.instance.playerSize.ObserveEveryValueChanged(x => x.Value)
            .Subscribe(value =>
            {
                var sliderValue = (value - 1) / 99f;
                playerSizeSlider.value = sliderValue;
                countValue.text = "Уровень 1\nМонет: " + value.ToString();

                if (sliderValue <= 0.25f)
                {
                    statusText.text = "Бедный";
                    SetColors(poorColor);
                }
                else if (sliderValue <= 0.75f)
                {
                    statusText.text = "Состоятельный";
                    SetColors(wealthyColor);
                }
                else
                {
                    statusText.text = "Богатый";
                    SetColors(richColor);
                }
                statusText.font.material.SetFloat("_OutlineWidth", 1);
                statusText.font.material.SetColor("_OutlineColor", Color.white);
            })
            .AddTo(subscriptions);

        GameEvents.instance.gameWon.ObserveEveryValueChanged(x => x.Value)
            .Subscribe(value =>
            {
                if (value)
                    statusText.transform.parent.DOScale(Vector3.zero, 0.25f);
            })
            .AddTo(subscriptions);

        GameEvents.instance.gameLost.ObserveEveryValueChanged(x => x.Value)
            .Subscribe(value =>
            {
                if (value)
                    statusText.transform.parent.DOScale(Vector3.zero, 0.25f);
            })
            .AddTo(subscriptions);
    }

    private void SetColors(Color color)
    {
        playerSizeSlider.fillRect.GetComponent<Image>().color = color;
        statusText.color = color;
    }

    private void OnDisable()
    {
        subscriptions.Clear();
    }
}