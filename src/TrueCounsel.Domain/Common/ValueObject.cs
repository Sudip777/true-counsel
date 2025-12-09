namespace TrueCounsel.Domain.Common;

/// <summary>
/// Base class for Domain-Driven Design (DDD) Value Objects in Clean Architecture.
///
/// Value Objects are immutable objects whose equality is defined by their values (structural equality),
/// not by identity. Examples: Money, Email, Address, Percentage, Range, etc.
///
/// This base class eliminates boilerplate and guarantees correct, consistent, and performant
/// implementation of <see cref="Equals(object?)"/> and <see cref="GetHashCode"/> for all value objects.
/// It also provides == and != operators for natural syntax.
///
/// Usage:
///   - Inherit from ValueObject
///   - Make properties immutable (init or readonly)
///   - Override GetEqualityComponents() and yield all properties that define equality
/// </summary>
/// <example>
/// public class Money : ValueObject
/// {
///     public decimal Amount { get; }
///     public string Currency { get; }
///
///     protected override IEnumerable<object> GetEqualityComponents()
///     {
///         yield return Amount;
///         yield return Currency;
///     }
/// }
/// </example>
public abstract class ValueObject
{
    /// <summary>
    /// <summary>
    /// Returns the components that make up the logical value of this object.
    /// Derived classes must return all properties/fields that participate in equality.
    /// Order matters — both sides must yield components in the same order.
    /// </summary>
    protected abstract IEnumerable<object> GetEqualityComponents();

    /// <summary>
    /// Determines whether the current ValueObject is equal to another object.
    /// Equality is based purely on the values returned by <see cref="GetEqualityComponents"/>.
    /// Handles null checks and exact type matching (prevents comparing Money with Percentage).
    /// </summary>
    public override bool Equals(object? obj)
    {
        if (obj == null || obj.GetType() != GetType())
            return false;

        var other = (ValueObject)obj;
        return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }

    /// <summary>
    /// Computes a hash code based on all equality components.
    /// Uses a prime-multiplication algorithm (23 is a common good prime) with unchecked overflow
    /// to ensure equal objects always produce the same hash code — required for dictionaries, HashSets,
    /// EF Core change tracking, and any hash-based collection.
    /// </summary>
    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Aggregate(1, (current, obj) =>
            {
                unchecked
                {
                    return current * 23 + (obj?.GetHashCode() ?? 0);
                }
            });
    }

    /// <summary>
    /// Value-based equality operator. Allows natural syntax: money1 == money2.
    /// Properly handles nulls on either side.
    /// </summary>
    public static bool operator ==(ValueObject? a, ValueObject? b)
        => a?.Equals(b) ?? b is null;

    /// <summary>
    /// Value-based inequality operator.
    /// </summary>
    public static bool operator !=(ValueObject? a, ValueObject? b)
        => !(a == b);
}