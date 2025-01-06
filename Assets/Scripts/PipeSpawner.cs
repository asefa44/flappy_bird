using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    [SerializeField] private float spawnTime = 1.4f;
    [SerializeField] private float maxHeigth = 1.6f;
    [SerializeField] GameObject pipe;
    public List<GameObject> pipes = new List<GameObject>();
    private float destroyTime = 5f;

    private void Start()
    {
        GameManager.instance.OnStartGame += SpawnPipeObject;
        GameManager.instance.OnRestartGame += DestroyAllPipes;
    }
    private void OnDestroy()
    {
        GameManager.instance.OnStartGame -= SpawnPipeObject;
        GameManager.instance.OnRestartGame -= DestroyAllPipes;
    }

    private void SpawnPipeObject()
    {
        StartCoroutine(SpawnPipe());
    }
    
    private void DestroyPipeObject(GameObject gameObject)
    {
        StartCoroutine(DestroyPipe(gameObject));
    }
    private IEnumerator SpawnPipe() 
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime);
            Vector3 spawnPosition = transform.position + new Vector3(0, Random.Range(-maxHeigth, maxHeigth));
            GameObject spawnedPipe = Instantiate(pipe, spawnPosition, Quaternion.identity, transform);
            pipes.Add(spawnedPipe);
            Debug.Log("Obje spawn edildi " + spawnedPipe.name);
            Debug.Log(pipes.Count);
            DestroyPipeObject(spawnedPipe);
        }  
    }

    private IEnumerator DestroyPipe(GameObject gameObject)
    {
        while (true)
        {
            yield return new WaitForSeconds(destroyTime);
            if(gameObject != null)
            {
                Destroy(gameObject);
                pipes.Remove(pipes[0]);
                Debug.Log("Obje yok edildi");
                Debug.Log(pipes.Count);
            }  
        }
    }

    public void DestroyAllPipes()
    {
        foreach (GameObject obj in pipes)
        {
            if (obj != null)
            {
                Destroy(obj);
            }
        }
        pipes.Clear();
    }
}
