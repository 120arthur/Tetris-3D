using UnityEngine;

[CreateAssetMenu(fileName = "TetraminoCube", menuName = "ScriptableObjects/CubeInfo", order = 1)]

public class TetraminoCubeInfo : ScriptableObject
{
    public Material Material;
    public TetraminoCubeType TetraminoCubeType;
}
