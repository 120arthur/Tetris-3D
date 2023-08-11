using System.Collections.Generic;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace TetrisMechanic
{
    public interface ITetraminoSpawner
    {
        AsyncOperationHandle<IList<TetraminoCubeInfo>> CubeTypesLoadOpHandle { get; }
        void DestroyCurrentTetramino();
        void SpawnTetris();
    }
}