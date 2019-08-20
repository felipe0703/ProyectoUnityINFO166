using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerarFondo : MonoBehaviour
{
    public GameObject[] spawnElements;
    public int cantASpawnear;
    public float rango = 300.0f;
    public float speed;
    public int rafaga;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawnear(1f, rafaga));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Spawnear(float time, int rafaga)
    {
        int cont = 0;
        yield return new WaitForSeconds(time/2);
        while (cont < cantASpawnear)
        {
            for(int i = 0; i < rafaga; ++i)
            {
                cont++;
                float r = Random.Range(-rango, rango);
                int ind = Random.Range(0, spawnElements.Length);
                GameObject gm = (GameObject)Instantiate(spawnElements[ind],this.transform.position + new Vector3(0, r, 0), this.transform.rotation,this.transform);
                float spd = Random.Range(speed, speed * 3);
                gm.GetComponent<Rigidbody2D>().velocity = new Vector2(-spd, 0);
            }
            yield return new WaitForSeconds(time);
        }
    }
}
