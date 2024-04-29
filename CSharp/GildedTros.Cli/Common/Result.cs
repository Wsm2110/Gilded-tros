using System;

namespace GildedTros.Cli.Common;

/// <summary>
/// Represents the outcome of an operation, either successful or failed with an error code.
/// </summary>
public class Result
{
    private Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None ||
            !isSuccess && error == Error.None)
        {
            throw new ArgumentException("Invalid error", nameof(error));
        }

        IsSuccess = isSuccess;
        Error = error;
    }

    /// <summary>
    /// Indicates whether the operation was successful.
    /// </summary>
    public bool IsSuccess { get; }

    /// <summary>
    /// Shortcut property indicating operation failure (opposite of IsSuccess).
    /// </summary>
    public bool IsFailure => !IsSuccess;

    /// <summary>
    /// The error code associated with the operation failure, or None if successful.
    /// </summary>
    public Error Error { get; }

    /// <summary>
    /// Creates a Result object indicating a successful operation.
    /// </summary>
    /// <returns>A Result object with IsSuccess set to true and Error set to Error.None.</returns>
    public static Result Success() => new Result(true, Error.None);

    /// <summary>
    /// Creates a Result object indicating a failed operation with a specific error code.
    /// </summary>
    /// <param name="error">The error code associated with the failure.</param>
    /// <returns>A Result object with IsSuccess set to false and Error set to the provided error code.</returns>
    public static Result Failure(Error error) => new Result(false, error);
}
