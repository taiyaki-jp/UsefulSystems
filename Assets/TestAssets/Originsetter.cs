using FadeOrigins;
using UnityEngine;
using UnityEngine.UI;

public class MethodSetter : MonoBehaviour
{
    private Dropdown _thisDropdown;

    private void Awake()
    {
        _thisDropdown = GetComponent<Dropdown>();
    }

}

public class FadeSetting
{
    public Horizontal horizontal;
    public Vertical vertical;
    public Radial_90 radial_90;
    public Radial_180 radial_180;
    public Radial_360 radial_360;
}