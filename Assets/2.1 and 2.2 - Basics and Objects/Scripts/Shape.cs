using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : PersistableObject {

    public int ShapeId { get { return shapeId; }
        set {
            if (shapeId == int.MinValue && value != int.MinValue)
                shapeId = value;
            else
                Debug.LogError("Not allowed to change ShapeID!");
        }
    }
    MeshRenderer meshRenderer;
    Color color;
    private void Awake() {
        meshRenderer = GetComponent<MeshRenderer>();
    }
    public int MaterialId { get; private set; }
    public void SetMaterial(Material mat, int materialId) {
        meshRenderer.material = mat;
        MaterialId = materialId;
    }
    static int colorPropertyId = Shader.PropertyToID("_Color");
    static MaterialPropertyBlock sharedPropertyBlock;

    public void SetColor(Color color) {
        this.color = color;
        if(sharedPropertyBlock == null)
        sharedPropertyBlock = new MaterialPropertyBlock();
        sharedPropertyBlock.SetColor(colorPropertyId, color);
        meshRenderer.SetPropertyBlock(sharedPropertyBlock);
    }
    int shapeId = int.MinValue;

    public override void Save(GameDataWriter writer) {
        base.Save(writer);
        writer.Write(color);
    }
    public override void Load(GameDataReader reader) {
        base.Load(reader);
        SetColor(reader.Version > 0 ? reader.ReadColor() : Color.white);
    }
}
