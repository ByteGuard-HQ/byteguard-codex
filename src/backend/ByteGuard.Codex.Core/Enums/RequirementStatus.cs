namespace ByteGuard.Codex.Core.Enums;

/// <summary>
/// Requirement status.
/// </summary>
public enum RequirementStatus
{
    /// <summary>
	/// None.
	/// </summary>
    /// <remarks>
    /// This value should not be used. It only exists to avoid defaulting to another value.
    /// </remarks>
    None = 0,

    /// <summary>
	/// Not applicable.
	/// </summary>
    /// <remarks>
    /// Defines that a specific requirement is not applicable for a given project.
    /// This may be because of the tech-stack used in the project.
    /// </remarks>
    NotApplicable = 1,

    /// <summary>
	/// Not implemented.
	/// </summary>
    /// <remarks>
    /// Defines that a specific requirements has been comitted to the project, but has not yet been implemented.
    /// </remarks>
    NotImplemented = 2,

    /// <summary>
	/// In progress.
	/// </summary>
    /// <remarks>
    /// Defines that a specific requirement has been comitted to the project, and is actively being worked on.
    /// </remarks>
    InProgress = 3,

    /// <summary>
	/// Implemented.
	/// </summary>
    /// <remarks>
    /// Defines that a specific requirement has been comitted to the project, and has been implemented.
    /// </remarks>
    Implemented = 4,

    /// <summary>
	/// Verified.
	/// </summary>
    /// <remarks>
    /// Defines that a specific requirement has been comitted to the project, and has been verified.
    /// The requirement implementation therefore is defined as successfully implemented.
    /// </remarks>
    Verified = 5
}
