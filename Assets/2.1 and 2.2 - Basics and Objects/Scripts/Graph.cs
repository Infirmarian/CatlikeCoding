using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour {
    [Range(10,100)]
    public int resolution = 10;

    public GraphFunctionName function;
    public Transform pointPrefab;
    private Transform[] points;
    static GraphFunction[] functions = { 
        SineFunction, MultiSineFunction, Sine2DFunction, MultiSine2DFunction, Ripple, Cylinder,
        Sphere, Torus
    };

    private void Awake() {
        points = new Transform[resolution*resolution];
        float step = 2f / resolution;
        Vector3 scale = Vector3.one * step;
        for (int i = 0; i< points.Length; i++) {
                Transform point = Instantiate(pointPrefab);
                point.localScale = scale;
                point.SetParent(this.transform, false);
                points[i] = point;
            }
        }

    private void Update() {
        float t = Time.time;
        GraphFunction f = functions[(int)function];
        float step = 2f / resolution;
        for (int i = 0, z = 0; z < resolution; z++){
            float v = (z + 0.5f) * step - 1f;
            for (int x = 0; x < resolution; x++, i++){
                float u = (x + 0.5f) * step - 1f;
                points[i].localPosition = f(u, v, t);
            }
        }
    }
    const float pi = Mathf.PI;
    static Vector3 SineFunction(float x, float z,float t){
        Vector3 p;
        p.x = x;
        p.z = z;
        p.y=Mathf.Sin(pi * (x + t));
        return p;
    }

    static Vector3 MultiSineFunction(float x, float z, float t) {
        float y =Mathf.Sin(pi * (x + t));
        y += Mathf.Sin(2f * pi * (x +2f* t)) * 0.5f;
        y *= .6666f;
        Vector3 p;
        p.x = x;
        p.y = y;
        p.z = z;
        return p;
    }
    static Vector3 Sine2DFunction(float x, float z, float t){
        float y = Mathf.Sin(pi * (x + t));
        y += Mathf.Sin(pi * (z + t));
        y *=0.5f;
        Vector3 p;
        p.z = z;
        p.x = x;
        p.y = y;
        return p;
    }
    static Vector3 MultiSine2DFunction(float x, float z, float t){
        float y = 4f * Mathf.Sin(pi * (x + z + 0.5f * t));
        y += Mathf.Sin(pi * (x + t));
        y += Mathf.Sin(2f * pi * (z + 2f * t)) * 0.5f;
        y *= 1f / 5.5f;
        Vector3 p;
        p.x = x;
        p.z = z;
        p.y = y;
        return p;
    }

    static Vector3 Ripple(float x, float z, float t){
        float d = Mathf.Sqrt(x * x + z * z);
        float y = Mathf.Sin(pi * (4f*d-t));
        y = y / (1f + d*10f);
        Vector3 p;
        p.x = x;
        p.y = y;
        p.z = z;
        return p;
    }
    static Vector3 Cylinder(float u, float v, float t){
        Vector3 p;
        float r=0.8f + Mathf.Sin(pi * (6f * u + 2f * v + t)) * 0.2f;
        p.x = r*Mathf.Sin(pi*u);
        p.y = v;
        p.z = r*Mathf.Cos(pi*u);
        return p;
    }

    static Vector3 Sphere(float u, float v, float t){
        Vector3 p;
        float r = 0.8f + Mathf.Sin(pi * (6f * u + t)) * 0.1f;
        r += Mathf.Sin(pi * (4f * v + t)) * 0.1f;
        float s = r * Mathf.Cos(pi * 0.5f * v);

        p.x = s* Mathf.Sin(pi * u);
        p.y = r*Mathf.Sin(pi * 0.5f * v);
        p.z = s * Mathf.Cos(pi * u);
        return p;
    }
    static Vector3 Torus(float u, float v, float t) {
        Vector3 p;
        float r1 = 0.65f + Mathf.Sin(pi * (6f * u + t)) * 0.1f;
        float r2 = 0.2f + Mathf.Sin(pi * (4f * v + t)) * 0.05f;
        float s =r2*Mathf.Cos(pi * v)+r1;
        p.x = s * Mathf.Sin(pi * u);
        p.y = r2*Mathf.Sin(pi * v);
        p.z = s * Mathf.Cos(pi * u);
        return p;
    }

}
