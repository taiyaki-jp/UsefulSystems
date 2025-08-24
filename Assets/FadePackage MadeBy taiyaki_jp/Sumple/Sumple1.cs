using System;
using UnityEngine;
using UnityEngine.UI;
public class Sumple1 : MonoBehaviour
{
    private FadeManager _fadeManager;
    [SerializeField] private Button _button;

    [SerializeField] private Color _startColor;
    [SerializeField] private Color _midColor;
    [SerializeField] private Color _mid2Color;
    [SerializeField] private Color _endColor;

    // Start is called before the first frame update
    void Start()
    {
        //これでフェードマネージャーを取れる
        _fadeManager = FadeManager.Instance;

        _button.onClick.AddListener(() =>
            //↓このように呼び出す
            _ = _fadeManager.Fade<Enum>("SumpleScene2",
                startOrigin: Horizontal.Right,//FillOriginEnum.cs参照
                endOrigin: Vertical.Top,//FillOriginEnum.cs参照
                startColor: _startColor,
                midColor: _midColor,
                midColor2: _mid2Color,
                endColor: _endColor
            )
        );
    }
}