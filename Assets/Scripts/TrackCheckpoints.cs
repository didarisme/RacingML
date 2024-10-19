using System;
using System.Collections.Generic;
using UnityEngine;

public class TrackCheckpoints : MonoBehaviour
{
    public class CarCheckPointEventArgs : EventArgs
    {
        public Transform carTransform { get; set; }
    }

    public event EventHandler<CarCheckPointEventArgs> OnCarCorrectCheckpoint;
    public event EventHandler<CarCheckPointEventArgs> OnCarWrongCheckpoint;

    [SerializeField] private List<Transform> carTransformList;

    private List<Checkpoint> checkpointList;
    private List<int> nextCheckpointIndexList;

    [SerializeField]

    private void Awake()
    {
        Transform checkpointsTransform = transform.Find("TrackCheckpoints");

        checkpointList = new List<Checkpoint>();
        foreach (Transform singleCheckpointTransform in checkpointsTransform)
        {
            Checkpoint checkpoint = singleCheckpointTransform.GetComponent<Checkpoint>();
            checkpoint.SetTrackCheckpoints(this);

            checkpointList.Add(checkpoint);
        }

        nextCheckpointIndexList = new List<int>();
        foreach (Transform carTransform in carTransformList)
        {
            nextCheckpointIndexList.Add(0);
        }
    }

    public void CarTroughCheckpoint(Checkpoint checkpoint, Transform carTransform)
    {
        int nextCheckpointIndex = nextCheckpointIndexList[carTransformList.IndexOf(carTransform)];

        if (checkpointList.IndexOf(checkpoint) == nextCheckpointIndex)
        {
            nextCheckpointIndexList[carTransformList.IndexOf(carTransform)] = (nextCheckpointIndex + 1) % checkpointList.Count;
            OnCarCorrectCheckpoint?.Invoke(this, new CarCheckPointEventArgs { carTransform = carTransform });
        }
        else
        {
            OnCarWrongCheckpoint?.Invoke(this, new CarCheckPointEventArgs { carTransform = carTransform });
        }
    }

    public void ResetCheckpoint(Transform carTransform)
    {
        int carIndex = carTransformList.IndexOf(carTransform);

        nextCheckpointIndexList[carIndex] = 0;
    }

    public Checkpoint GetNextCheckpoint(Transform carTransform)
    {
        int nextCheckpoint = nextCheckpointIndexList[carTransformList.IndexOf(carTransform)];
        return checkpointList[nextCheckpoint];
    }
}