using UnityEngine;

public class UFOSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject ufo;

    private float time = 0;

    [SerializeField]
    private float interval = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        SpawnUfo();
    }

    // Update is called once per frame
    void Update()
    {
        if (time > interval)
        {
            SpawnUfo();
            time = 0;
        }

        time += Time.deltaTime;
    }

    void SpawnUfo()
    {
        float x = Random.Range(-7f, 7f);
        float y = 6f;
        float scale = Random.Range(0.5f, 2f);

        GameObject newUfo = Instantiate(ufo, new Vector3(x, y), Quaternion.identity);
        newUfo.transform.localScale = new Vector3(scale, scale);
    }
}
