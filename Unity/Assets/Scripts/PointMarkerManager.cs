using UnityEngine;

public class PointMarkerManager : MonoBehaviour
{
    [Header("Settings")]
    public GameObject pointMarkerPrefab;  // Prefab for Point A & Point B markers
    public GameObject pivotMarkerPrefab;  // Prefab for Pivot Point marker
    public Material lineMaterial; // Material for lines
    public Material arcMaterial;  // Material for the filled arc

    private LineRenderer lineAP;
    private LineRenderer lineBP;
    private MeshFilter arcMeshFilter;
    private Mesh arcMesh;

    bool isPointAAssigned = false;
    bool isPivotPointAssigned = false;
    bool isPointBAssigned = false;

    /// <summary>
    /// Instantiates a marker at the given position with a custom name.
    /// </summary>
    public void PlaceMarker(Vector3 position, string pointName)
    {
        GameObject prefabToUse = (pointName == "Pivot Point") ? pivotMarkerPrefab : pointMarkerPrefab;
        GameObject markerInstance = Instantiate(prefabToUse, position, Quaternion.identity);
        markerInstance.name = pointName;

        switch (pointName)
        {
            case "Pivot Point":
                isPivotPointAssigned = true;
                break;
            case "Point A":
                isPointAAssigned = true;
                break;
            case "Point B":
                isPointBAssigned = true;
                break;
        }

        UpdateLines();
        UpdateArc();
    }

    /// <summary>
    /// Draws or updates lines connecting Point A → Pivot and Pivot → Point B.
    /// </summary>
    public void UpdateLines()
    {
        AngleCaptureSystem angleCaptureSystem = FindObjectOfType<AngleCaptureSystem>();
        if (angleCaptureSystem == null) return;

        if (angleCaptureSystem.currentState != AngleCaptureSystem.CaptureState.WaitingForPointA && isPointAAssigned && isPivotPointAssigned)
        {
            if (lineAP == null) { lineAP = CreateLineRenderer("Line A-Pivot"); }
            lineAP.SetPosition(0, angleCaptureSystem.pointA);
            lineAP.SetPosition(1, angleCaptureSystem.pivotPoint);
        }

        if (angleCaptureSystem.currentState != AngleCaptureSystem.CaptureState.WaitingForPointB && isPointBAssigned && isPivotPointAssigned)
        {
            if (lineBP == null) { lineBP = CreateLineRenderer("Line Pivot-B"); }
            lineBP.SetPosition(0, angleCaptureSystem.pivotPoint);
            lineBP.SetPosition(1, angleCaptureSystem.pointB);
        }
    }

    /// <summary>
    /// Creates a new LineRenderer for visualizing connections.
    /// </summary>
    private LineRenderer CreateLineRenderer(string lineName)
    {
        GameObject lineObject = new GameObject(lineName);
        LineRenderer lr = lineObject.AddComponent<LineRenderer>();
        lr.material = lineMaterial;
        lr.startWidth = 0.02f;
        lr.endWidth = 0.02f;
        lr.positionCount = 2;

        // Make the lines render on top
        lr.material.renderQueue = 4000; // Overlay queue
        lr.material.SetInt("_ZTest", (int)UnityEngine.Rendering.CompareFunction.Always); // Always render

        return lr;
    }

    /// <summary>
    /// Generates or updates a filled arc between Point A and Point B around the pivot.
    /// </summary>
    public void UpdateArc()
    {
        if (!isPointAAssigned || !isPivotPointAssigned || !isPointBAssigned) return;

        AngleCaptureSystem angleCaptureSystem = FindObjectOfType<AngleCaptureSystem>();
        if (angleCaptureSystem == null) return;

       
            // Ensure arc renders on top
            if (arcMeshFilter == null)
            {
                GameObject arcObject = new GameObject("ArcMesh");
                arcObject.transform.position = angleCaptureSystem.pivotPoint;
                arcMeshFilter = arcObject.AddComponent<MeshFilter>();
                MeshRenderer arcRenderer = arcObject.AddComponent<MeshRenderer>();
                arcRenderer.material = arcMaterial;

                // Ensure arc renders on top
                arcRenderer.material.renderQueue = 4000; // Overlay queue
                arcRenderer.material.SetInt("_ZTest", (int)UnityEngine.Rendering.CompareFunction.Always);

                arcMesh = new Mesh();
            }



        GenerateArcMesh(angleCaptureSystem.pointA, angleCaptureSystem.pointB, angleCaptureSystem.pivotPoint);
    }

    private void GenerateArcMesh(Vector3 pointA, Vector3 pointB, Vector3 pivotPoint)
    {
        int segments = 20; // Number of divisions in the arc
        float radius = Vector3.Distance(pivotPoint, pointA) * 0.5f; // Arc radius based on point distance

        Vector3 startDir = (pointA - pivotPoint).normalized * radius;
        Vector3 endDir = (pointB - pivotPoint).normalized * radius;

        Vector3[] vertices = new Vector3[segments + 2];
        int[] triangles = new int[segments * 3];

        vertices[0] = Vector3.zero; // Center (pivot)
        for (int i = 0; i <= segments; i++)
        {
            float t = (float)i / segments;
            Vector3 interpolatedDir = Vector3.Slerp(startDir, endDir, t);
            vertices[i + 1] = interpolatedDir;
        }

        for (int i = 0; i < segments; i++)
        {
            triangles[i * 3] = 0;
            triangles[i * 3 + 1] = i + 1;
            triangles[i * 3 + 2] = i + 2;
        }

        arcMesh.Clear();
        arcMesh.vertices = vertices;
        arcMesh.triangles = triangles;
        arcMesh.RecalculateNormals();

        arcMeshFilter.mesh = arcMesh;
        arcMeshFilter.transform.position = pivotPoint;
    }
}