﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NucleonSpawner : MonoBehaviour {

    public float timeBetweenSpawns;
    public float spawnDistance;
    public Nucleon[] nucleonPrefabs;
    private float timeSinceLastSpawn;
    public int objectsSpawned { get; private set; }
    private void FixedUpdate() {
        timeSinceLastSpawn += Time.deltaTime;
        if(timeSinceLastSpawn>=timeBetweenSpawns){
            timeSinceLastSpawn -= timeBetweenSpawns;
            SpawnNucleon();
        }
            
    }

    void SpawnNucleon(){
        Nucleon prefab = nucleonPrefabs[Random.Range(0, nucleonPrefabs.Length)];
        Nucleon spawn = Instantiate<Nucleon>(prefab);
        spawn.transform.localPosition = Random.onUnitSphere * spawnDistance;
        objectsSpawned++;
    }
}
