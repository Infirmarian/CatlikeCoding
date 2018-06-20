using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fractal : MonoBehaviour {

    public Material material;
    public int maxDepth;
    public float childScale;
    public float maxRotationSpeed, maxTwist;
    [Range(0f,1f)]
    public float spawnProbability;

    private float rotationSpeed;
    private static Vector3[] childDirections={
        Vector3.up, Vector3.right, Vector3.left, Vector3.forward, Vector3.back
    };
    private static Quaternion[] childOrientations ={
        Quaternion.identity, Quaternion.Euler(0f,0f,-90f), Quaternion.Euler(0f,0f,90f),
        Quaternion.Euler(90f,0f,0f), Quaternion.Euler(-90f,0,0)
    };
    private Material[,] materials;
    public Mesh[] meshes;

    void InitializeMaterials(){
        materials = new Material[maxDepth + 1,2];
        for (int i = 0; i <=maxDepth; i++){
            float t = i / (maxDepth - 1f);
            t *= t;
            materials[i,0] = new Material(material);
            materials[i,0].color = Color.Lerp(Color.white, Color.cyan, t);
            materials[i, 1] = new Material(material);
            materials[i, 1].color = Color.Lerp(Color.white, Color.yellow, t);
        }
        materials[maxDepth,0].color = Color.magenta;
        materials[maxDepth, 1].color = Color.red;

    }
    private int depth;
	// Use this for initialization
	void Start () {
        rotationSpeed = Random.Range(-maxRotationSpeed, maxRotationSpeed);
        if (materials == null)
            InitializeMaterials();
        transform.Rotate(Random.Range(-maxTwist, maxTwist), 0f, 0f);
        gameObject.AddComponent<MeshFilter>().mesh = meshes[Random.Range(0, meshes.Length)];
        gameObject.AddComponent<MeshRenderer>().material = materials[depth, Random.Range(0,2)];
        if (depth < maxDepth) {
            StartCoroutine(CreateChildren());
        }
    }

    private void Update() {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }


    IEnumerator CreateChildren(){
        for (int i = 0; i < childDirections.Length; i++){
            if (Random.value > spawnProbability)
                continue;
            yield return new WaitForSeconds(Random.Range(0.1f,0.9f));
            new GameObject("Fractal Child").AddComponent<Fractal>().Initialize(this, i);
        }
     }

    private void Initialize(Fractal parent, int childIndex){
        meshes = parent.meshes;
        maxTwist = parent.maxTwist;
        maxRotationSpeed = parent.maxRotationSpeed;
        spawnProbability = parent.spawnProbability;
        materials = parent.materials;
        childScale = parent.childScale;
        material = parent.material;
        depth = parent.depth + 1;
        maxDepth = parent.maxDepth;
        transform.parent = parent.transform;
        transform.localRotation = childOrientations[childIndex];
        transform.localScale = Vector3.one * childScale;
        transform.localPosition = childDirections[childIndex] * (0.5f + 0.5f * childScale);
    }

}
