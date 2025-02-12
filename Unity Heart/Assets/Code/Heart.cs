using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    public GameObject heartPrefab;
    public int pointsCount = 50;
    public float scale = 0.5f;
    public float animationSpeed = 2f;

    private GameObject[] heartPoints;
    private float[] baseYPositions;
    // Start is called before the first frame update
    void Start()
    {
        heartPoints = new GameObject[pointsCount];
        baseYPositions = new float[pointsCount];

        for(int i = 0; i < pointsCount; i++) {
            float t = Mathf.PI * 2 * i / pointsCount;
            float x = 16 * Mathf.Pow(Mathf.Sin(t), 3);
            float y = 13 * Mathf.Cos(t) - 5 * Mathf.Cos(2*t) - 2 * Mathf.Cos(3*t) - Mathf.Cos(4*t);
            float z = 0;

            Vector3 position = new Vector3(x, y, z) * scale;
            heartPoints[i] = Instantiate(heartPrefab, position, Quaternion.identity, transform);
            baseYPositions[i] = position.y;
        }

        StartCoroutine(Animate());

    }

    IEnumerator Animate()
    {
        while(true) {
            for(int i = 0; i < heartPoints.Length; i++) {
                if(heartPoints[i] == null){
                    continue;
                }

                float offset = Mathf.Sin(Time.time * animationSpeed + i) * 0.5f;
                Vector3 newPos = heartPoints[i].transform.position;
                newPos.y = baseYPositions[i] + offset;
                heartPoints[i].transform.position = newPos;

                float colorValue = (Mathf.Sin(Time.time * 2 + i) + 1) / 2;
                heartPoints[i].GetComponent<Renderer>().material.color = Color.Lerp(Color.red, Color.magenta, colorValue);
            }

            yield return null;
        }
    }
}
