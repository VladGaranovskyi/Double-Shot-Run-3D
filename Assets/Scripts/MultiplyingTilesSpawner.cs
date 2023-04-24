using UnityEngine;
using TMPro;

public class MultiplyingTilesSpawner : MonoBehaviour
{
    [SerializeField] private MultiplyingTilesConfig _config;
    [SerializeField] private Transform _startingPoint;    
    private MultiplyingTile[] _tiles;

    private void Start()
    {
        FillTilesArray(_config.countOfTiles);
    }

    public void FillTilesArray(int count)
    {
        _tiles = new MultiplyingTile[count];
        for (int i = 0; i < count; i++)
        {
            _tiles[i] = new MultiplyingTile(
                _config.scale.z, 1f + _config.multiplierDiff * i, _startingPoint.position + Vector3.forward * _config.scale.z * i
                );
        }
    }

    public void SpawnTiles()
    {
        foreach(MultiplyingTile tile in _tiles)
        {
            Transform obj = Instantiate(_config.cubePrefab);
            obj.position = tile.Position;
            int i = Random.Range(0, _config.colors.Length);
            obj.GetComponent<MeshRenderer>().material.color = _config.colors[i];
            TextMeshPro txtMesh = Instantiate(_config.textMeshPrefab);
            txtMesh.transform.position = tile.Position + _config.tmpOffset;
            txtMesh.text = tile.Multiplier.ToString() + 'x';
        }
    }

    public float GetMultiplierByPosition(float zPos)
    {
        foreach (MultiplyingTile tile in _tiles)
        {
            if(zPos > tile.ZBorders.x && zPos < tile.ZBorders.y)
            {
                return tile.Multiplier;
            }
        }
        return 0f;
    }
}

internal class MultiplyingTile
{
    private float ZScale;
    public Vector3 Position { get; private set; }
    public float Multiplier { get; private set; }
    public Vector2 ZBorders
    {
        get => new Vector2(Position.z - ZScale, Position.z + ZScale);
    }

    public MultiplyingTile(float scale, float mult, Vector3 pos)
    {
        ZScale = scale;
        Position = pos;
        Multiplier = mult;
    }
}