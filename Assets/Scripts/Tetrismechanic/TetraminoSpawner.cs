using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Zenject;
using Random = UnityEngine.Random;


namespace TetrisMechanic
{
    /// <summary>
    /// This class is in charge of selecting and instantiating tretramino.
    /// </summary>
    public class TetraminoSpawner : MonoBehaviour
    {
        [Inject]
        private IInstantiator m_Instantiator;

        private AsyncOperationHandle<IList<GameObject>> m_TetraminosLoadOpHandle;
        private List<string> m_Keys = new List<string>() { "Tetraminos" };

        public GameObject m_CurrentTetramino;
        // Store last tetris spawned index
        private int m_previewTetris;

        private void Start()
        {
            m_TetraminosLoadOpHandle = Addressables.LoadAssetsAsync<GameObject>(m_Keys, null, Addressables.MergeMode.Union);
            m_TetraminosLoadOpHandle.Completed += OnTetraminosLadComplete;
        }

        // This method instantiates a random tetris.
        public void SpawnTetris()
        {
            if (m_TetraminosLoadOpHandle.Status != AsyncOperationStatus.Succeeded)
            {
                return;
            }

            while (true)
            {
                var tetrisToSpawn = SwitchNumber(0, m_TetraminosLoadOpHandle.Result.Count);

                if (m_previewTetris == tetrisToSpawn) continue;

                GameObject tetramino = m_TetraminosLoadOpHandle.Result[tetrisToSpawn];
                m_CurrentTetramino = m_Instantiator.InstantiatePrefab(tetramino, gameObject.transform.position, Quaternion.identity, gameObject.transform);
                m_previewTetris = tetrisToSpawn;

                break;
            }
        }

        private int SwitchNumber(int min, int max) => Random.Range(min, max);

        private void OnTetraminosLadComplete(AsyncOperationHandle<IList<GameObject>> asyncOperationHandle)
        {
            Debug.Log("AsyncOperationHandle Status: " + asyncOperationHandle.Status);

            if (asyncOperationHandle.Status == AsyncOperationStatus.Succeeded)
            {
                SpawnTetris();
            }
        }      
    }
}