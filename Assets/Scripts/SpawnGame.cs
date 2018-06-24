﻿using UnityEngine;
using System.IO;
using System.Collections.Generic;

    public class SpawnGame : PersistableObject {

    public PersistantStorage storage;
    const int saveVersion = 1;
    public ShapeFactory shapeFactory;
    public KeyCode createKey = KeyCode.C;
    public KeyCode newGameKey = KeyCode.N;
    public KeyCode saveKey = KeyCode.S;
    public KeyCode loadKey = KeyCode.L;

    string savePath;
    List<Shape> shapes;

    private void Awake() {
        shapes = new List<Shape>();      
    }


    private void Update() {
        if (Input.GetKeyDown(createKey)) {
            CreateShape();
        } else if (Input.GetKeyDown(newGameKey)){
            BeginNewGame();
        } else if (Input.GetKeyDown(saveKey)) {
            storage.Save(this, saveVersion);
        } else if (Input.GetKeyDown(loadKey)) {
            BeginNewGame();
            storage.Load(this);
        }
    }

    public override void Save(GameDataWriter writer) {
        writer.Write(shapes.Count);
        for(int i=0; i<shapes.Count; i++) {
            writer.Write(shapes[i].ShapeId);
            writer.Write(shapes[i].MaterialId);
            shapes[i].Save(writer);
        }
    }

    public override void Load(GameDataReader reader) {
        int version = reader.Version;
        if(version > saveVersion) {
            Debug.LogError("Unsupported future save version " + version);
            return;
        }
        int count = version <= 0 ? -version : reader.ReadInt();
        for(int i=0; i<count; i++) {
            int shapeId = version > 0 ? reader.ReadInt() : 0;
            int shapeMatId = version > 0 ? reader.ReadInt() : 0;
            Shape instance = shapeFactory.Get(shapeId, shapeMatId);
            instance.Load(reader);
            shapes.Add(instance);
        }
    }

    void BeginNewGame() {
            for (int i = 0; i < shapes.Count; i++) {
                Destroy(shapes[i].gameObject);
            }
            shapes.Clear();
        }

        void CreateShape() {
            Shape instance = shapeFactory.GetRandom();
            Transform t = instance.transform;
            t.localPosition = Random.insideUnitSphere * 5f;
            t.localRotation = Random.rotation;
            t.localScale = Vector3.one * Random.Range(0.1f, 1f);
            instance.SetColor(Random.ColorHSV(0f, 1f, 0.5f, 1f, 0.25f, 1f, 1f, 1f));
            shapes.Add(instance);
        }
    }