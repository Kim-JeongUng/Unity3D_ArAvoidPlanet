using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject[] asteroid; // 0 : Bomb, 1 : invi, 2 : heal,  3 : Smaller , 4 : Asteroid

    public int randSp;
    public float timer;

    // Start is called before the first frame update
    void Start()
    {
    }
    private void Update()
    {
        if (GameObject.Find("ImageTarget").GetComponent<MyDefaultTrackableEventHandler>().isAttach)
        {
            timer += Time.deltaTime;
            if (timer > 1.0f)
            {
                timer = 0;
                SpawnAsteroid();
            }
        }
    }
    private void SpawnAsteroid()
    {
        GetComponent<AllSkill>().bomb = false;
        Vector3 pos = this.transform.position + new Vector3(Random.Range(-100, 100), 1000, Random.Range(-100, 100));
        randSp = Random.Range(0, 10);
        if (randSp > asteroid.Length-1)
            randSp = asteroid.Length -1;      // 장애물의 발생확률 증가시키기
        Debug.Log(randSp);
        GameObject asteroidPref = Instantiate(asteroid[randSp]);
        asteroidPref.transform.parent = GameObject.Find("ARCamera").transform;
        asteroidPref.transform.position=pos;
        asteroidPref.transform.LookAt(transform);
    }
}
