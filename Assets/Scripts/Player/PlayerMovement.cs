using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    GameObject gunPivot;
    [SerializeField]
    SpriteRenderer gun;
    [SerializeField]
    GameObject flash;
    [SerializeField]
    Rigidbody2D rb;
    [SerializeField]
    Transform reach;
    [SerializeField]
    Transform reachPivot;
    [SerializeField]
    Vector2 launchForce;
    [SerializeField]
    Vector2 maxVelocity;
    [SerializeField]
    LayerMask interactable;
    [SerializeField]
    float speedBarrier;
    [SerializeField]
    float rad;
    [SerializeField]
    TrailRenderer tra;
    [SerializeField]
     AudioClip sh;
    Vector2 prevPos;
    private bool shoot;
    Vector2 dir;
    float timer = 0; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        prevPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -0.7f) {
            transform.position = new Vector2(transform.position.x,-0.6f);
        }
     
        dir = (Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - gunPivot.transform.position).normalized;
        gunPivot.transform.eulerAngles = new Vector3(0,0,Mathf.Rad2Deg*Mathf.Atan2(dir.y,dir.x));
        gun.flipY = dir.x < 0;
        try
        {
            reachPivot.transform.eulerAngles = new Vector3(0, 0, Mathf.Rad2Deg * Mathf.Atan2(rb.linearVelocity.normalized.y, rb.linearVelocity.normalized.x));
        }
        catch (Exception e) {
            return;
        }
        tra.emitting = rb.linearVelocity.magnitude >= speedBarrier;
        if (!tra.emitting&& GameManager.rageMeter>0)
        {

            GameManager.timer -= Time.deltaTime;
            if (GameManager.timer <= 0)
            {
                GameManager.rageMeter = 0;
            }
        }
        else {
            GameManager.timer = GameManager.maxTime;
        }
    }
    private void FixedUpdate()
    {
        if (!shoot && flash.activeSelf) {
            timer += Time.fixedDeltaTime;
            if (timer >= 0.1f)
            {
                flash.SetActive(false);
                timer = 0; 
            }
        }
        else if (shoot)
        {
            shoot = false;
            flash.SetActive(true);
            AudioSource.PlayClipAtPoint(sh, this.transform.position);
            rb.AddForce(new Vector2(-dir.x * launchForce.x,0), ForceMode2D.Impulse);
            rb.linearVelocityY = -dir.y * launchForce.y;
        }
        rb.linearVelocityX = Mathf.Clamp(rb.linearVelocityX, -maxVelocity.x, maxVelocity.x);
        rb.linearVelocityY = Mathf.Clamp(rb.linearVelocityY, -maxVelocity.y, maxVelocity.y);
        prevPos = transform.position;
    }
    private void LateUpdate()
    {
        Collider2D[] hits1 = Physics2D.OverlapCircleAll(transform.position, rad, interactable);
        if (hits1.Length > 0)
        {
            foreach (Collider2D hit in hits1)
            {
                if (hit.tag == "Bullet")
                {
                    Destroy(hit.gameObject);
                    if (GameManager.rageMeter <= 0)
                    {
                        EndGame();
                    }
                    GameManager.rageMeter = 0;
                    continue;
                }
                if (rb.linearVelocity.magnitude < speedBarrier) { continue; }
                try
                {
                    hit.gameObject.GetInterface<ISpeedInteractable>().onHit(rb.linearVelocity, launchForce);
                }
                catch {
                    continue;
                }

            }
        }
        if (rb.linearVelocity.magnitude < speedBarrier) { return; }
        RaycastHit2D[] hits = Physics2D.LinecastAll(prevPos, reach.position, interactable);
        
        if (hits.Length > 0) {
            foreach (RaycastHit2D hit in hits) {
                if (hit.collider.tag == "Bullet")
                {
                   continue;
                }
                try
                {
                    hit.collider.gameObject.GetInterface<ISpeedInteractable>().onHit(rb.linearVelocity, launchForce);
                }
                catch
                {
                    continue;
                }

            }
        }
       
    }
    public void OnJump()
    { 
        shoot = !flash.activeSelf;
        
    }

    public void EndGame() {
        SceneManager.LoadScene("Lose");
    }
}
