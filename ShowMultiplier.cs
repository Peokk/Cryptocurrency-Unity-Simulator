using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShowMultiplier : MonoBehaviour
{
    private Vector3 target;

    public bool graph1;
    public bool graph2;
    public bool graph3;

    public Transform graph1Pos;
    public Transform graph2Pos;
    public Transform graph3Pos;

    public Text text;
    public GameObject parent;
    public CreateGraphs createGraps;


    public void MouseExit()
    {
        parent.transform.position = new Vector3(0, 0, 10000f);
    }

    public void TransformMouse()
    {
        if (graph1)
        {
            parent.transform.position = graph1Pos.position;
            text.text = Graph1Count().ToString() + " x";
        }

        if (graph2)
        {
            parent.transform.position = graph2Pos.position;

            text.text = Graph2Count().ToString() + " x";
        }

        if (graph3)
        {
            parent.transform.position = graph3Pos.position;
            text.text = ActualMultiplier().ToString() + " x";
        }
    }

    public float ActualMultiplier()
    {
        float actualMultiplier;
      
        actualMultiplier = createGraps.actualMultiplier;
       
        return actualMultiplier;    
    }

    public float Graph2Count()
    {
        float actualMultiplier;

        if (createGraps.pastBiggestMultiplarer[0] < 1)
        {
            actualMultiplier = (createGraps.pastBiggestMultiplarer[0] * 3f) * -1;
        }
        else
        {
            actualMultiplier = createGraps.pastBiggestMultiplarer[0];
        }

        return actualMultiplier;
    }

    public float Graph1Count()
    {
        float actualMultiplier;

        if (createGraps.pastBiggestMultiplarer[1] < 1)
        {
            actualMultiplier = (createGraps.pastBiggestMultiplarer[1] * 3f) * -1;
        }
        else
        {
            actualMultiplier = createGraps.pastBiggestMultiplarer[1];
        }

        return actualMultiplier;
    }
}
