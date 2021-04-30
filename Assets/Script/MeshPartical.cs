using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshPartical : MonoBehaviour
{
    [SerializeField] private PlayerShoot playershoot;
    private Mesh mesh;
    private Vector3[] vertices;
    private Vector2[] uv;
    private int[] triangles;

    private int maxQuadIndex = 10000;
    private int quadIndex = 0;

    void Awake()
    {
        mesh = new Mesh();

        vertices = new Vector3[4 * maxQuadIndex];
        uv = new Vector2[4 * maxQuadIndex];
        triangles = new int[6 * maxQuadIndex];


        playershoot.OnShoot += Playershoot_OnShoot;

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        gameObject.GetComponent<MeshFilter>().mesh = mesh;
        
    }

    private void Playershoot_OnShoot(object sender, PlayerShoot.OnShootEventArg e)
    {
        AddQuad(e.gunEndPoint);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void AddQuad(Vector3 postion)
    {
        if (quadIndex > maxQuadIndex) return;
        int vIndex = quadIndex * 4;
        int vIndex0 = vIndex ;
        int vIndex1 = vIndex + 1;
        int vIndex2 = vIndex + 2;
        int vIndex3 = vIndex + 3;


        Vector3 quadSize = new Vector3(0.2f, 0.2f);

        float rotation = 0f;

        vertices[vIndex0] = postion + Quaternion.Euler(0, 0, rotation - 180) * quadSize;
        vertices[vIndex1] = postion + Quaternion.Euler(0, 0, rotation - 270) * quadSize;
        vertices[vIndex2] = postion + Quaternion.Euler(0, 0, rotation - 0) * quadSize;
        vertices[vIndex3] = postion + Quaternion.Euler(0, 0, rotation - 90) * quadSize;

        uv[vIndex0] = new Vector2(0, 0);
        uv[vIndex1] = new Vector2(0, 1);
        uv[vIndex2] = new Vector2(1, 1);
        uv[vIndex3] = new Vector2(1, 0);

        int tIndex = quadIndex * 6;
        triangles[tIndex + 0] = vIndex0;
        triangles[tIndex + 1] = vIndex1;
        triangles[tIndex + 2] = vIndex2;

        triangles[tIndex + 3] = vIndex0;
        triangles[tIndex + 4] = vIndex2;
        triangles[tIndex + 5] = vIndex3;
        quadIndex++;
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

    }
    public void UpdateQuad(int quadIndex,Vector3 postion,float rotation,Vector3 quadSize)
    {
        
    }
}
