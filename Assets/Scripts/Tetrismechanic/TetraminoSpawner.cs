using UnityEngine;
using Random = UnityEngine.Random;

namespace TetrisMechanic
{
    /// <summary>
    /// This class is in charge of selecting and instantiating tretramino.
    /// </summary>
    public class TetraminoSpawner : MonoBehaviour
    {
        // This matrix stores all pre-made tetris.
        [SerializeField] private GameObject[] tetrisPrefabs;

        public GameObject currentTetramino;
        // Store last tetris spawned index
        private int _previewTetris;


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

                if (_previewTetris == tetrisToSpawn) continue;
                
               currentTetramino = Instantiate(tetrisPrefabs[tetrisToSpawn], gameObject.transform.position, Quaternion.identity);
                _previewTetris = tetrisToSpawn;

                break;
            }
        }

        private int SwitchNumber(int min, int max) => Random.Range(min, max);
    }
}