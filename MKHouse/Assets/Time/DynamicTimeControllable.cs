using System;
using System.Collections.Generic;
using UnityEngine;

//public class DynamicTimeControllable : MonoBehaviour, ITimeControllable
//{
//    public Collider col;
//    public Rigidbody rb;

//    private LinkedList<Past> pasts = new LinkedList<Past>();

//    private void Start()
//    {
//        TimeController.instance.stack.Push(this);
//        SaveTime();
//    }

//    public void SaveTime()
//    {
//        pasts.AddLast(new Past
//        {
//            position = transform.position,
//            rotation = transform.rotation,
//            velocity = rb.velocity
//        });
//    }


//    public void StartTimeChange()
//    {
//        col.enabled = false;
//    }

//    public void EndTimeChange()
//    {
//        col.enabled = true;
//    }

//    public void GoToTime(float delta)
//    {
//        var past1 = pasts[firstIndex];
//        var past2 = pasts[secondIndex];

//        Vector3 pos = past1.position * (1 - coef) + past2.position * coef;
//        Vector3 vel = past1.velocity * (1 - coef) + past2.velocity * coef;
//        Quaternion rot = Quaternion.Lerp(past1.rotation, past2.rotation, coef);

//        transform.position = pos;
//        transform.rotation = rot;
//        rb.velocity = vel;
//    }

//    private struct Past
//    {
//        public Vector3 position;
//        public Quaternion rotation;
//        public Vector3 velocity;
//    }
//}