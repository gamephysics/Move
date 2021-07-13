using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMove : MonoBehaviour
{
    // WAYPOINT
    public  LineRenderer moveLine;
    // MOVE SPEED
    const   float   speed = 4.0f;

    // VARIABLE
    private double  start = 0;
    private int     index = 0;
    private Vector3 targetPos;
    private Vector3 targetDir;

    // Start is called before the first frame update
    void Start()
    {
        //======================================
        // PREPRE START 
        //======================================
        index = 0;
        if (moveLine && moveLine.positionCount > index)
        {
            transform.position = moveLine.GetPosition(index);
        }

        //======================================
        // PREPRE NEXT
        //======================================
        ++index;
        if (moveLine && moveLine.positionCount > index)
        {
            // SET TARGET POSITION, TARGET DIRECTION
            targetPos = moveLine.GetPosition(index);
            targetDir = Vector3.Normalize(targetPos - transform.position);
        }

        //======================================
        // START TIME
        //======================================
        start = Time.timeSinceLevelLoadAsDouble;
        Debug.LogFormat("START");
    }
        
    // Update is called once per frame
    void Update()
    {
        //======================================
        // CONDITION
        //======================================
        // NO MORE WAYPOINT
        if (moveLine == null || moveLine.positionCount <= index) 
            return;
        // 1 FPS TIME
        if ((Time.timeSinceLevelLoadAsDouble - start) <= 1) 
            return;
        // UPDATE TIME
        start = Time.timeSinceLevelLoadAsDouble;

        //======================================
        // MOVE 
        //======================================
        transform.position += speed * targetDir;
        
        //======================================
        // CHECK ARRIVED 
        //======================================
        // DIRECTION from CURRENT POSITION to TARGE TPOSITION
        var curdir  = Vector3.Normalize(targetPos - transform.position);
        // CHECK ARRAIVED or PASSED
        var dotdir  = Vector3.Dot(targetDir, curdir);
        if (dotdir <= 0)
        {
            transform.position = targetPos;

            //======================================
            // PREPRE NEXT
            //======================================
            ++index;
            if (moveLine && moveLine.positionCount > index)
            {
                // SET TARGET POSITION, TARGET DIRECTION
                targetPos = moveLine.GetPosition(index);
                targetDir = Vector3.Normalize(targetPos - transform.position);
            }
            else
            {
                // ARRIVED
                Debug.LogFormat("ARRIVED");
                return;
            }
        }
        // MOVED
        Debug.LogFormat("MOVED");        
    }

}
