using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Uses parts of randomwalk from previous assignment

public class MazeGeneration : MonoBehaviour {

    // Number of tunnels - 300
    [SerializeField]
    private int tunnelNumber;

    // Tunnel length max - 10
    [SerializeField]
    private int tunnelLengthMax;

    private int mazeWidth = 25;
    private int mazeHeight = 25;

    private FloorTypes[,] map;

    public GameObject Impas;
    public GameObject Impas1;
    public GameObject Impas2;
    public GameObject Pass;

    public string tagg;

    int passCount= 0;

    // Use this for initialization
    void Start () {
        tagg = gameObject.tag.ToString();
        map = new FloorTypes[mazeWidth, mazeHeight];
        CreateRand();
        DrawFloor();
        SetFace();
        
    }

    void DrawFloor()
    {
        for (int x = 0; x < map.GetLength(0); x++)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                float mazeY = (transform.position.y);
                map[0, 11] = FloorTypes.Passable;
                map[0, 12] = FloorTypes.Passable;
                map[0, 13] = FloorTypes.Passable;
                map[24, 11] = FloorTypes.Passable;
                map[24, 12] = FloorTypes.Passable;
                map[24, 13] = FloorTypes.Passable;
                map[11, 0] = FloorTypes.Passable;
                map[12, 0] = FloorTypes.Passable;
                map[13, 0] = FloorTypes.Passable;
                map[11, 24] = FloorTypes.Passable;
                map[12, 24] = FloorTypes.Passable;
                map[13, 24] = FloorTypes.Passable;


                switch (map[x, y])
                {

                    case FloorTypes.Impassable:
                        {
                            float randInt = Random.Range(0,3);
                            if (randInt == 0){
                                GameObject go = Instantiate(Impas, new Vector3(x, (mazeY), y), Quaternion.identity);
                                go.transform.parent = transform;
                            }
                            else if (randInt == 1){
                                GameObject go = Instantiate(Impas1, new Vector3(x, (mazeY), y), Quaternion.identity);
                                go.transform.parent = transform;
                            }
                            else if (randInt == 2){
                                GameObject go = Instantiate(Impas2, new Vector3(x, (mazeY), y), Quaternion.identity);
                                go.transform.parent = transform;
                            }
                            
                            break;
                        }


                    case FloorTypes.Passable:
                        {
                            GameObject go = Instantiate(Pass, new Vector3(x, (mazeY-1), y), Quaternion.identity);
                            go.transform.parent = transform;

                            passCount++;
                            break;
                        }
                }
            }
        }
        if (passCount < 50)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }


    void CreateRand()
    {
        Vector2 position = new Vector2(
            Random.Range(0, mazeWidth - 1),
            Random.Range(0, mazeHeight - 1)
        );

        for (int i = 0; i < tunnelNumber; i++)
        {
            // Vertical or horizontal tunnel?
            // We set vertical to true, if a random float (0<x<1) is less
            // than 0.5f, and false if it is greater than 0.5f
            bool vertical = Random.value < 0.5f;

            // Similarly, we set direction to 1 if a random float is less
            // than 0.5f, and -1 if it is greater than 0.5f
            float direction = Random.value < 0.5f ? 1 : -1;

            // If we're moving vertically, generate an appropriate tunnel
            // length (max is set from unity inspector) and then
            // create a tunnel that is that long (if we don't hit a boundary)
            // and moving in the appropriate positive or negative direction
            // Similarly, if we're moving horizontall, we generate that tunnel
            // in the same way. Each time we generate a tunnel a new position
            // is returned, this is the position of our walk progressing.
            if (vertical)
            {
                int tunnelLength = Random.Range(0, tunnelLengthMax);
                CreateTunnel(ref position, tunnelLength, new Vector2(0, direction));
            }
            else
            {
                int tunnelLength = Random.Range(0, tunnelLengthMax);
                CreateTunnel(ref position, tunnelLength, new Vector2(direction, 0));
            }
        }

        // Checks all the walls and changes them to pathways if they are
        // single standing walls.  Keeps the map tidy and open.
        for (int x = 0; x < map.GetLength(0); x++)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                if (map[x, y] == FloorTypes.Impassable)
                {
                    if (CheckWalls(x, y))
                    {
                        //Debug.Log(true);
                        map[x, y] = FloorTypes.Passable;
                    }

                }
            }
        }
    }

    void CreateTunnel(ref Vector2 position, int length, Vector2 direction)
    {
        // This is a complicated for loop, but it's not as bad as it at first looks
        // let's break it down into three segments and it should become clearer.

        // int x = (int)position.x, i = 0 : This segment starts our X off at the X position
        // We have to cast to int with (int) otherwise the compiler won't let us store
        // position.x which is a float type, in x, which is an int. It also creates a i
        // variable, which we'll use to track our tunnel length.

        // i < length && x < gridWidth - 1 && x > 0 : this segment is basically three checks in one
        // we want to loop whilst i is less than length, AND x is less than gridWidth, AND x is
        // greater than zero. If any of those conditions cease to be true, we want to stop looping.

        // x += (int) direction.x, i++ : finally, if we're moving in vertical axis, direction.x will be 0
        // so we can safely add direction.x, to get our direction - it will be 0, -1 or 1. We can simply
        // increment i, as we're using that to track length independant of direction.

        // The second loop is exactly the same, except over the Y axis, and using j to track length.

        for (int x = (int)position.x, i = 0; i < length && x < mazeWidth - 1 && x > 0; x += (int)direction.x, i++)
        {
            map[x, (int)position.y] = FloorTypes.Passable; // Update appropriate map tile to walkable

            // Update current position
            position.x = x;
        }

        for (int y = (int)position.y, j = 0; j < length && y < mazeHeight - 1 && y > 0; y += (int)direction.y, j++)
        {
            map[(int)position.x, y] = FloorTypes.Passable; // Update appropriate map tile to walkable

            // Update current position
            position.y = y;
        }
    }

    /// <summary>
    /// Checks the 9 elements surrounding the element passed.
    /// </summary>
    /// <param name="row">Row of element being checked.</param>
    /// <param name="col">Column of element being checked.</param>
    /// <returns>A true if the element is a single standing wall.</returns>
    bool CheckWalls(int row, int col) {
        int notWalls = 0, cells = 0; // Define counters

        // Define the area to search
        int rowLen = System.Math.Min(row + 1, map.GetLength(0) - 1),
            colLen = System.Math.Min(col + 1, map.GetLength(1) - 1),
            rowIdx = System.Math.Max(0, row - 1),
            colIdx = System.Math.Max(0, col - 1);

        for (int i = rowIdx; i <= rowLen; i++) {
            for (int j = colIdx; j <= colLen; j++) {

                // If it is our given index, don't need to check this element
                if (i == row && j == col)
                    continue;

                ++cells;
                if (map[i, j] == FloorTypes.Passable) // Checks If the element is a pathway
                    ++notWalls;
            }
        }

        if (notWalls == 8) {
            return true;
        } else {
            return false;
        }   
    }

    void SetFace()
    {
        GameObject mz0 = GameObject.FindGameObjectWithTag("0");
        GameObject mz1 = GameObject.FindGameObjectWithTag("1");
        GameObject mz2 = GameObject.FindGameObjectWithTag("2");
        GameObject mz3 = GameObject.FindGameObjectWithTag("3");
        GameObject mz4 = GameObject.FindGameObjectWithTag("4");
        GameObject mz5 = GameObject.FindGameObjectWithTag("5");

        if (gameObject.tag == "0")
        {
            mz0.transform.position = new Vector3(0, 0, 0);
            mz0.GetComponent<Transform>().Rotate(0, 0, 0, relativeTo: Space.World);
        }
        if (gameObject.tag == "1")
        {
            mz1.transform.position = new Vector3(0, -25, -1);
            mz1.GetComponent<Transform>().Rotate(-90, 0, 0, relativeTo: Space.World);
        }
        if (gameObject.tag == "2")
        {
            mz2.transform.position = new Vector3(25, -1, 0);
            mz2.GetComponent<Transform>().Rotate(0, 0, -90, relativeTo: Space.World);
        }
        if (gameObject.tag == "3")
        {
            mz3.transform.position = new Vector3(0, -1, 25);
            mz3.GetComponent<Transform>().Rotate(90, 0, 0, relativeTo: Space.World);
        }
        if (gameObject.tag == "4")
        {
            mz4.transform.position = new Vector3(-1, -25, 0);
            mz4.GetComponent<Transform>().Rotate(0, 0, 90, relativeTo: Space.World);
        }
        if (gameObject.tag == "5")
        {
            mz5.transform.position = new Vector3(24, -26, 0);
            mz5.GetComponent<Transform>().Rotate(0, 0, 180, relativeTo: Space.World);
        }
    }

    // Update is called once per frame
    void Update ()
    {
	}
}
