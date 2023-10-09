using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIUtils : GenericSingleton<AIUtils>
{
    public bool WithInRange(Vector3 pointA, Vector3 pointB, float distance)
    {
        return Vector3.Distance(pointA, pointB) <= distance;
    }

    public bool OutOfRange(Vector3 pointA, Vector3 pointB, float distance)
    {
        return Vector3.Distance(pointA, pointB) >= distance;
    }

    public int ClosestMinLocation(Vector3 enemy, List<Transform> minLocations)
    {
        int closestIndex = -1;
        float closestDistance = Mathf.Infinity;

        for (int i = 0; i < minLocations.Count; i++)
        {
            float distance = Vector3.Distance(enemy, minLocations[i].position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestIndex = i;
            }
        }
        if (closestIndex == -1)
            return 0;

        return closestIndex;
    }

    public bool IsInFront(Transform objectA, Transform objectB, float minDistance)
    {
        // Get the direction vector from objectA to objectB
        Vector3 direction = objectB.position - objectA.position;

        // Normalize the direction vector
        direction.Normalize();

        // Calculate the dot product between the normalized direction vector and the forward vector of objectA
        float dotProduct = Vector3.Dot(direction, objectA.forward);

        bool isWithIn = WithInRange(objectA.position, objectB.position, minDistance);
        // Check if objectB is in front of objectA
        if (dotProduct >= 0f && isWithIn)
        {
            Debug.Log("objectB is in front of objectA");
            return true;
        }

        Debug.Log("objectB is behind objectA");
        return false;
        
    }

}
