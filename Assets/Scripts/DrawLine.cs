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
        // If left button of mouse is clicked
        if (Input.GetMouseButtonDown (0))
        {
            CreateLine ();
        }
        // If left button of mouse is being hold
        if (Input.GetMouseButton (0))
        {
            Vector2 tempDrawingPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
            // Check if the mouse moves while being hold (by checking distance between last drawing position and current mouse position)
            if (Vector2.Distance (tempDrawingPos, drawingPositions[drawingPositions.Count - 1]) >.1f)
            {
                UpdateLine(tempDrawingPos);
            }
        }
    }

    private void CreateLine ()
    {
        // Create a line (position is set by Line Renderer component)
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
        // Add current drawing position into the list
        drawingPositions.Add (newDrawingPos);
        lineRenderer.positionCount++;
        lineRenderer.SetPosition (lineRenderer.positionCount - 1, newDrawingPos);
        // Update edge collider
        edgeCollider.points = drawingPositions.ToArray ();
    }
}