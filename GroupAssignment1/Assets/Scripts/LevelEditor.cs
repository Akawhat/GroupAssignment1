using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class LevelEditor : MonoBehaviour
{
    const string DLL_NAME = "tut2";

    [DllImport(DLL_NAME)]
    static extern void saveLocation([In, Out] Vector4[] vecArray, int vecSize);
    [DllImport(DLL_NAME)]
    static extern void loadLocation();
    [DllImport(DLL_NAME)]
    static extern int getSize();
    [DllImport(DLL_NAME)]
    static extern System.IntPtr getPosition();

    int size;
    float[] location;
    
    // game object variables 
    public GameObject hole;
    public GameObject wall;
    public GameObject temp;

    public Factory factory;

   
    List<Vector4> test = new List<Vector4>();
    Vector4 tempVec;

    static private List<GameObject> holeList = new List<GameObject>();
    static private List<GameObject> wallList = new List<GameObject>();
    public void saveLocation()
    {
   
        for (int i = 0; i < wallList.Count; i++)
        {
            tempVec = new Vector4(wallList[i].transform.localPosition.x, wallList[i].transform.localPosition.y, wallList[i].transform.localPosition.z, 1.0f);
            test.Add(tempVec);
        }

        for (int i = 0; i < holeList.Count; i++)
        {
            tempVec = new Vector4(holeList[i].transform.localPosition.x, holeList[i].transform.localPosition.y, holeList[i].transform.localPosition.z, 2.0f);
            test.Add(tempVec);
        }

        test.ToArray();

        saveLocation(test.ToArray(), (test.Count * 4));

    }

    public void LoadLocation()
    {
        holeList.Clear();
        wallList.Clear();
       
        size = getSize();

        Debug.Log(size);

        location  = new float[size];

        loadLocation();

        Marshal.Copy(getPosition (), location , 0, size);

        int wallNum = 0, holeNum = 0;

        for (int i = 0; i < size; i += 4)
        {
            if (location [i + 3] == 1.0f)
            {
                wallList.Add(factory.Spawn(wall));
                wallList[wallNum].transform.localPosition = new Vector3(location [i], location [i + 1], location [i + 2]);

                wallNum++;
            }
            else if (location [i + 3] == 2.0f)
            {
                holeList.Add(factory.Spawn(hole)); ;

                holeList[holeNum].transform.localPosition = new Vector3(location [i], location [i + 1], location [i + 2]);

                holeNum++;
            }
        }
    }

    void Start() // start function is initiated before the first frame 
    {
        factory = GetComponent<Factory>();
    }

    void Update()
    {
        size = (test.Count * 4);
    }

    public void addHole() // adds the hole object to the game scene 
    {
        temp = factory.Spawn(hole);
        holeList.Add(temp);
    }

    public void addWall() // adds the wall objects to the scene 
    {
        temp = factory.Spawn(wall);
        wallList.Add(temp);
    }


}
