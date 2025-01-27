using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization;

namespace Content.Shared.Pinpointer;

/// <summary>
/// Displays a sprite on the item that points towards the target component.
/// </summary>
[RegisterComponent, NetworkedComponent]
[AutoGenerateComponentState]
[Access(typeof(SharedPinpointerSystem))]
public sealed partial class PinpointerComponent : Component
{
    /// <summary>
    ///     A list of components that will be searched for when selected from the verb menu.
    ///     The closest entities found with one of the components in this list will be added to the StoredTargets.
    /// </summary>
    [DataField]
    public ComponentRegistry Components = new();

    /// <summary>
    ///     A list of entities that are stored on the pinpointer
    /// </summary>
    [ViewVariables, AutoNetworkedField]
    public readonly List<EntityUid> StoredTargets = new();

    /// <summary>
    ///     The maximum amount of targets the pinpointer is able to store
    /// </summary>
    [DataField]
    public int MaxTargets = 10;

    /// <summary>
    ///     The arrow's colour is red when the tile distance to the target is higher than this value and blue when below.
    /// </summary>
    [DataField]
    public float MediumDistance = 16f;

    /// <summary>
    ///     The arrow's colour is blue when the tile distance to the target is higher than this value and green when below.
    /// </summary>
    [DataField]
    public float CloseDistance = 8f;

    /// <summary>
    ///     The arrow's colour is green when the tile distance to the target is higher than this value and the arrow
    ///     turns into a black dot when below this value.
    /// </summary>
    [DataField]
    public float ReachedDistance = 1f;

    /// <summary>
    ///     Pinpointer arrow precision in radians.
    /// </summary>
    [DataField]
    public double Precision = 0.09;

    /// <summary>
    ///     Name to display of the target being tracked.
    /// </summary>
    [DataField, AutoNetworkedField]
    public string? TargetName;

    /// <summary>
    ///     Whether or not the target name should be updated when the target is updated.
    /// </summary>
    [DataField]
    public bool UpdateTargetName = true;

    /// <summary>
    ///     Whether or not the target can be reassigned.
    /// </summary>
    [DataField, AutoNetworkedField]
    public bool CanRetarget;

    /// <summary>
    ///     The current target.
    /// </summary>
    [ViewVariables, AutoNetworkedField]
    public EntityUid? Target = null;

    /// <summary>
    ///     If the pinpointer is turned on.
    /// </summary>
    [ViewVariables, AutoNetworkedField]
    public bool IsActive = false;

    /// <summary>
    ///     The angle the arrow is pointing at.
    /// </summary>
    [ViewVariables, AutoNetworkedField]
    public Angle ArrowAngle;

    /// <summary>
    ///     The distance towards the target.
    /// </summary>
    [ViewVariables, AutoNetworkedField]
    public Distance DistanceToTarget = Distance.Unknown;

    [ViewVariables]
    public bool HasTarget => DistanceToTarget != Distance.Unknown;
}

[Serializable, NetSerializable]
public enum Distance : byte
{
    Unknown,
    Reached,
    Close,
    Medium,
    Far
}
