using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class caltrops : MonoBehaviour
{
    private float elapsed = 0f;
    private float[] sendEnemy = { 5f, 0f, 0f, 0f, 0f };

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().moveSpeed = 1f;
            elapsed += Time.deltaTime;
            if (elapsed >= 1f)
            {
                collision.gameObject.SendMessage("TakeDamage", sendEnemy);
                elapsed = 0f;
            }
        }
        if (collision.gameObject.CompareTag("grass"))
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().moveSpeed = 5f;
        }
    }
}
