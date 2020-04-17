using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Contains all delegates used for Action and other event calls.
/// </summary>
public class Events
{
    // Allows remote access for unique instance
    public static Events Instance
    {
        get { return instance ?? (instance = new Events()); }
    }
    private static Events instance;

    #region Events

    public static event Action InspectionViewOnEvent;
    public void InspectionViewOn()
    {
        if (InspectionViewOnEvent != null) InspectionViewOnEvent();
    }

    public static event Action InspectionViewOffEvent;
    public void InspectionViewOff()
    {
        if (InspectionViewOffEvent != null) InspectionViewOffEvent();
    }

    /*
    public static event Action<int, int> TileCollected;
    public void CallEventTileCollected(int playerID, int tileID)
    {
        if (UpdateScoreColorEvent != null) UpdateScoreColorEvent(player, tile, highest);
    }*/

    #endregion
}