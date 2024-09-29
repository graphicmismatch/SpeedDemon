using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform target;
    private Vector3 effectiveTarget;
    public float followSharpness;
    public Vector3 offset;
    public bool lockx;
    public bool locky;
    public bool lockz;

    public float activationDistance;

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, target.position) < activationDistance||activationDistance<0) {
            return;
        }
        effectiveTarget = new Vector3((lockx)? transform.position.x:target.position.x, (locky) ? transform.position.y : target.position.y, (lockz) ? transform.position.z: target.position.z);
        transform.position += ((effectiveTarget + offset) - transform.position) * followSharpness;

    }
}
