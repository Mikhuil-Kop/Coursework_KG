using System;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public static TimeController instance;

    public const int CacheSize = 100;
    public const float CacheDelta = 0.2f;

    public Stack<ITimeControllable> stack = new Stack<ITimeControllable>();

    private bool timeIsChanged;
    private float timer;
    private int maxIndex, minIndex;
    private float nowTime;


    private void Awake()
    {
        instance = this;
    }

    private void OnDestroy()
    {
        instance = null;
    }

    public void Update()
    {
        if (timeIsChanged)
            return;

        timer += Time.deltaTime;
        if (timer >= CacheDelta)
        {
            timer = 0;
            maxIndex++;

            if (maxIndex - minIndex >= CacheSize)
                minIndex = maxIndex - CacheSize + 1;

            RigidTimeControllable.saveIndex = maxIndex % CacheSize;
            foreach (var controllable in stack)
                controllable.SaveTime();
        }
    }

    public void StartTimeChange()
    {
        Time.timeScale = 0;
        nowTime = maxIndex * CacheDelta;
        foreach (var controllable in stack)
            controllable.StartTimeChange();
    }

    public void ChangeTime(float delta)
    {
        nowTime += delta;

        if (nowTime >= maxIndex * CacheDelta)
            nowTime = maxIndex * CacheDelta;

        if (nowTime <= minIndex * CacheDelta)
            nowTime = minIndex * CacheDelta;

        float f = nowTime / CacheDelta;
        int i = Mathf.FloorToInt(f);
        int firstIndex = i % CacheSize;
        int secondIndex = (i + 1) % CacheSize;
        float coef = (nowTime % CacheDelta) / CacheDelta;

        Debug.Log(firstIndex + "-" + secondIndex + "-" + coef + "-" + nowTime);

        RigidTimeControllable.firstIndex = firstIndex;
        RigidTimeControllable.secondIndex = secondIndex;
        RigidTimeControllable.coef = coef;

        foreach (var controllable in stack)
            controllable.GoToTime(delta);
    }

    public void EndTimeChange()
    {
        maxIndex = Mathf.RoundToInt(nowTime / CacheDelta);
        timer = nowTime % CacheDelta;

        Time.timeScale = 1;
        foreach (var controllable in stack)
            controllable.EndTimeChange();
    }
}
