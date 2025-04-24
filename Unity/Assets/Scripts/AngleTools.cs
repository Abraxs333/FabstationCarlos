using UnityEngine;

public static class AngleTools
{
    /// <summary>
    /// Calculates the angle (in degrees) between two vectors intersectin in a pivot point.
    ///  </summary>
    /// <param  name="pointA"> The first directional point.</param>
    /// <param  name="pointB"> The second directional point.</param>
    /// <param  name="pivotPoint"> The common vertex where both vectors intersect.</param>
    /// <returns> Angle in Degrees </returns>
    ///
    public static float CalculateAngle(Vector3 pointA, Vector3 pointB, Vector3 pivotPoint)
    {
        // Compute direction vectors
        Vector3 vectorA = (pointA - pivotPoint).normalized;
        Vector3 vectorB = (pointB - pivotPoint).normalized;

        // Ensure valid input (avoid NaN errors if vectors are too close)
        float dotProduct = Mathf.Clamp(Vector3.Dot(vectorA, vectorB), -1f, 1f);

        // Compute angle using dot product formula
        float angle = Mathf.Acos(dotProduct) * Mathf.Rad2Deg;

        return angle;
    }

}