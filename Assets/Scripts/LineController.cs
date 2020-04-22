using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    public Transform lineParent, lastSegment;
    public GameObject goodSegment, badSegment;
    public FloatVariable speed;

    bool goodSeg = true;

    List<Transform> segments = new List<Transform>();

    private void Start()
    {
        SpawnStartingLine();
    }

    private void Update()
    {
        for (int i = segments.Count - 1; i >= 0; i--)
        {
            segments[i].position -= new Vector3(0, speed.RuntimeValue * Time.deltaTime);
            if (segments[i].position.y < -15f)
            {
                SpawnNewSegment();
                var segToDestroy = segments[i];
                segments.Remove(segments[i]);
                Destroy(segToDestroy.gameObject);
            }
        }
    }

    void SpawnStartingLine()
    {
        if (segments.Count > 0)
        {
            for (int i = segments.Count - 1; i >= 0; i--)
            {
                var seg = segments[i];
                segments.Remove(seg);
                Destroy(seg.gameObject);
            }
        }

        float startSize = 30;
        float currSize = startSize;

        while (currSize > 0)
        {
            var newSegment = Instantiate(goodSeg ? goodSegment : badSegment, lineParent.position + new Vector3 (0, startSize/2 - currSize), Quaternion.identity, lineParent);
            var tempScale = newSegment.transform.localScale;
            tempScale.y = goodSeg ? Random.Range(1f, 3f) : Random.Range(0.5f, 2f);
            newSegment.transform.localScale = tempScale;
            currSize -= tempScale.y;
            goodSeg = !goodSeg;
            lastSegment = newSegment.transform;
            segments.Add(newSegment.transform);
        }
    }

    void SpawnNewSegment ()
    {
        var newSegment = Instantiate(goodSeg ? goodSegment : badSegment, lastSegment.position + new Vector3 (0, lastSegment.localScale.y), Quaternion.identity, lineParent);
        var tempScale = newSegment.transform.localScale;
        tempScale.y = goodSeg ? Random.Range(1f, 3f) : Random.Range(0.5f, 2f);
        newSegment.transform.localScale = tempScale;
        goodSeg = !goodSeg;
        lastSegment = newSegment.transform;
        segments.Add(newSegment.transform);
    }

    public void Stop ()
    {
        speed.RuntimeValue = 0f;
    }

    public void Restart()
    {
        SpawnStartingLine();
        speed.RuntimeValue = speed.InitialValue;
    }
}
