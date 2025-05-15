using System;
using System.Collections.Generic;
using UnityEngine;

public class SelectObjectInfo : MonoBehaviour
{
    public CanSelectObject CurrentObject;

    public Action<string> ProfileIMGUpdate;
    public Action<string> NameUpdate;
    public Action<float, float> HealthBarUpdate;
    public Action<Dictionary<string, string>> StateUpdate;

    private void Start()
    {
        UIManager.Instance.SelectObjectInfo = this;

        ProfileIMGUpdate?.Invoke("Images/Default");
        NameUpdate?.Invoke("Yoon");
        HealthBarUpdate?.Invoke(100, 100);
        Dictionary<string, string> TestMap = new Dictionary<string, string>(){};
        TestMap["AttackPower"] = "9999";
        TestMap["sdfjlsdfj"] = "9999";
        TestMap["dsf"] = "9999";
        TestMap["dfgsd"] = "9999";
        TestMap["sdfgsdfh"] = "9999";
        StateUpdate?.Invoke(TestMap);

        GetComponent<Canvas>().enabled = false;
    }
}
