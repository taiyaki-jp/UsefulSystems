using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeManager : SingletonBase<FadeManager>
{
    [SerializeField, Header("フェード速度")] private float _fadeSpeed = 1;
    [SerializeField] private GameObject _fadeCanvas;
    private FadeAndLoad _load;

    Action _beforeAction = null;
    Action _afterAction = null;
    Action _finishAction = null;

    /// <summary>
    /// 外部からActionを設定する
    /// </summary>
    /// <param name="mode">0=画面暗転後　1=画面遷移直後　2=フェード終了後</param>
    /// <param name="action">設定する関数</param>
    public void AddAction(int mode, Action action)
    {
        switch (mode)
        {
            case 0:
                _beforeAction += action;
                break;
            case 1:
                _afterAction += action;
                break;
            default:
                _finishAction += action;
                break;
        }
    }

    /// <summary>
    /// 外部から設定したActionを削除する
    /// </summary>
    /// <param name="mode">0=画面暗転後　1=画面遷移直後　2=フェード終了後</param>
    /// <param name="action">設定する関数</param>
    public void RemoveAction(int mode, Action action)
    {
        switch (mode)
        {
            case 0:
                _beforeAction -= action;
                break;
            case 1:
                _afterAction -= action;
                break;
            case 2:
                _finishAction -= action;
                break;
        }
    }

    protected override void Awake()
    {
        base.Awake();//SingletonBaseのAwakeを実行
        _load = new FadeAndLoad
        {
            Image = _fadeCanvas.GetComponentInChildren<Image>(),
            Speed = _fadeSpeed
        };

    }

    private void Start()
    {
        _ = FirstFade();
    }

    /// <summary>
    /// 最初のフェード
    /// </summary>
    private async UniTask FirstFade()
    {
        Debug.Log("FirstFade");
        _afterAction?.Invoke();
        await _load.FadeSystem<Enum>(-1, Color.black, Color.clear);
        _finishAction?.Invoke();

        _fadeCanvas.SetActive(false);
    }


    /// <summary>
    /// フェードを呼び出す関数
    /// </summary>
    /// <param name="sceneName">遷移先のシーンの名前</param>
    /// <param name="startOrigin">[省略可]FillOriginEnum.csのEnum 省略すると透明度フェード</param>
    /// <param name="endOrigin">[省略可]FillOriginEnum.csのEnum 省略すると透明度フェード</param>
    /// <param name="startColor">[省略可]フェード開始時の色　省略すると黒　透明度フェードなら透明</param>
    /// <param name="midColor">[省略可]画面が見えなくなった時の色　省略すると黒</param>
    /// <param name="midColor2">[省略可]画面が見えなくなったあと色をさらに変えたいときに使う</param>
    /// <param name="endColor">[省略可]フェード終了時の色　省略すると黒　透明度フェードなら透明</param>
    public async UniTask Fade<TOriginEnum>(string sceneName, TOriginEnum startOrigin = default, TOriginEnum endOrigin = default, Color startColor = default, Color midColor = default, Color midColor2 = default, Color endColor = default) where TOriginEnum : Enum
    {
        //defaultを変換
        if (startOrigin == null && startColor == default) startColor = Color.clear;
        else　if (startColor == default) startColor = Color.black;

        if (midColor == default) midColor = Color.black;

        if (endOrigin == null && endColor == default) endColor = Color.clear;
        else if (endColor == default) endColor = Color.black;

        _fadeCanvas.SetActive(true);
        Color finalMid = midColor;

        await _load.FadeSystem(+1, startColor, midColor, startOrigin);
        _beforeAction?.Invoke();
        if (midColor2 != default)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(1f));
            await _load.FadeSystem<Enum>(-1, midColor, midColor2);
            await UniTask.Delay(TimeSpan.FromSeconds(1f));
            finalMid = midColor2;
        }

        await SceneManager.LoadSceneAsync(sceneName);
        _afterAction?.Invoke();

        await _load.FadeSystem(-1, finalMid, endColor, endOrigin);
        _finishAction?.Invoke();

        _fadeCanvas.SetActive(false);
    }
}