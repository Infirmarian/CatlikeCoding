﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformationGrid : MonoBehaviour {
    public Transform prefab;
    public int resolution;
    Transform[] grid;

    private void Awake() {
        grid = new Transform[resolution * resolution * resolution];
        for (int i = 0, z = 0; z < resolution; z++){
            for (int y = 0; y < resolution;y++){
                for (int x = 0; x < resolution; i++, x++){
                    grid[i] = CreateGridPoint(x, y, z);
                }
            }
        }
    }

    private Transform CreateGridPoint(int x, int y, int z){
        Transform point = Instantiate<Transform>(prefab);
        point.localPosition = GetCoordinates(x, y, z);
        point.GetComponent<MeshRenderer>().material.color = new Color(
            (float)x / resolution, (float)y / resolution, (float)z / resolution);
        return point;
    }

    private Vector3 GetCoordinates(int x, int y, int z){
        return new Vector3(
            x - (resolution - 1) * 0.5f, y - (resolution - 1) * 0.5f, z - (resolution - 1) * 0.5f);
    }
}
