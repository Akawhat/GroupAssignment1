using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Command : MonoBehaviour
{

    public bool chosen;
    LevelEditor level;
    Transform selectedTrans;

    public void Chosen(bool select, Transform trans)
    {
        chosen = select;
        selectedTrans = trans;
    }

    void Start()
    {
        level = GetComponent<LevelEditor>();
        UndoList = new List<setting>();
    }

    public class setting
    {
        public GameObject Obj;

        public Vector3 Pos;
        public Quaternion Rot;

        public bool Deleted;
        public setting(GameObject g)
        {
            Obj = g;
            Pos = g.transform.position;
            Rot = g.transform.rotation;
            Deleted = g.activeSelf;

        }

        public void Restore()
        {
            Obj.transform.position = Pos;
            Obj.transform.rotation = Rot;
            Obj.SetActive(Deleted);
        }

    }

    public List<setting> UndoList;

    public void AddUndo(GameObject g)
    {
        setting s = new setting(g);
        UndoList.Add(s);
    }

    public void Undo()
    {
        if (UndoList.Count > 0)
        {
            UndoList[UndoList.Count - 1].Restore();
            UndoList.RemoveAt(UndoList.Count - 1);
        }
    }
}
