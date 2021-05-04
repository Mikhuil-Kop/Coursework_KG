using UnityEngine;
using UnityEngine.UI;

public class TimeView : MonoBehaviour
{
    public RectTransform scroll;
    private bool touch;

    public void DeltaScrollStart()
    {
        touch = true;
        TimeController.instance.StartTimeChange();
    }

    //private void Update()
    //{
    //    if (!touch)
    //        ScrollValue /= 2;
    //    else
    //    {
    //        Vector3 poi = Input.mousePosition;
    //        ScrollValue;
    //        TimeController.instance.ChangeTime(1);
    //    }
    //}

    //public void DeltaScrollEnd()
    //{
    //    touch = false;
    //    TimeController.instance.StartTimeChange();
    //}

    //private float ScrollValue
    //{
    //    get
    //    {
    //        return scroll.anchoredPosition.x / 200 - 1;
    //    }
    //    set
    //    {
    //        scroll.anchoredPosition = new Vector3((value + 1) * 200, 0);
    //    }
    //}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            b = !b;

            if (b)
                TimeController.instance.StartTimeChange();
            else
                TimeController.instance.EndTimeChange();
        }

        if (b)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (Input.GetKey(KeyCode.Q))
                    TimeController.instance.ChangeTime(-Time.unscaledDeltaTime * 6);
                if (Input.GetKey(KeyCode.E))
                    TimeController.instance.ChangeTime(+Time.unscaledDeltaTime * 6);
            }
            else
            {
                if (Input.GetKey(KeyCode.Q))
                    TimeController.instance.ChangeTime(-Time.unscaledDeltaTime / 2);
                if (Input.GetKey(KeyCode.E))
                    TimeController.instance.ChangeTime(+Time.unscaledDeltaTime / 2);
            }
        }
    }
    bool b;
}
