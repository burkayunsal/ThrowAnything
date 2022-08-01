using System.Collections;
using System.Collections.Generic;
using FluffyUnderware.Curvy.Controllers;
using UnityEngine;
using SBF.Extentions.Transforms;
using SBF.Extentions.Vector;

public class EnvironmentHandler : Singleton<EnvironmentHandler>
{
    public Transform ground;
 
    
    [SerializeField] SplineController recruitZone;
    
    [SerializeField] SplineController upgradeZone;

    
    public void SetRecruitZonePosition(Path currentPath)
    {
        
        recruitZone.gameObject.SetActive(currentPath.useRecruitZone);
        
        if (currentPath.useRecruitZone)
        {   
            recruitZone.Spline = currentPath.GetRoad();

            recruitZone.Position = currentPath.recruitZoneSplinePos;
        }

    }
    
    public void SetUpgradeZonePosition(Path currentPath)
    {
        
        upgradeZone.gameObject.SetActive(currentPath.useUpgradeZone);
        
        if (currentPath.useUpgradeZone)
        {   
            upgradeZone.Spline = currentPath.GetRoad();

            upgradeZone.Position = currentPath.upgradeZoneSplinePos;
        }

    }
}
