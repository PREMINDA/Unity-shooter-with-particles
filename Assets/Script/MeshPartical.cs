using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

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

        Vector3 quadPostion = e.gunEndPoint;
        float rotation = 0f;
        Vector3 size = new Vector3(0.1f, 0.2f);

        int spawnquadindex = AddQuad(e.gunEndPoint,rotation, size,true);

        FunctionUpdater.Create(() =>
        {
            quadPostion += new Vector3(1f, 1f) * Time.deltaTime;
            rotation += 360f * Time.deltaTime;
            UpdateQuad(spawnquadindex, quadPostion, rotation, new Vector3(0.1f, 0.2f),true);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private int AddQuad(Vector3 postion,float rotation,Vector3 size,bool skewed)
    {
        if (quadIndex > maxQuadIndex) return 0;

        UpdateQuad(quadIndex, postion, rotation, size,skewed);

        int spawnQuad = quadIndex;
        quadIndex++;

        return spawnQuad;
        

    }
    public void UpdateQuad(int quadIndex,Vector3 postion,float rotation,Vector3 quadSize,bool skewed)
    {
        int vIndex = quadIndex * 4;
        int vIndex0 = vIndex;
        int vIndex1 = vIndex + 1;
        int vIndex2 = vIndex + 2;
        int vIndex3 = vIndex + 3;




        if (skewed)
        {
            vertices[vIndex0] = postion + Quaternion.Euler(0, 0, rotation) * new Vector3(-quadSize.x,-quadSize.y);
            vertices[vIndex1] = postion + Quaternion.Euler(0, 0, rotation) * new Vector3(-quadSize.x, +quadSize.y);
            vertices[vIndex2] = postion + Quaternion.Euler(0, 0, rotation) * new Vector3(+quadSize.x, +quadSize.y);
            vertices[vIndex3] = postion + Quaternion.Euler(0, 0, rotation) * new Vector3(+quadSize.x, -quadSize.y);
        }
        else
        {
            vertices[vIndex0] = postion + Quaternion.Euler(0, 0, rotation - 180) * quadSize;
            vertices[vIndex1] = postion + Quaternion.Euler(0, 0, rotation - 270) * quadSize;
            vertices[vIndex2] = postion + Quaternion.Euler(0, 0, rotation - 0) * quadSize;
            vertices[vIndex3] = postion + Quaternion.Euler(0, 0, rotation - 90) * quadSize;
        }

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

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }
}
