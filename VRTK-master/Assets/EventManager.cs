using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {

    //Delegates
    public delegate void ItemExitedConveyor(GameObject obj);
    public delegate void DifficultyChanged();
    public delegate void ToyCompleted(GameObject toy);

    //Events
    public static event ItemExitedConveyor CompleteItemExitedConveyorMethods;
    public static event ItemExitedConveyor IncompleteItemExitedConveyorMethods;
    public static event DifficultyChanged DifficultyChangedMethods;
    public static event ToyCompleted ToyCompletedMethods;

    public static void OnCompleteItemExitedConveyor(GameObject obj)
    {
        CompleteItemExitedConveyorMethods(obj);
    }

    public static void OnIncompleteItemExitedConveyor(GameObject obj)
    {
        IncompleteItemExitedConveyorMethods(obj);
    }

    public static void OnDifficultyChanged()
    {
        DifficultyChangedMethods();
    }

    public static void OnToyCompleted(GameObject toy)
    {
        ToyCompletedMethods(toy);
    }
}
