
using UnityEngine;
using System.Collections;

public class TowerController : MonoBehaviour
{

    public GameManager GameplayManager;
    public GameObject ArrowPrefab;
    public GameObject PlayerPrefab;

    public int Health = 10;
    public float ArrowInteraval = 3f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnArrows());
        GameplayManager.HealthTxt.text = $"Health - {Health}/10";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TowerTakeDmg()
    {
        Health = Health - 1;
        GameplayManager.HealthTxt.text = $"Health - {Health}/10";

        if (Health == 0)
        {
            GameplayManager.GameEnd();
        }
    }

    IEnumerator SpawnArrows()
    {
        while (true)
        {
            Instantiate(ArrowPrefab, transform.Find("ArrowSpawn").position,Quaternion.identity);
            yield return new WaitForSeconds(ArrowInteraval);
        }
    }

    public Vector3 SpawnPlayerLocation()
    {
        return transform.Find("PlayerSpawn").position;
    }

}
