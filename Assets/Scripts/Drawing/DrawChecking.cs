using UnityEngine;

namespace Drawing
{
    public class DrawChecking : MonoBehaviour
    {
        private const float PercentageToPass = 65f;
        private const float MaxSuitableDistanceToPoint = 0.2f; 
        
        public bool CheckLine(LineRenderer drawnLine, LineRenderer standardLine)
        {
            var drawnLinePoints = new Vector3[drawnLine.positionCount];
            var standardLinePoints = new Vector3[standardLine.positionCount];
            var closePointsCount = 0;

            drawnLine.GetPositions(drawnLinePoints);
            standardLine.GetPositions(standardLinePoints);

            for (var i = 0; i < drawnLinePoints.Length; i++)
            {
                if (Vector3.Distance(FindClosestLinePoint(drawnLinePoints[i], standardLinePoints), drawnLinePoints[i])
                    < MaxSuitableDistanceToPoint)
                    closePointsCount++;
            }

            var correctPointsPercentage = closePointsCount * 100 / standardLinePoints.Length;
            Debug.Log(correctPointsPercentage);
            return correctPointsPercentage > PercentageToPass;
        }

        private Vector3 FindClosestLinePoint(Vector3 point, Vector3[] linePoints)
        {
            var closestPoint = linePoints[0];

            for (var i = 1; i < linePoints.Length; i++)
            {
                if (Vector3.Distance(closestPoint, point) > Vector3.Distance(linePoints[i], point))
                    closestPoint = linePoints[i];
            }

            return closestPoint;
        }
    }
}
