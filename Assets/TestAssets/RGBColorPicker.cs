using UnityEngine;
using UnityEngine.UI;

public class RGBColorPicker : MonoBehaviour
{
    [Header("Sliders")]
    [SerializeField]private Slider _rSlider;
    [SerializeField]private Slider _gSlider;
    [SerializeField]private Slider _bSlider;

    [Header("Target")]
    [SerializeField]private Image _targetImage;

    void Start()
    {
        // スライダーの範囲を0〜255に設定
        _rSlider.minValue = 0; _rSlider.maxValue = 255;
        _gSlider.minValue = 0; _gSlider.maxValue = 255;
        _bSlider.minValue = 0; _bSlider.maxValue = 255;

        // 値変更イベントを登録
        _rSlider.onValueChanged.AddListener(_ => UpdateColor());
        _gSlider.onValueChanged.AddListener(_ => UpdateColor());
        _bSlider.onValueChanged.AddListener(_ => UpdateColor());

        // 初期色を反映
        UpdateColor();
    }

    void UpdateColor()
    {
        float r = _rSlider.value / 255f;
        float g = _gSlider.value / 255f;
        float b = _bSlider.value / 255f;

        if (_targetImage != null)
            _targetImage.color = new Color(r, g, b, 1f);
    }
}