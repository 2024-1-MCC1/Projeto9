using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public Transform Target;
    public float within_range;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(Target.position, transform.position);
        transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, speed);
    }
}
