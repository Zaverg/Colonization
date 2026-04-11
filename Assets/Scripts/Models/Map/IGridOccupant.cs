using UnityEngine;
using System;

public interface IGridOccupant
{
    public event Action<IGridOccupant> OnGridOut;

    public Transform Transform { get; }
}