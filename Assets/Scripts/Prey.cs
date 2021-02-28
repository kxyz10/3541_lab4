using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prey : MonoBehaviour
{
    public Vector3 position;
    public GameObject predator;
    public GameObject prey;
    Vector3 up = new Vector3(0, 0, 0.2f);
    Vector3 down = new Vector3(0, 0, -0.2f);
    Vector3 left = new Vector3(-0.2f, 0, 0);
    Vector3 right = new Vector3(0.2f, 0, 0);


    private void Awake()
    {
        prey = new GameObject("prey");
    }
    // Start is called before the first frame update
    void Start()
    {  
        createMesh(prey);
        prey.transform.position = new Vector3(5, 1, 1);
        position = prey.transform.position;
        prey.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        prey.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.blue);
        predator = GameObject.Find("predator");
    }

    // Update is called once per frame
    void Update()
    {
        if (prey != null)
        {
            if (position.x < 10 && position.x > -10 && position.z < 10 && position.z > -10)
            {
                prey.transform.LookAt(position);
                prey.transform.position = position;
            }
            //prey cant have field of view because it wont run when predator is directly behind it
            if (Vector3.Distance(position, predator.transform.position) < 5)
            {
                avoidPredator();
            }
            if (Vector3.Distance(position, predator.transform.position) < 0.1)
            {
                Debug.Log("Prey destroyed");
                Destroy(prey);
            }
        }
        
    }

    void changeDirection()
    {
        if(position.x > 10)
        {
            position.x = 9.9f;
            position += up;
        }
        if(position.x < -10)
        {
            position.x = -9.9f;
            position += left;
        }
        if(position.z > 10)
        {
            position.z = 9.9f;
            position += left;
        }
        if(position.z < -10)
        {
            position.z = -9.9f;
            position += up;
        }
    }


    void avoidPredator()
    {
        float distance = Vector3.Distance(position, predator.transform.position);
        Vector3 predatorPos = predator.transform.position;
        if (Vector3.Distance((position + up), predatorPos) > distance && position.z < 9.5f)
        {
            position += up;
        }
        if (Vector3.Distance((position + down), predatorPos) > distance && position.z > -9.5f)
        {
            position += down;
        }
        if (Vector3.Distance((position + left), predatorPos) > distance && position.x > -9.5f)
        {
            position += left;
        }
        if (Vector3.Distance((position + right), predatorPos) > distance && position.x < 9.5f)
        {
        position += right;
        }
    }

    void createMesh(GameObject obj)
    {
        MeshFilter filter = obj.AddComponent<MeshFilter>();
        Mesh mesh = filter.mesh;
        mesh.Clear();

        //verticies
        Vector3[] vertices = new Vector3[]
        {
            //front
            new Vector3(0,1,1),    //0: left top front
            new Vector3(0,1,1),     //1: right top front
            new Vector3(0,-1,1),   //2: left bottom front
            new Vector3(0,-1,1),    //3: right bottom front

            //back
            new Vector3(1,1,-1),    //4: right top back
            new Vector3(-1,1,-1),   //5: left top back
            new Vector3(1,-1,-1),   //6: right bottom back
            new Vector3(-1,-1,-1),   //7: left bottom back
            
            //left
            new Vector3(-1,1,-1),   //8: left top back
            new Vector3(0,1,1),    //9: left top front
            new Vector3(-1,-1,-1),  //10: left bottom back
            new Vector3(0,-1,1),    //11: left bottom front

            //right
            new Vector3(0,1,1),     //12: right top front
            new Vector3(1,1,-1),    //13: right top back
            new Vector3(0,-1,1),    //14: right bottom front
            new Vector3(1,-1,-1),   //15: right bottom rback

            //top
            new Vector3(-1,1,-1),    //16: left top back
            new Vector3(1,1,-1),     //17: right top back
            new Vector3(0,1,1),   //18: left top front
            new Vector3(0,1,1),    //19: right top front

            //bottom
            new Vector3(0,-1,1),    //20: left bottom front
            new Vector3(0,-1,1),     //21: right bottom front
            new Vector3(-1,-1,-1),   //22: left top back
            new Vector3(1,-1,-1)     //23: right top back

        };

        //triangles
        int[] triangles = new int[]
        {
            //front
            0,2,3,
            3,1,0,

            //back
            4,6,7,
            7,5,4,

            //left
            8,10,11,
            11,9,8,

            //right
            12,14,15,
            15,13,12,

            //top
            16,18,19,
            19,17,16,

            //bottom
            20,22,23,
            23,21,20

        };

        //UVs
        Vector2[] uvs = new Vector2[]
        {
            //front
            new Vector2(0,1),
            new Vector2(0,0),
            new Vector2(1,1),
            new Vector2(1,0),
            
            //bottom
            new Vector2(0,1),
            new Vector2(0,0),
            new Vector2(1,1),
            new Vector2(1,0),

            //left
            new Vector2(0,1),
            new Vector2(0,0),
            new Vector2(1,1),
            new Vector2(1,0),

            //right
            new Vector2(0,1),
            new Vector2(0,0),
            new Vector2(1,1),
            new Vector2(1,0),

            //top
            new Vector2(0,1),
            new Vector2(0,0),
            new Vector2(1,1),
            new Vector2(1,0),

            //bottom
            new Vector2(0,1),
            new Vector2(0,0),
            new Vector2(1,1),
            new Vector2(1,0)
        };


        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;

        mesh.RecalculateNormals();
        mesh.Optimize();

        //create mesh renderer and material
        MeshRenderer renderer = obj.AddComponent<MeshRenderer>();
        Material material = renderer.material;
    }
}