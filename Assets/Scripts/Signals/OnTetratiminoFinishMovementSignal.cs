using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTetratiminoFinishMovementSignal
{
    public TetraminoCube[] m_tetraminoCubes;
    public OnTetratiminoFinishMovementSignal(TetraminoCube[] tetraminoCubes)
    {
        m_tetraminoCubes = tetraminoCubes;
    }
}
