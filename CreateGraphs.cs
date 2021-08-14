using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGraphs : MonoBehaviour
{
    public List<float> pastBiggestMultiplarer;
    public List<float> numbers;

    public GameObject graph1GO;
    public GameObject graph2GO;
    public GameObject graph3GO;

    public bool up;
    public bool down;
    public bool keep;

    public float actualMultiplier;
    public float biggestMultiplier = -100;

    public bool procesPerformed = false;
    float change = 0;
    public float houersCounter = 3550;

    public float graphPosition;
    private float graphPositionY;

    private void Start()
    {
        graphPosition = graph3GO.transform.position.y;
    }

    private void Update()
    {
        SetAcutalGraph();

        SetGraphPos();
        SetBiggestMultiplier();
        SetPastBiggestMultiplarer();
        SetPastGraphs();
    }

    private float GRN(int maxNumber, int minNumber)
    {
        return UnityEngine.Random.Range(minNumber, maxNumber);
    }

    private void SetAcutalGraph()
    {
        if (procesPerformed == false)
        {
            WhatDo();
            StartCoroutine(KeepTheTime(HowMuchTimeKeep()));
            HowMuchChange();

            procesPerformed = true;
        }
    }

    private void WhatDo()
    {
        float randomNumber = GRN(3600,0);

        up = false;
        down = false;
        keep = false;

        if (randomNumber > 0 && randomNumber < 80){
            up = true;
        }

        if (randomNumber > 80 && randomNumber < 200){
            down = true;
        }

        if (randomNumber > 350 && randomNumber < 3601){
            keep = true;
        }
    }

    private void HowMuchChange()
    {
        if (up)
        {
            change = GRN(1000,0) / 100.0f;
            actualMultiplier = 1f + change;
        }

        if (keep)
        {
            change = GRN(15,-15) / 100.0f;

            actualMultiplier = 1f + change;
        }

        if (down)
        {

            change = GRN(1000,0) / 100.0f;

            if (actualMultiplier > 0)
            {
                actualMultiplier = 1f - change / 3f;            
            }
        }

        numbers.Add(actualMultiplier);
    }

    private void SetPastBiggestMultiplarer()
    {
        if (houersCounter > 3600)
        {
            if (pastBiggestMultiplarer.Count == 2)
            {
                pastBiggestMultiplarer.Add(biggestMultiplier);
                pastBiggestMultiplarer.Add(pastBiggestMultiplarer[0]);
                pastBiggestMultiplarer.RemoveAt(0);
                pastBiggestMultiplarer.RemoveAt(0);
                biggestMultiplier = -100;
            }

            if (pastBiggestMultiplarer.Count == 1)
            {
                pastBiggestMultiplarer.Add(biggestMultiplier);
                pastBiggestMultiplarer.Add(pastBiggestMultiplarer[0]);
                pastBiggestMultiplarer.RemoveAt(0);
                biggestMultiplier = -100;
            }

            if (pastBiggestMultiplarer.Count == 0)
            {
                pastBiggestMultiplarer.Add(biggestMultiplier);
                biggestMultiplier = -100;
            }

            houersCounter = 0;
        }
    }

    public void SetPastGraphs()
    {
        float time = 0.7f;

        if (pastBiggestMultiplarer.Count > 1)
        {
            float _graphPositionY = graphPosition + pastBiggestMultiplarer[1] * 20;
            graph1GO.transform.position = Vector2.Lerp(graph1GO.transform.position, new Vector2(graph1GO.transform.position.x, _graphPositionY), time * Time.deltaTime);
        }
        if(pastBiggestMultiplarer.Count > 0)
        {
            float _graphPositionY = graphPosition + pastBiggestMultiplarer[0] * 20;
            graph2GO.transform.position = Vector2.Lerp(graph2GO.transform.position, new Vector2(graph2GO.transform.position.x, _graphPositionY), time * Time.deltaTime);
        }
    }

    private void SetBiggestMultiplier()
    {
        for (int i = 0; i < numbers.Count; i++)
        {
            if(numbers[i] > biggestMultiplier)
            {
                biggestMultiplier = numbers[i];
            }
            else if (numbers[i] < biggestMultiplier)
            {
                numbers.RemoveAt(i);
            }
        }
    }

    private void SetGraphPos()
    {
        float time = 0.8f;

        graphPositionY = graphPosition + actualMultiplier * 20;

        graph3GO.transform.position = Vector2.Lerp(graph3GO.transform.position, new Vector2(graph3GO.transform.position.x, graphPositionY), time * Time.deltaTime);
    }

    private float HowMuchTimeKeep()
    {
        float time = GRN(3600, 30);

        return time;
    }

    private IEnumerator KeepTheTime(float time)
    {
        yield return new WaitForSeconds(time);

        houersCounter += time;
        procesPerformed = false;
    }
}
