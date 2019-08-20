using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportTo : MonoBehaviour
{
    public GameObject target;
    public float rango;
    public float speed;
    public GameObject[] spawnElements;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
        float r = Random.Range(-rango, rango);
        int ind = Random.Range(0, spawnElements.Length);
        GameObject gm = (GameObject)Instantiate(spawnElements[ind], target.transform.position + new Vector3(0, r, 0), target.transform.rotation, target.transform);
        float spd = Random.Range(speed, speed * 3);
        gm.GetComponent<Rigidbody2D>().velocity = new Vector2(-spd, 0);
    }
}
