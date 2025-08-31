using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        for(int row = 0; row < 8; row++)
        {
            Gizmos.color = AlternateColor(Gizmos.color);
            for(int col = 0; col < 8; col++)
            {
                Gizmos.DrawCube(new Vector3(col, row), Vector3.one);

                Gizmos.color = AlternateColor(Gizmos.color);
            }
        }
    }

    Color AlternateColor(Color current)
    {
        if (current == Color.white) return new Color(0.2f, 0.2f, 0.2f);
        else return Color.white;
    }
}
