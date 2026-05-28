using UnityEngine;
using System.Collections.Generic;

public class VoxelChunkGenerator : MonoBehaviour
{
    public Transform blocksParent;

    private HashSet<Vector3Int> blocks = new HashSet<Vector3Int>();

    void Start()
    {
        ConvertPrefabsToGrid();
        GenerateMesh();

        Destroy(blocksParent.gameObject);
    }

    void ConvertPrefabsToGrid()
    {
        foreach (Transform child in blocksParent)
        {
            Vector3 pos = child.position;

            Vector3Int gridPos = new Vector3Int(
                Mathf.RoundToInt(pos.x),
                Mathf.RoundToInt(pos.y),
                Mathf.RoundToInt(pos.z)
            );

            blocks.Add(gridPos);
        }

        Debug.Log("Blocs détectés : " + blocks.Count);
    }

    void GenerateMesh()
    {
        GameObject obj = new GameObject("VoxelMesh");
        obj.transform.parent = transform;

        MeshFilter mf = obj.AddComponent<MeshFilter>();
        MeshRenderer mr = obj.AddComponent<MeshRenderer>();
        MeshCollider mc = obj.AddComponent<MeshCollider>();

        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();

        foreach (var block in blocks)
        {
            AddBlock(block, vertices, triangles);
        }

        Mesh mesh = new Mesh();
        mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;

        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();

        mesh.RecalculateNormals();

        mf.mesh = mesh;
        mc.sharedMesh = mesh;
    }

    void AddBlock(Vector3Int pos, List<Vector3> v, List<int> t)
    {
        CheckFace(pos, Vector3Int.forward, v, t);
        CheckFace(pos, Vector3Int.back, v, t);
        CheckFace(pos, Vector3Int.left, v, t);
        CheckFace(pos, Vector3Int.right, v, t);
        CheckFace(pos, Vector3Int.up, v, t);
        CheckFace(pos, Vector3Int.down, v, t);
    }

    void CheckFace(Vector3Int pos, Vector3Int dir, List<Vector3> v, List<int> t)
    {
        if (blocks.Contains(pos + dir))
            return;

        AddFace(pos, dir, v, t);
    }

    void AddFace(Vector3Int pos, Vector3Int dir, List<Vector3> v, List<int> t)
    {
        int index = v.Count;
        Vector3 p = pos;

        Vector3[] face = GetFace(p, dir);

        v.AddRange(face);

        // ✅ TRIANGLES CORRIGÉS (extérieur)
        t.Add(index);
        t.Add(index + 1);
        t.Add(index + 2);

        t.Add(index + 2);
        t.Add(index + 1);
        t.Add(index + 3);
    }

    Vector3[] GetFace(Vector3 p, Vector3Int dir)
    {
        if (dir == Vector3Int.forward)
            return new Vector3[] {
                p + new Vector3(0,0,1),
                p + new Vector3(0,1,1),
                p + new Vector3(1,0,1),
                p + new Vector3(1,1,1)
            };

        if (dir == Vector3Int.back)
            return new Vector3[] {
                p + new Vector3(1,0,0),
                p + new Vector3(1,1,0),
                p + new Vector3(0,0,0),
                p + new Vector3(0,1,0)
            };

        if (dir == Vector3Int.left)
            return new Vector3[] {
                p + new Vector3(0,0,0),
                p + new Vector3(0,1,0),
                p + new Vector3(0,0,1),
                p + new Vector3(0,1,1)
            };

        if (dir == Vector3Int.right)
            return new Vector3[] {
                p + new Vector3(1,0,1),
                p + new Vector3(1,1,1),
                p + new Vector3(1,0,0),
                p + new Vector3(1,1,0)
            };

        if (dir == Vector3Int.up)
            return new Vector3[] {
                p + new Vector3(0,1,1),
                p + new Vector3(0,1,0),
                p + new Vector3(1,1,1),
                p + new Vector3(1,1,0)
            };

        // down
        return new Vector3[] {
            p + new Vector3(0,0,0),
            p + new Vector3(0,0,1),
            p + new Vector3(1,0,0),
            p + new Vector3(1,0,1)
        };
    }
}