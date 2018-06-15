using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour {
    [Range(10,100)]
    public int resolution = 10;

    public Transform pointPrefab;
    private void Awake() {
        float step = 2f / resolution;
        Vector3 scale = Vector3.one * step;
        Vector3 pos;
        pos.y = 0f;
        pos.z = 0f;
        for (int i = 0; i < resolution; i++){
            pos.x = ((i + 0.5f) * step - 1f);
            pos.y = pos.x * pos.x * pos.x;
            Transform point = Instantiate(pointPrefab);
            point.localPosition = pos;
            point.localScale = scale;
            point.SetParent(this.transform, false);
            }
    }
}
