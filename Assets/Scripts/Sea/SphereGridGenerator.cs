using UnityEngine;

public class SphereGridGenerator : MonoBehaviour
{
    [Header("Sphere Settings")]
    public float radius = 5f;
    public int latitudeCount = 10;
    public int longitudeCount = 20;

    [Header("Tile Settings")]
    public GameObject tilePrefab;
    public float tileScale = 0.9f;

    void Start()
    {
        GenerateSphere();
    }

    void GenerateSphere()
    {
        // Supprime les anciennes cases
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }

        for (int lat = 0; lat <= latitudeCount; lat++)
        {
            float a1 = Mathf.PI * lat / latitudeCount;

            float sin1 = Mathf.Sin(a1);
            float cos1 = Mathf.Cos(a1);

            for (int lon = 0; lon < longitudeCount; lon++)
            {
                float a2 = 2 * Mathf.PI * lon / longitudeCount;

                float sin2 = Mathf.Sin(a2);
                float cos2 = Mathf.Cos(a2);

                // Position sur la sphère
                Vector3 pos = new Vector3(
                    sin1 * cos2,
                    cos1,
                    sin1 * sin2
                ) * radius;

                // Création de la case
                GameObject tile = Instantiate(tilePrefab, pos, Quaternion.identity, transform);

                // Oriente la case vers l'extérieur
                tile.transform.up = pos.normalized;

                // Taille
                tile.transform.localScale = Vector3.one * tileScale;
            }
        }
    }
}