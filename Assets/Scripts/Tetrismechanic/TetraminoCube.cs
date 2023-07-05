using UnityEngine;

public enum TetraminoCubeType
{
    NORMAL,
    ANGRY,
    BORED,
    ENERGY
}

public class TetraminoCube : MonoBehaviour
{
    [SerializeField]
    private Material m_Material;
    [SerializeField]
    private TetraminoCubeType m_TetraminoCubeType;

    public TetraminoCubeType TetraminoCubeType => m_TetraminoCubeType;
    public Material Material => m_Material;

    public void SetTetraminoCubeInfo(TetraminoCubeInfo tetraminoCubeInfo)
    {
        if (tetraminoCubeInfo != null)
        {
            m_Material = tetraminoCubeInfo.Material;
            m_TetraminoCubeType = tetraminoCubeInfo.TetraminoCubeType;

            Renderer renderer = GetComponentInChildren<Renderer>();
            renderer.material = m_Material;
        }
    }
}
