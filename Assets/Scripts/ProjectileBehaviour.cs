using UnityEngine;
using System.Collections;

public class ProjectileBehaviour : MonoBehaviour
{
    Vector3 dir;
    public float speed;
    public float TimeToDie;
    public Transform target;
    
    void Start()
    {
        dir = Vector2.right;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(die());
    }
    private void Update()
    {
      
                float a = Vector3.Angle(this.transform.position, target.position);
                transform.eulerAngles = new Vector3(0, 0, -a);
                transform.position += (target.position - transform.position).normalized * speed * Time.deltaTime;
          
    }
    void LateUpdate()
    {
        
    }
   
    IEnumerator die()
    {
        yield return new WaitForSeconds(TimeToDie);
        Destroy(this.gameObject);
    }
}
