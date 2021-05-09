using System;
using System.Collections.Generic;
using UnityEngine;

public class RigidTimeControllable : MonoBehaviour, ITimeControllable
{
    //Статические переменные для кэширования вычисления
    public static int firstIndex, secondIndex, saveIndex;
    public static float coef;

    public Collider col;
    public Rigidbody rb;

    private readonly Past[] pasts = new Past[TimeController.CacheSize];

    private void Start()
    {
        if (col == null)
            col = GetComponent<Collider>();
        if (rb == null)
            rb = GetComponent<Rigidbody>();

        TimeController.instance.stack.Push(this);
        SaveTime();
    }

    public void SaveTime()
    {
        pasts[saveIndex] = new Past
        {
            position = transform.position,
            rotation = transform.rotation,
            velocity = rb.velocity,
            angularVelocity = rb.angularVelocity
        };
    }

    public void StartTimeChange()
    {
        col.enabled = false;
    }

    public void EndTimeChange()
    {
        col.enabled = true;
    }

    public void GoToTime(float delta)
    {
        var past1 = pasts[firstIndex];
        var past2 = pasts[secondIndex];

        Vector3 pos = past1.position * (1 - coef) + past2.position * coef;
        Vector3 vel = past1.velocity * (1 - coef) + past2.velocity * coef;
        Quaternion rot = Quaternion.Lerp(past1.rotation, past2.rotation, coef);
        Vector3 avel = past1.angularVelocity * (1 - coef) + past2.angularVelocity * coef;

        transform.position = pos;
        transform.rotation = rot;
        rb.velocity = vel;
        rb.angularVelocity = avel;
    }

    private struct Past
    {
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 velocity;
        public Vector3 angularVelocity;
    }
}
