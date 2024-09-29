using UnityEngine;

public class Bob : MonoBehaviour
{
    float yval;
    void Start() {
        yval = transform.localPosition.y;
    }
    // Update is called once per frame
    void Update()
    {
        this.transform.localPosition = new Vector3(this.transform.localPosition.x, yval+((Mathf.PerlinNoise1D(Time.time) * 0.1f)), this.transform.localPosition.z); 
    }
}
