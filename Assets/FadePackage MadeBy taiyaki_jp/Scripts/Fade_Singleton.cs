using UnityEngine;

public class Fade_Singleton : MonoBehaviour
{
    private static Fade_Singleton _instanceClosed;
    [SerializeField]private FadeManager _fadeManager;
    public FadeManager Manager => this._fadeManager;
    private static bool _isFirst;

    private void Awake()
    {
        //もしすでに生成されていれば
        if (_instanceClosed != null && _instanceClosed != this)
        {
            Destroy(this.gameObject);//自身を削除

        }
        else//これがないとDestroyしたあと初期化され直す
        {

            //staticに自身を入れる
            _instanceClosed = this;
            DontDestroyOnLoad(this.gameObject);//それをシーンを跨ぐ様にする

            //↓初期化処理
            _isFirst = true;
        }
    }
    private Fade_Singleton() { }//外部からの生成をブロック

    public static Fade_Singleton Instance => _instanceClosed;

    public static bool IsFirst
    {
        get => _isFirst;
        set => _isFirst = value;
    }
}
