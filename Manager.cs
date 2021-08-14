using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using System.Linq;


public class Manager : MonoBehaviour
{
    public CreateGraphs createGraphs;

    public GameObject video;
    public GameObject startMinningButton;
    public GameObject background;

    public Text howMuchPerMinuteText;
    public Text yourPopuText;
    public Text howMuchUpgradeCost;
    public Text addedPopusCountText;
    public Text color;
    public Text howMouchHaveSelledText;

    public InputField addCountText;
    public InputField howMuchSellText;

    string popuPerMinuteLocalization;
    string yourPopuCountLocalization;
    string upgradeCounterLocalization;
    string addCounterLocalization;
    string howMuchSelledLocalization;

    private List<string> lines1 = new List<string>();
    private List<string> lines2 = new List<string>();
    private List<string> lines3 = new List<string>();
    private List<string> lines4 = new List<string>();
    private List<string> lines5 = new List<string>();


    public bool miningStarted;

    public float yourPopu;
    public float popuPerMinute;
    public float upgradesCount = 0;
    public float addCount;
    public float addedPopusCounter;
    public float howMuchSelledCount;

    public float money = 0;
    public int jd = 0;


    private void Start()
    {
        UpdatePopus();

        miningStarted = false;

        yourPopuCountLocalization = @"C:\KoparkaPopu\MonoBleedingEdge\etc\mono\2.0\Browsers\yourPopu.txt";
        popuPerMinuteLocalization = @"C:\KoparkaPopu\MonoBleedingEdge\etc\mono\2.0\Browsers\popuPerMinute.txt";
        upgradeCounterLocalization = @"C:\KoparkaPopu\MonoBleedingEdge\etc\mono\2.0\Browsers\upgradesCount.txt";
        addCounterLocalization = @"C:\KoparkaPopu\MonoBleedingEdge\etc\mono\2.0\Browsers\addCounter.txt";
        howMuchSelledLocalization = @"C:\KoparkaPopu\MonoBleedingEdge\etc\mono\2.0\Browsers\howMuchSelled.txt";

        lines3 = new List<string>();
        lines3 = File.ReadAllLines(upgradeCounterLocalization).ToList();

        video.SetActive(false);
        startMinningButton.SetActive(true);
        background.SetActive(true);

        foreach (var counter in lines3)
        {
            upgradesCount = (float.Parse(counter));
        }
    }

    private void Update()
    {
        UpdatePopus();

        lines1 = new List<string>();
        lines1 = File.ReadAllLines(yourPopuCountLocalization).ToList();

        lines2 = new List<string>();
        lines2 = File.ReadAllLines(popuPerMinuteLocalization).ToList();
        File.WriteAllLines(popuPerMinuteLocalization, lines2);

        lines3 = new List<string>();
        lines3 = File.ReadAllLines(upgradeCounterLocalization).ToList();

        lines4 = new List<string>();
        lines4 = File.ReadAllLines(addCounterLocalization).ToList();

        lines5 = new List<string>();
        lines5 = File.ReadAllLines(howMuchSelledLocalization).ToList();

        howMuchPerMinuteText.text = popuPerMinute.ToString() + "/min";
        yourPopuText.text = yourPopu.ToString() + " $";

        float upgradeCost = 200 + 200 * upgradesCount * 13f / 100;
        howMuchUpgradeCost.text = "Cost " + upgradeCost.ToString();

        addedPopusCountText.text = addedPopusCounter.ToString();

        howMouchHaveSelledText.text = howMuchSelledCount.ToString();
    }

    public void SellPopus()
    {
        if(yourPopu >= (float.Parse(howMuchSellText.text)) && (float.Parse(howMuchSellText.text)) > 0)
        {
            float sellCount = (int.Parse(howMuchSellText.text));

            sellCount = sellCount * createGraphs.actualMultiplier;

            if (createGraphs.down)
            {
                sellCount = (sellCount / createGraphs.actualMultiplier) * createGraphs.actualMultiplier * 3;
            }

            lines1.RemoveAt(0);
            lines1.Add((yourPopu - sellCount / createGraphs.actualMultiplier).ToString());
            yourPopu -= sellCount / createGraphs.actualMultiplier; ;
            File.WriteAllLines(yourPopuCountLocalization, lines1);

            howMuchSelledCount += sellCount;
            lines5.RemoveAt(0);
            lines5.Add((howMuchSelledCount).ToString());
            File.WriteAllLines(howMuchSelledLocalization, lines5);
        }      
    }

    void mine()
    {
        yourPopu += popuPerMinute / 60;
        lines1.RemoveAt(0);
        lines1.Add(yourPopu.ToString());
        File.WriteAllLines(yourPopuCountLocalization, lines1);
    }

    public void AddPopus()
    {
        addCount = (float.Parse(addCountText.text));
        addCountText.text = "";

        yourPopu += addCount / (createGraphs.actualMultiplier + 0.07f);
        lines1.RemoveAt(0);
        lines1.Add(yourPopu.ToString());
        File.WriteAllLines(yourPopuCountLocalization, lines1);

        lines4.RemoveAt(0);
        lines4.Add((addedPopusCounter + 1).ToString());
        File.WriteAllLines(addCounterLocalization, lines4);
    }

    public void UpdatePopuPerMinute()
    {
        float upgradeCost = 200 + 200 * upgradesCount * 13f / 100;

        if (yourPopu >= upgradeCost)
        {
            lines3.RemoveAt(0);
            lines3.Add(upgradesCount.ToString());
            File.WriteAllLines(upgradeCounterLocalization, lines3);

            yourPopu -= 200 + 200 * upgradesCount * 13f / 100;
            lines1.RemoveAt(0);
            lines1.Add(yourPopu.ToString());
            File.WriteAllLines(yourPopuCountLocalization, lines1);

            upgradesCount++;

            popuPerMinute++;
            lines2.RemoveAt(0);
            lines2.Add(popuPerMinute.ToString());
            File.WriteAllLines(popuPerMinuteLocalization, lines2);
        }
    }

    public void start()
    {
        InvokeRepeating("mine", 1f, 1f);
    }

    public void UpgradePopuPerMinute(float howMuchAdd)
    {
        popuPerMinute += howMuchAdd;
    }

    public void StartMinningVideo()
    {
        video.SetActive(true);
        startMinningButton.SetActive(false);
        background.SetActive(false);
        miningStarted = true;
        addedPopusCountText.color = color.color;
    }

    public void UpdatePopus()
    {
        foreach (var yp in lines1)
        {
            yourPopu = (float.Parse(yp));
        }

        foreach (var ppm in lines2)
        {
            popuPerMinute = (float.Parse(ppm));
        }

        foreach (var counter in lines4)
        {
           addedPopusCounter = (float.Parse(counter));
        }

        foreach (var counter in lines5)
        {
            howMuchSelledCount = (float.Parse(counter));
        }
    }
}
