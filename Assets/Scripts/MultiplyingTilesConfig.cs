using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "MultiplyingTilesConfig", menuName = "Configs/MultiplyingTilesConfig")]
public sealed class MultiplyingTilesConfig : ScriptableObject
{
    public Color[] colors;
    public float multiplierDiff;
    public TextMeshPro textMeshPrefab;
    public Vector3 tmpOffset;
    public Transform cubePrefab;
    public int countOfTiles;
    public Vector3 scale 
    { 
        get => cubePrefab.localScale; 
    }
}
