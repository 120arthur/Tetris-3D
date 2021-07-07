using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// This class is in charge of selecting and instantiating tretramino.
/// </summary>
public class TetrisSpawner : MonoBehaviour
{
    public static TetrisSpawner TetrisSpawnerInstance;

    // This matrix stores all pre-made tetris.
    [SerializeField] private GameObject[] tetrisPrefabs;

    // Store last tetris spawned index
    private int _previewTetris;

    private void Awake()
    {
        TetrisSpawnerInstance = this;
    }

    private void Start()
    {
        SpawnTetris();
    }

    // This method instantiates a random tetris.
    public void SpawnTetris()
    {
        while (true)
        {
            var tetrisToSpawn = SwitchNumber(0, tetrisPrefabs.Length);

            if (_previewTetris == tetrisToSpawn)
            {
                continue;
            }

            Instantiate(tetrisPrefabs[tetrisToSpawn], gameObject.transform.position, Quaternion.identity);
            _previewTetris = tetrisToSpawn;

            break;
        }
    }

    private int SwitchNumber(int min, int max)
    {
        return Random.Range(min, max);
    }
}