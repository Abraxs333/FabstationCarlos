using UnityEngine;
using TMPro;
using static UnityEditor.PlayerSettings;


public class GameManager : MonoBehaviour
{
    // Singleton instance
    public static GameManager Instance { get; private set; }

    // The current state; initial state can be set as needed.
    public GameState CurrentState { get; private set; } = GameState.NoState;

    private void Awake()
    {
        // Enforce singleton pattern: destroy any duplicates
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        // Optionally persist throughout scenes
        DontDestroyOnLoad(gameObject);

    }

    private void Start()
    {
        GlobalVariables.Instance.StoreCameraInitialValues(Camera.main.transform);
        GlobalVariables.Instance.StoreTargetObjectInitialValues(TargetObject.transform);
        onResetStateEvent?.Invoke();
        Intructions.text = defaultInstructionsMessage;

    }

    [SerializeField] StateEvent onResetStateEvent;
    [SerializeField] StateEvent onMeasureAngleStarted;
    [SerializeField] StateEvent onPivotRotationStarted;
    [SerializeField] StateEvent onMinimapStarted;
    [SerializeField] StateEvent onPartSelectorStarted;
    [SerializeField] StateEvent onToolsStarted;
    [SerializeField] StateEvent onViewChangeStarted;

    [SerializeField] TextMeshProUGUI Intructions;
    string defaultInstructionsMessage = "Please select an option from the menu to start";
    string TransitionText = "Initializing system, please wait";

    [SerializeField] Transform TargetObject;


    /// <summary>
    /// Public function to change the state.
    /// </summary>
    /// <param name="newState">The new target game state.</param>
    public void ChangeState(GameState newState)
    {
        if (newState == CurrentState)
            return;

        CurrentState = newState;

        LogTools.Print(this, LogTools.LogType.GameManager, "Reseting Game States...");
        onResetStateEvent?.Invoke();

        // Log the state change using our custom LogTools
        LogTools.Print(this, LogTools.LogType.GameManager, "Game state changed to: " + newState.ToString());

        // Use switch-case to execute state-specific logic.
        switch (newState)
        {
            case GameState.MeasureAngle:
                HandleMeasureAngleState();
                break;
            case GameState.PivotRotation:
                HandlePivotRotationState();
                break;
            case GameState.Minimap:
                HandleMinimapState();
                break;
            case GameState.PartSelector:
                HandlePartSelectorState();
                break;
            case GameState.Tools:
                HandleToolsState();
                break;
            case GameState.ViewChange:
                HandleViewChangeState();
                break;
            default:
                LogTools.Print(this, LogTools.LogType.GameManager, "Unhandled game state: " + newState.ToString());
                break;
        }
    }

    #region State-Specific Methods

    private void HandleMeasureAngleState()
    {
        // Place any logic for MeasureAngle state here.
        LogTools.Print(this, LogTools.LogType.GameManager, "Entering Measure Angle state.");
        Intructions.text = TransitionText;
        onMeasureAngleStarted?.Invoke();
        Intructions.text = onMeasureAngleStarted.Instructions;
    }

    private void HandlePivotRotationState()
    {
        // Place any logic for PivotRotation state here.
        LogTools.Print(this, LogTools.LogType.GameManager, "Entering Pivot Rotation state.");
        Intructions.text = TransitionText;
        onPivotRotationStarted?.Invoke();
        Intructions.text = onPivotRotationStarted.Instructions;
    }

    private void HandleMinimapState()
    {
        // Place any logic for Minimap state here.
        LogTools.Print(this, LogTools.LogType.GameManager, "Entering Minimap state.");
        Intructions.text = TransitionText;
        onMinimapStarted?.Invoke();
        Intructions.text = onMinimapStarted.Instructions;
    }

    private void HandlePartSelectorState()
    {
        // Place any logic for PartSelector state here.
        LogTools.Print(this, LogTools.LogType.GameManager, "Entering Part Selector state.");
        Intructions.text = TransitionText;
        onPartSelectorStarted?.Invoke();
        Intructions.text = onPartSelectorStarted.Instructions;
    }

    private void HandleToolsState()
    {
        // Place any logic for Tools state here.
        LogTools.Print(this, LogTools.LogType.GameManager, "Entering Tools state.");
        Intructions.text = TransitionText;
        onToolsStarted?.Invoke();
        Intructions.text = onToolsStarted.Instructions;
    }

    private void HandleViewChangeState()
    {
        // Place any logic for ViewChange state here.
        LogTools.Print(this, LogTools.LogType.GameManager, "Entering View Change state.");
        Intructions.text = TransitionText;
        onViewChangeStarted?.Invoke();
        Intructions.text = onViewChangeStarted.Instructions;
    }

    #endregion

    public void writeInstructions(string newInstruction)
    {
        Intructions.text = newInstruction;
    }

    public void resetView()
    {
        LogTools.Print(this, LogTools.LogType.GameManager, "Resetting View");

        if (GlobalVariables.Instance.CameraInitialPosition != null)
        {

            LogTools.Print(this, LogTools.LogType.GameManager, "Initial pos = " + GlobalVariables.Instance.CameraInitialPosition + "  Current Pos = " + Camera.main.transform.position);

            Camera.main.transform.position = GlobalVariables.Instance.CameraInitialPosition;
            Camera.main.transform.rotation = GlobalVariables.Instance.CameraInitialRotation;
            Camera.main.transform.localScale = GlobalVariables.Instance.CameraInitialScale;
        


        LogTools.Print(this, LogTools.LogType.GameManager, "Finished Code block printing data again Initial pos = " + GlobalVariables.Instance.CameraInitialPosition + "  Current Pos = " + Camera.main.transform.position);
        }

        if (GlobalVariables.Instance.TargetObjectInitialPosition != null)
        {
            TargetObject.transform.position = GlobalVariables.Instance.TargetObjectInitialPosition;
            TargetObject.transform.rotation = GlobalVariables.Instance.TargetObjectInitialRotation;
            TargetObject.transform.localScale = GlobalVariables.Instance.TargetObjectInitialScale;
        }

    }
}