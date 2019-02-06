using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using MLAgents;
using System.Linq;

public class LASAgent : Agent
{
    public TextAsset csvFile;
    public GameObject prefab;

    public int nodeNum = 0;
    public List<GameObject> nodeList = new List<GameObject>();
    public List<GameObject> irList = new List<GameObject>();
    public List<GameObject> ledList = new List<GameObject>();
    public List<GameObject> smaList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        // Load nodes, where each node has:
        //     1 IR
        //     1 LED
        //     6 SMA
        LoadNodes();
    }

    void LoadNodes()
    {
        // Read .csv file
        var stringArray = csvFile.text.Split(',');
        int entryEachRow = 4;
        nodeNum = stringArray.Length / entryEachRow;
        Debug.Log(string.Format("Node number: {0}", nodeNum));
        for(var i = 1; i < nodeNum; i += 1)
        {
            // Get node name and position (x, y, z)
            string nodeName = stringArray[(i * entryEachRow) + 0];
            float x = float.Parse(stringArray[(i * entryEachRow) + 1]);
            float y = float.Parse(stringArray[(i * entryEachRow) + 2]);
            float z = float.Parse(stringArray[(i * entryEachRow) + 3]);
            Debug.Log(string.Format("Loading {0}: x = {1}, y = {2}, z = {3}", 
                                    nodeName,
                                    x,
                                    y,
                                    z));

            // Instantiate node, change parent, and name
            GameObject tmp_node = Instantiate(prefab, new Vector3(x, y, z), Quaternion.Euler(0, Random.Range(0.0f, 360.0f), 0));
            tmp_node.transform.parent = transform;
            GameObject tmp_ir = tmp_node.transform.Find("IR").gameObject;
            GameObject tmp_led = tmp_node.transform.Find("LED").gameObject;
            GameObject tmp_sma1 = tmp_node.transform.Find("SMA1").gameObject;
            GameObject tmp_sma2 = tmp_node.transform.Find("SMA2").gameObject;
            GameObject tmp_sma3 = tmp_node.transform.Find("SMA3").gameObject;
            GameObject tmp_sma4 = tmp_node.transform.Find("SMA4").gameObject;
            GameObject tmp_sma5 = tmp_node.transform.Find("SMA5").gameObject;
            GameObject tmp_sma6 = tmp_node.transform.Find("SMA6").gameObject;

            tmp_node.name = nodeName;
            tmp_ir.name = string.Format("{0}_{1}", nodeName, "IR");
            tmp_led.name = string.Format("{0}_{1}", nodeName, "LED");
            tmp_sma1.name = string.Format("{0}_{1}", nodeName, "SMA1");
            tmp_sma2.name = string.Format("{0}_{1}", nodeName, "SMA2");
            tmp_sma3.name = string.Format("{0}_{1}", nodeName, "SMA3");
            tmp_sma4.name = string.Format("{0}_{1}", nodeName, "SMA4");
            tmp_sma5.name = string.Format("{0}_{1}", nodeName, "SMA5");
            tmp_sma6.name = string.Format("{0}_{1}", nodeName, "SMA6");

            nodeList.Add(tmp_node);
            irList.Add(tmp_ir);
            ledList.Add(tmp_led);
            smaList.Add(tmp_sma1);
            smaList.Add(tmp_sma2);
            smaList.Add(tmp_sma3);
            smaList.Add(tmp_sma4);
            smaList.Add(tmp_sma5);
            smaList.Add(tmp_sma6);
        }
    }

    // Get Observation
    public override void CollectObservations()
    {
        foreach (GameObject ir in irList)
        {
            // Get irReading [0,1] corresponding to [maximum, minimum]
            float irReading = ir.transform.Find("IR Detection Area").GetComponent<IRDistanceCalculate>().GetIRReading();
            AddVectorObs(irReading);
            //Debug.Log(string.Format("{0}: {1}", ir.name, irReading));
        }
    }

    // Take Action and Add Reward
    public override void AgentAction(float[] vectorAction, string textAction)
    {
        // Clamp the values of action into [-1, 1]
        for(int i = 0; i < vectorAction.Length; i += 1)
        {
            vectorAction[i] = Mathf.Clamp(vectorAction[i], -1, 1);
        }

        int ledNum = ledList.Count;
        int smaNum = smaList.Count;
        Debug.Log(string.Format("length of LED: {0}", ledNum));
        Debug.Log(string.Format("length of SMA: {0}", smaNum));

        // Take Actions on LEDs
        for(int i = 0; i < ledNum; i += 1)
        {
            ledList[i].GetComponent<LEDLightIntensity>().SetLedIntensity(vectorAction[i]);
        }

        // Take Actions on SMAs
        for(int i = 0; i < smaNum; i += 1)
        {
            smaList[i].GetComponent<AnimateSMAContraction>().SetSMAAction(vectorAction[ledNum + i]);
        }

        // Reward functioon
        float[] irReading = new float[irList.Count];
        for(int i = 0; i < irList.Count; i += 1)
        {
            irReading[i] = irList[i].transform.Find("IR Detection Area").GetComponent<IRDistanceCalculate>().GetIRReading();
        }
        float reward_value = irReading.Sum();
        AddReward(reward_value);
        Debug.Log(string.Format("reward={0}", reward_value));
    }

    public override void AgentReset()
    {
        Debug.Log(string.Format("Reset Agent: {0}", gameObject.name));

        float[] resetAction = new float[(ledList.Count+ smaList.Count)];
        this.AgentAction(resetAction, "");
    }

    // Update is called once per frame
    void Update()
    {
        // Controll all LEDs
        if (Input.GetKeyDown(KeyCode.W))
        {
            for (int i = 0; i < ledList.Count(); i += 1)
            {
                ledList[i].GetComponent<LEDLightIntensity>().SetLedIntensity(1.0f);
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            for (int i = 0; i < ledList.Count(); i += 1)
            {
                ledList[i].GetComponent<LEDLightIntensity>().SetLedIntensity(0.0f);
            }
        }

        // Controll all SMAs
        if (Input.GetKeyDown(KeyCode.A))
        {
            for (int i = 0; i < smaList.Count(); i += 1)
            {
                smaList[i].GetComponent<AnimateSMAContraction>().SetSMAAction(1);
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            for (int i = 0; i < smaList.Count(); i += 1)
            {
                //smaList[i].transform.Find("bend").GetComponent<Bend>().curvature = 0;
                smaList[i].GetComponent<AnimateSMAContraction>().SetSMAAction(0);
            }
        }
    }
}
