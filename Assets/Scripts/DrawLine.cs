using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    public GameObject linePrefab;
    public GameObject currentLine;

    public LineRenderer lineRenderer;
    public EdgeCollider2D edgeCollider;
    public List<Vector2> drawingPositions;

    // Update is called once per frame
    void Update ()
    {
        // If left button of mouse clicked
        if (Input.GetMouseButton (0))
        {
            CreateLine ();
        }
        // If left button of mouse is being hold
        if (Input.GetMouseButton (0))
        {
            Vector2 tempDrawingPos = Camera.main.ScreenToViewportPoint (Input.mousePosition);
            if (Vector2.Distance (tempDrawingPos, drawingPositions[drawingPositions.Count - 1]) >.1f)
            {
                UpdateLine(tempDrawingPos);
            }
        }
    }

    private void CreateLine ()
    {
        currentLine = Instantiate (linePrefab, Vector3.zero, Quaternion.identity);
        lineRenderer = currentLine.GetComponent<LineRenderer> ();
        edgeCollider = currentLine.GetComponent<EdgeCollider2D> ();
        // Clear the list
        drawingPositions.Clear ();
        // Get the player's current mouse/finger position and save it to the list.
        drawingPositions.Add (Camera.main.ScreenToWorldPoint (Input.mousePosition));
        drawingPositions.Add (Camera.main.ScreenToWorldPoint (Input.mousePosition));
        // Set first two positions of line renderer
        lineRenderer.SetPosition (0, drawingPositions[0]); // First point
        lineRenderer.SetPosition (1, drawingPositions[1]); // Second point
        // Update edge collider
        edgeCollider.points = drawingPositions.ToArray ();
    }

    private void UpdateLine (Vector2 newDrawingPos)
    {
        drawingPositions.Add (newDrawingPos);
        lineRenderer.positionCount++;
        lineRenderer.SetPosition (lineRenderer.positionCount - 1, newDrawingPos);
        edgeCollider.points = drawingPositions.ToArray ();
    }
}