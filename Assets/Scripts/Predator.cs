using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Predator : MonoBehaviour
{
    public Vector3 position;
    public Vector3 direction;
    public bool chasing;
    public GameObject predator;
    public GameObject prey;
    public float timeInterval;
    public float timePassed = 0;
    Vector3 up = new Vector3(0, 0, 0.1f);
    Vector3 down = new Vector3(0, 0, -0.1f);
    Vector3 left = new Vector3(-0.1f, 0, 0);
    Vector3 right = new Vector3(0.1f, 0, 0);
    

    // Start is called before the first frame update
    void Start()
    {
        predator = new GameObject("predator");
        createMesh(predator);
        predator.transform.position = new Vector3(1, 1, 1);
        position = predator.transform.position;
        predator.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.red);
        chasing = false;
        prey = GameObject.Find("prey");
        if (prey == null)
        {
            Debug.Log("Prey is null");
        }
        timeInterval = 0.5f;
        direction = moveRandomly();

    }

    // Update is called once per frame
    void Update()
    {
        //need to worry about when random position takes it far out of bounds
        //right now, it just waits to move until its position is back in bounds
        if (position.x < 10 && position.x > -10 && position.z < 10 && position.z > -10)
        {
            predator.transform.position = position;
        }
        else
        {
            //position = new Vector3(0, 0, 0);
            //predator.transform.position = new Vector3(0, 0, 0);
        }

        
        if (Vector3.Distance(position, prey.transform.position) < 4)
        {
            chasing = true;
            Debug.Log("chasing");
        }
        else
        {
            chasing = false;
        }

        if (chasing)
        {
            chasePrey();
        }
        else
        {
            timePassed += Time.deltaTime;
            
            if (timePassed < timeInterval)
            {
                position += direction;
            }
            else
            {
                direction = moveRandomly();
                timePassed = 0;
            }
        }

    }

    Vector3 moveRandomly()
    {
        Vector3 direction;
        float rand = Random.Range(0.0f, 1.0f);
        if(rand < 0.25f)
        {
            direction = up;
        }
        else if(rand < 0.5f)
        {
            direction = down;
        }
        else if(rand < 0.75f)
        {
            direction = left;
        }
        else
        {
            direction = right;
        }
        return direction;
        //timePassed = 0;
        //while(timePassed < timeInterval && !chasing)
        //{
        //    position += direction;
        //    timePassed += Time.deltaTime;
        //}
        //timePassed = 0;
    }

    void chasePrey()
    {
        float distance = Vector3.Distance(position, prey.transform.position);
        Vector3 preyPos = prey.transform.position;
        if(Vector3.Distance((position + up),preyPos) < distance)
        {
            position += up;
        }
        if (Vector3.Distance((position + down), preyPos) < distance)
        {
            position += down;
        }
        if (Vector3.Distance((position + left), preyPos) < distance)
        {
            position += left;
        }
        if (Vector3.Distance((position + right), preyPos) < distance)
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