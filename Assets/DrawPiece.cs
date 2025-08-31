using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEditor;
using UnityEngine;

public class DrawPiece : MonoBehaviour
{
    public string pieceName;
    public Color colorTint = Color.white;
    [Range(0, 7)]
    public int row = 0;
    [Range(0, 7)]
    public int column = 0;

    [HideInInspector] public float size = 1;
    [HideInInspector] public Vector3 lastPos { get; set; } = new Vector3(1, 0, 0);
    Vector3 location = Vector3.zero;
    //private Sprite[] sprites;
    
    private void OnDrawGizmos()
    {
        //sprites = Resources.LoadAll<Sprite>("Pieces");
        // Draw the selected piece
        location = new Vector3(column, row);
        transform.position = location;

        Gizmos.DrawIcon(location, pieceName, true, colorTint);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(location, size);

        Gizmos.color = Color.red;
        switch (pieceName)
        {
            case "Pawn":
                DrawPawnMovement(1);
                break;
            case "Knight":
                DrawKnightMovement();
                break;
            case "Rook":
                DrawRookMovement();
                break;
            case "Bishop":
                DrawBishopMovement();
                break;
            case "Queen":
                DrawQueenMovement();
                break;
            case "King":
                DrawKingMovement();
                break;
        }
    }

    void DrawPawnMovement(int mod)
    {
        Gizmos.DrawLine(location, new Vector3(column, row + mod));
        Gizmos.DrawLine(location, new Vector3(column + 1, row + mod));
        Gizmos.DrawLine(location, new Vector3(column - 1, row + mod));
    }
    void DrawKnightMovement()
    {
        Gizmos.DrawLine(location, new Vector3(column, row + 2));
        Gizmos.DrawLine(new Vector3(column, row + 2), new Vector3(column - 1, row + 2));
        Gizmos.DrawLine(new Vector3(column, row + 2), new Vector3(column + 1, row + 2));

        Gizmos.DrawLine(location, new Vector3(column, row - 2));
        Gizmos.DrawLine(new Vector3(column, row - 2), new Vector3(column - 1, row - 2));
        Gizmos.DrawLine(new Vector3(column, row - 2), new Vector3(column + 1, row - 2));

        Gizmos.DrawLine(location, new Vector3(column + 2, row));
        Gizmos.DrawLine(new Vector3(column + 2, row), new Vector3(column + 2, row + 1));
        Gizmos.DrawLine(new Vector3(column + 2, row), new Vector3(column + 2, row - 1));

        Gizmos.DrawLine(location, new Vector3(column - 2, row));
        Gizmos.DrawLine(new Vector3(column - 2, row), new Vector3(column - 2, row + 1));
        Gizmos.DrawLine(new Vector3(column - 2, row), new Vector3(column - 2, row - 1));
    }
    void DrawRookMovement()
    {
        Gizmos.DrawLine(location, new Vector3(column, row + 8));
        Gizmos.DrawLine(location, new Vector3(column, row - 8));
        Gizmos.DrawLine(location, new Vector3(column + 8, row));
        Gizmos.DrawLine(location, new Vector3(column - 8, row));
    }
    void DrawBishopMovement()
    {
        Gizmos.DrawLine(location, new Vector3(column + 8, row + 8));
        Gizmos.DrawLine(location, new Vector3(column - 8, row - 8));
        Gizmos.DrawLine(location, new Vector3(column + 8, row - 8));
        Gizmos.DrawLine(location, new Vector3(column - 8, row + 8));
    }
    void DrawQueenMovement()
    {
        DrawRookMovement();
        DrawBishopMovement();
    }
    void DrawKingMovement()
    {
        DrawPawnMovement(1);
        DrawPawnMovement(0);
        DrawPawnMovement(-1);

    }
}
// Custom editor for DrawPiece component
[CustomEditor(typeof(DrawPiece))]
public class PieceEditor : Editor
{
    public void OnSceneGUI()
    {
        var t = target as DrawPiece;
        var tr = t.transform;
        var pos = tr.position;
        var color = new Color(1, 0.8f, 0.4f, 1);
        Handles.color = color;

        var handlePosition = pos + t.lastPos;

        Vector3 newpos = Handles.FreeMoveHandle(handlePosition, 0.3f, Vector3.zero, Handles.CircleHandleCap);

        if (newpos != handlePosition)
        {
            t.lastPos = newpos - pos;
            t.size = t.lastPos.magnitude;
        }
    }
}
