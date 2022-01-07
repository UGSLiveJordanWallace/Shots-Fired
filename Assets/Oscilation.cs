using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscilation : MonoBehaviour
{
    [SerializeField] Vector3 Vector = new Vector3(10f, 10f, 10f);
    [SerializeField] float period = 2f;
    [SerializeField] Transform playerPos;

    [Range(0,1)] [SerializeField] private float movementFactor;
    Vector3 startingPoint;
    // Start is called before the first frame update
    void Start()
    {
        startingPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon) { return; }
        //Set Movement Factor
        float cycles = Time.time / period; //grows continually from zero

        const float tau = Mathf.PI * 2; // about 6.28
        float rawSinWave = Mathf.Sin(cycles * tau);

        movementFactor = rawSinWave / 2f + 0.5f;
        Vector3 offset = Vector * movementFactor;
        transform.position = playerPos.position + offset + new Vector3(15, 0, 1);
    }
}
