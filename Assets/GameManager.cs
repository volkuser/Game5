using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Platforms;
    public GameObject[] SegmentPrefab;
    private List<GameObject> Segments = new List<GameObject>();
    private GameObject LastSegment;
    
    public float speed = 0.5f;
    private int Tick = 0;
    
    public void Start()
    {
        LastSegment = GameObject.FindGameObjectWithTag("Start");
        Segments.Add(LastSegment);
        for (int i = 0; i < 4; i++)
        {
            GenerateSegment();
        }
    }
    
    public void FixedUpdate()
    {
        Platforms.transform
            .Translate(Vector2.down * speed);
        if (Tick == 150) 
        {
            GenerateSegment(); 
            Tick = 0;
        }
        
        if (Segments.Count > 50)
        {
            Destroy(Segments[0]);
            Segments.RemoveAt(0);
        }
    }
    
    private void GenerateSegment()
    {
        int index = Random.Range(0, SegmentPrefab.Length);
        GameObject SelectSegment = SegmentPrefab[index];

        foreach (Transform In in SelectSegment.transform)
        {
            if (In.tag == "In") 
            { 
                foreach (Transform Out in LastSegment.transform)
                {
                    if (Out.tag == "Out")
                    {
                        GameObject CurrentSegment 
                            = Instantiate(SelectSegment);
                        CurrentSegment.transform.position 
                            = Out.position - In.localPosition;
                        CurrentSegment.transform.parent 
                            = Platforms.transform;
                        Segments.Add(CurrentSegment);
                        LastSegment = CurrentSegment;
                    }        
                }
            }
        }
    }
}
