using System;
using System.Collections.Generic;
using System.Linq;
using FadeOrigins;
using UnityEngine;
using UnityEngine.UI;

public class Originsetter : MonoBehaviour
{
    [SerializeField] private Dropdown _originDropdown;
    [SerializeField] private bool _canSkip;
    private Dropdown _thisDropdown;

    private void Awake()
    {
        _thisDropdown = GetComponent<Dropdown>();
        _thisDropdown.ClearOptions();
        List<string> methodList = new List<string>();

        if (_canSkip) methodList.Add("Skip");
        else methodList.Add("None");

        var methods = Enum.GetValues(typeof(Image.FillMethod)).Cast<Image.FillMethod>().ToList();
        foreach (var t in methods)
        {
            methodList.Add(t.ToString());
        }

        _thisDropdown.AddOptions(methodList);
    }

    private void Start()
    {
        OnValueChange();
        _thisDropdown.onValueChanged.AddListener((i) => OnValueChange());
    }

    private void OnValueChange()
    {
        _originDropdown.ClearOptions();
        if (_thisDropdown.value == 0)
        {
            _originDropdown.interactable = false;
            return;
        }

        _originDropdown.interactable = true;
        var originList = new List<string>();
        switch (_thisDropdown.value)
        {
            case 1:
            {
                var origin = Enum.GetValues(typeof(Horizontal)).Cast<Horizontal>().ToList();
                foreach (var t in origin)
                {
                    originList.Add(t.ToString());
                }

                break;
            }
            case 2:
            {
                var origin = Enum.GetValues(typeof(Vertical)).Cast<Vertical>().ToList();
                foreach (var t in origin)
                {
                    originList.Add(t.ToString());
                }

                break;
            }
            case 3:
            {
                var origin = Enum.GetValues(typeof(Radial_90)).Cast<Radial_90>().ToList();
                foreach (var t in origin)
                {
                    originList.Add(t.ToString());
                }

                break;
            }
            case 4:
            {
                var origin = Enum.GetValues(typeof(Radial_180)).Cast<Radial_180>().ToList();
                foreach (var t in origin)
                {
                    originList.Add(t.ToString());
                }

                break;
            }
            case 5:
            {
                var origin = Enum.GetValues(typeof(Radial_360)).Cast<Radial_360>().ToList();
                foreach (var t in origin)
                {
                    originList.Add(t.ToString());
                }

                break;
            }
        }

        _originDropdown.AddOptions(originList);
    }
}