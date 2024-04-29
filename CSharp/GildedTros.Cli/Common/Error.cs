
namespace GildedTros.Cli.Common;

/// <summary>
/// Represents an error code associated with an operation failure.
/// </summary>
public sealed record Error(string Code, string? Description = null)
{
    /// <summary>
    /// An error code with an empty string, signifying a successful operation.
    /// </summary>
    public static readonly Error None = new(string.Empty);

    /// <summary>
    /// A pre-defined error code indicating item update validation failure.
    /// </summary>
    public static readonly Error ValidationFailed = new("101", "Update item validation failed");

    /// <summary>
    /// Implicit conversion operator that allows casting an Error object to a Result object.
    /// The resulting Result object will be marked as failure with the provided error code.
    /// </summary>
    /// <param name="error">The Error object to be converted.</param>
    /// <returns>A Result object with IsSuccess set to false and Error set to the provided error code.</returns>
    public static implicit operator Result(Error error) => Result.Failure(error);
}
