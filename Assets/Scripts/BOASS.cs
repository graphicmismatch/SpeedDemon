using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class BOASS : MonoBehaviour, ISpeedInteractable
{
    public bool dead = false;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Collider2D col;
    public float health;
    public Image healthb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    public void onHit(Vector2 direction, Vector2 launchForce)
    {
        if (dead) { return; }
        health -= GameManager.rageMeter;
        GameManager.rageMeter = 0;
        healthb.fillAmount = health / 100;
        if (health <= 0)
        {
            dead = true;
            rb.bodyType = RigidbodyType2D.Dynamic;
            col.isTrigger = true;
            Vector2 dir = direction - 2 * direction * (Vector2.Dot(direction, Vector2.up));
            rb.AddForce(new Vector2(dir.x * launchForce.x, launchForce.y) * 0.2f * Time.deltaTime, ForceMode2D.Impulse);
            StartCoroutine(onWin());

        }

    }
    IEnumerator onWin() {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
        SceneManager.LoadScene("Win");
    }
}
