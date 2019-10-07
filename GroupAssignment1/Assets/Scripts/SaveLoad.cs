using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
public class SaveLoad : MonoBehaviour
{
    const string DLL_NAME = "tut2";
    float[] location;

    [DllImport(DLL_NAME)]
    static extern void locSave([In, Out] Vector4[] vecArray, int vecSize);
    [DllImport(DLL_NAME)]
    static extern void locLoad();
    [DllImport(DLL_NAME)]
    static extern int getSize();
    [DllImport(DLL_NAME)]
    static extern System.IntPtr getPos();


    List<Vector4> test = new List<Vector4>();


    public GameObject Hole;
    public GameObject Wall;

    List<GameObject> holeList = new List<GameObject>();
    List<GameObject> wallList = new List<GameObject>();

    Factory factory;
    void Start()
    {
        factory = GetComponent<Factory>();
    }

    void Update()
    {

        if (Input.GetKeyUp(KeyCode.P))
        {
            Loadlocation();
        }

    }

    public void Loadlocation()
    {
        location = new float[getSize()];

        locLoad();

        Marshal.Copy(getPos(), location, 0, getSize());

        int wallNum = 0, holeNum = 0;

            for (int i = 0; i < getSize(); i += 4)
            {
                Debug.Log(location[i + 3]);

                if (location[i + 3] == 1.0f)
                {
                    Debug.Log("Wall Spawn");
                    wallList.Add(factory.Spawn(Wall));
                    wallList[wallNum].transform.localPosition = new Vector3(location[i], location[i + 1], location[i + 2]);

                    wallNum++;
                }
                else if (location[i + 3] == 2.0f)
                {
                    Debug.Log("Hole Spawn");
                    holeList.Add(factory.Spawn(Hole)); ;

                    holeList[holeNum].transform.localPosition = new Vector3(location[i], location[i + 1], location[i + 2]);

                    holeNum++;
                }
            }
        }

    }

