using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class DropAgent : Agent
{
    private StageManager stageManager = null;

    private Rigidbody rigidBody = null;

    [SerializeField]
    private Material goodMaterial = null;
    [SerializeField]
    private Material badMaterial = null;

    [SerializeField]
    private float moveSpeed = 10f;

    private float maxPosition = 7f;
    private Vector3 tempPos = Vector3.zero;

    private bool isDead = false;

    private void Update()
    {
        if (transform.position.x > maxPosition || transform.position.x < -maxPosition)
        {
            tempPos = transform.position;
            tempPos.x = transform.position.x > 0f ? maxPosition : -maxPosition;
            transform.position = tempPos;
        }

        if (transform.position.z > maxPosition || transform.position.z < -maxPosition)
        {
            tempPos = transform.position;
            tempPos.z = transform.position.z > 0f ? maxPosition : -maxPosition;
            transform.position = tempPos;
        }
    }


    public override void Initialize()
    {
        stageManager = transform.parent.GetComponent<StageManager>();
        stageManager.Init();

        rigidBody = GetComponent<Rigidbody>();
    }

    public override void OnEpisodeBegin()
    {
        rigidBody.velocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;

        transform.localPosition = new Vector3(0f, 60.05f, 0f);
        transform.rotation = Quaternion.identity;

        isDead = false;

        stageManager.StageSet();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(rigidBody.velocity);
        base.CollectObservations(sensor);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        actionsOut.ContinuousActions.Array[0] = Input.GetAxis("Horizontal");
        actionsOut.ContinuousActions.Array[1] = Input.GetAxis("Vertical");
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        if (isDead) return;
        float h = Mathf.Clamp(actions.ContinuousActions[0], -1.0f, 1.0f);
        float v = Mathf.Clamp(actions.ContinuousActions[1], -1.0f, 1.0f);

        rigidBody.velocity += new Vector3(h, 0, v);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("DEAD"))
        {
            SetReward(-0.5f);
            stageManager.text.SetText("DIE");
            stageManager.AllObjectStop();
            stageManager.FloorColor(badMaterial);
            isDead = true;
            StartCoroutine(DelayEnd());
        }
        else if (collision.collider.CompareTag("BULLET"))
        {
            SetReward(-0.05f);
        }
        else if (collision.collider.CompareTag("END_FLOOR"))
        {
            SetReward(1.0f);
            stageManager.text.SetText("GOOD");
            stageManager.AllObjectStop();
            stageManager.FloorColor(goodMaterial);
            isDead = true;
            StartCoroutine(DelayEnd());
        }
    }
    private IEnumerator DelayEnd()
    {
        yield return new WaitForSeconds(0.5f);
        EndEpisode();
    }
}