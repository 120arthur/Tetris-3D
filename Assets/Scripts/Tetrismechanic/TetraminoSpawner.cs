using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Zenject;
using Random = UnityEngine.Random;

namespace TetrisMechanic
{
    public enum TetraminoCubeType
    {
        NORMAL,
        ANGRY,
        BORED,
        ENERGY
    }

    /// <summary>
    /// This class is in charge of selecting and instantiating tretramino.
    /// </summary>
    public class TetraminoSpawner : MonoBehaviour, ITetraminoSpawner
    {
        [Inject]
        private IInstantiator m_instantiator;
        [Inject]
        private IGameStateManager m_controllerManager;

        private AsyncOperationHandle<IList<GameObject>> m_tetraminosLoadOpHandle;
        private List<string> m_keys = new List<string>() { "Tetraminos" };

        private AsyncOperationHandle<IList<TetraminoCubeInfo>> m_cubeTypesLoadOpHandle;
        private List<string> m_keys2 = new List<string>() { "CubeTypes" };

        private TetraminoMovement m_currentTetramino;

        // Store last tetris spawned index
        private int m_previewTetris;

        public AsyncOperationHandle<IList<TetraminoCubeInfo>> CubeTypesLoadOpHandle { get => m_cubeTypesLoadOpHandle; }

        private void Start()
        {
            m_controllerManager.ChangeState(GameState.START);

            m_tetraminosLoadOpHandle = Addressables.LoadAssetsAsync<GameObject>(m_keys, null, Addressables.MergeMode.Union);
            m_tetraminosLoadOpHandle.Completed += OnTetraminosLadComplete;

            m_cubeTypesLoadOpHandle = Addressables.LoadAssetsAsync<TetraminoCubeInfo>(m_keys2, null, Addressables.MergeMode.Union);
        }

        private void OnDestroy()
        {
            ReleaseTetraminosLoadHandle();
        }

        private void ReleaseTetraminosLoadHandle()
        {
            if (m_tetraminosLoadOpHandle.IsValid())
            {
                Addressables.Release(m_tetraminosLoadOpHandle);
            }

            if (m_cubeTypesLoadOpHandle.IsValid())
            {
                Addressables.Release(m_cubeTypesLoadOpHandle);
            }
        }

        // This method instantiates a random tetris.
        public void SpawnTetris()
        {
            if (m_tetraminosLoadOpHandle.Status != AsyncOperationStatus.Succeeded)
            {
                return;
            }

            while (true)
            {
                int tetrisIndexToSpawn = SwitchNumber(0, m_tetraminosLoadOpHandle.Result.Count);

                if (m_previewTetris == tetrisIndexToSpawn) continue;

                GameObject tetramino = m_tetraminosLoadOpHandle.Result[tetrisIndexToSpawn];
                m_currentTetramino = m_instantiator.InstantiatePrefabForComponent<TetraminoMovement>(tetramino, gameObject.transform.position, Quaternion.identity, gameObject.transform);
                m_currentTetramino.InitTetramino();
                m_previewTetris = tetrisIndexToSpawn;

                break;
            }
        }

        public void DestroyCurrentTetramino()
        {
            Destroy(m_currentTetramino);
            Addressables.ReleaseInstance(m_currentTetramino.ParentAnchor);
        }

        private int SwitchNumber(int min, int max)
        {
            return Random.Range(min, max);
        }

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