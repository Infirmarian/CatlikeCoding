using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour {
    const float degreesPerHour = 30f,
    degreesPerMinute = 6f,
    degreesPerSecond = 6f;

    public Transform hoursTransform, minutesTransform, secondsTransform;
    public bool continuous;

    void Update() {
        if (continuous)
            ContinuousUpdate();
        else
            DiscreteUpdate();
    }

    private void ContinuousUpdate(){
        System.TimeSpan time = System.DateTime.Now.TimeOfDay;
        hoursTransform.localRotation = Quaternion.Euler(0, (float)time.TotalHours * degreesPerHour, 0);
        minutesTransform.localRotation = Quaternion.Euler(0, (float)time.TotalMinutes * degreesPerMinute, 0);
        secondsTransform.localRotation = Quaternion.Euler(0, (float)time.TotalSeconds * degreesPerSecond, 0);
    }

    private void DiscreteUpdate() {
        System.DateTime time = System.DateTime.Now;
        hoursTransform.localRotation = Quaternion.Euler(0, time.Hour * degreesPerHour, 0);
        minutesTransform.localRotation = Quaternion.Euler(0, time.Minute * degreesPerMinute, 0);
        secondsTransform.localRotation = Quaternion.Euler(0, time.Second * degreesPerSecond, 0);
    }
}