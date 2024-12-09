using System;
using System.Collections.Generic;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "WaypointsEmpty", story: "Is [Waypoints] empty?", category: "Conditions", id: "bd9454c1c04ae713a3fefaef73126bad")]
public partial class WaypointsEmptyCondition : Condition
{
    [SerializeReference] public BlackboardVariable<List<GameObject>> Waypoints;

    public override bool IsTrue()
    {
        return Waypoints.Value.Count <= 0;
    }

    public override void OnStart()
    {
    }

    public override void OnEnd()
    {
    }
}
