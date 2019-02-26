using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class VisitorAgent : Agent
{
    public override void CollectObservations()
    {
        AddVectorObs(0);
    }

    // Take Action and Add Reward
    public override void AgentAction(float[] vectorAction, string textAction)
    {

        Debug.Log("AgentAction");

        AddReward(0);
        Debug.Log("Calculate Reward");
    }

    public override void AgentReset()
    {
        Debug.Log("AgentReset");
    }
}
