using System.Collections.Generic;
using UnityEngine;

public class SnakeTail : MonoBehaviour
{
    public Transform SnakeHead;
    public float DistanceBetweenSegments;
    public int StartSegmentsCount;

    private List<Transform> snakeSegments = new List<Transform>();
    public List<Vector3> segmentPositions = new List<Vector3>();

    private int tailLenght;

    void Start()
    {
        segmentPositions.Add(SnakeHead.position);
        AddSegments(StartSegmentsCount);
    }

    void Update()
    {
        float distance = ((Vector3) SnakeHead.position - segmentPositions[0]).magnitude;

        if (distance > DistanceBetweenSegments)
        {
            Vector3 direction = ((Vector3)SnakeHead.position - segmentPositions[0]).normalized;

            segmentPositions.Insert(0, segmentPositions[0] + direction * DistanceBetweenSegments);
            segmentPositions.RemoveAt(segmentPositions.Count - 1);

            distance -= DistanceBetweenSegments;
        }

        for (int i = 0; i < snakeSegments.Count; i++)
        {
            snakeSegments[i].position = Vector3.Lerp(segmentPositions[i + 1], segmentPositions[i], distance / DistanceBetweenSegments);
        }
    }

    public void AddSegments(int points)
    {
        for (int i = 0; i < points; i++)
        {
            Transform segment = Instantiate(SnakeHead, segmentPositions[segmentPositions.Count - 1], Quaternion.identity);
            snakeSegments.Add(segment);
            segmentPositions.Add(segment.position);
            tailLenght++;
        }
    }

    public void RemoveSegment()
    {
        Debug.Log(tailLenght);
        Destroy(snakeSegments[tailLenght - 1].gameObject);
        snakeSegments.RemoveAt(tailLenght - 1);
        segmentPositions.RemoveAt(tailLenght);
        tailLenght--;
    }

    public void RemoveTail()
    {
        while (tailLenght > 0)
        {
            RemoveSegment();
        }
    }
}
