using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class AgentController : Agent
{
    Rigidbody rBodyAgent;
    Rigidbody rBodyBall;

    void Start()
    {
        rBodyAgent = GetComponent<Rigidbody>();
        rBodyBall = Ball.GetComponent<Rigidbody>();
    }

    public Transform Target;
    public Transform Ball;

    public override void OnEpisodeBegin()
    {
        // If either the agent or the ball fell, zero its momentum
        if (this.transform.localPosition.y < 0 || Ball.localPosition.y < 0)
        {
            this.rBodyAgent.angularVelocity = Vector3.zero;
            this.rBodyAgent.velocity = Vector3.zero;
            this.transform.localPosition = new Vector3(Random.Range(3.0f, 13.0f),
                                                       0.5f,
                                                       Random.Range(-8.0f, 8.0f));

            this.transform.LookAt(Ball);
            rBodyBall.angularVelocity = Vector3.zero;
            rBodyBall.velocity = Vector3.zero;
        }

        // Move  the target to a new spot, reset ball location to 0
        this.transform.localPosition = new Vector3(Random.Range(3.0f, 13.0f),
                                                   0.5f,
                                                    Random.Range(-8.0f, 8.0f));
        this.transform.LookAt(Ball);
        Target.localPosition = new Vector3(Random.Range(-13.0f, -3.0f),
                                           0.5f,
                                           Random.Range(-8.0f, 8.0f));
        Ball.transform.localPosition = new Vector3(0, 0.5f, 0);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Target, Ball, and Agent Positions
        sensor.AddObservation(Target.localPosition);
        sensor.AddObservation(Ball.localPosition);
        sensor.AddObservation(this.transform.localPosition);

        // Agent Velocity
        sensor.AddObservation(rBodyAgent.velocity.x);
        sensor.AddObservation(rBodyAgent.velocity.z);
    }

    public float speedMultiplier = 10;
    public override void OnActionReceived(ActionBuffers actions)
    {
        // Actions for moving and rotating
        Vector3 controlSignal = Vector3.zero;
        Vector3 rotateDir = Vector3.zero;

        controlSignal.x = actions.ContinuousActions[0];
        controlSignal.z = actions.ContinuousActions[1];
        rotateDir.y = actions.ContinuousActions[2];

        transform.Translate(controlSignal * Time.deltaTime * speedMultiplier);
        transform.Rotate(rotateDir, Time.deltaTime * 100f);

        // Rewards
        float distanceToTarget = Vector3.Distance(Ball.localPosition, Target.localPosition);
        float distanceToBall = Vector3.Distance(Ball.localPosition, this.transform.localPosition);

        if (distanceToBall < 1.42f)
        {
            SetReward(0.05f);
        }

        if (distanceToTarget < 1.42f)
        {
            SetReward(1.0f);
            EndEpisode();
        }
        else if (this.transform.localPosition.y < 0)
        {
            EndEpisode();
        }
        else if (Ball.localPosition.y < 0)
        {
            EndEpisode();
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        if (Input.GetKey(KeyCode.W))
        {
            continuousActionsOut[0] = -1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            continuousActionsOut[0] = 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            continuousActionsOut[1] = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            continuousActionsOut[1] = 1;
        }
        if (Input.GetKey(KeyCode.E))
        {
            continuousActionsOut[2] = 1;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            continuousActionsOut[2] = -1;
        }
    }
}
