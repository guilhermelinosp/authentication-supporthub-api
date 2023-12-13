namespace SupportHub.Auth.Domain.Shared.Returns;

public class BasicReturn<TValue>(TValue? value, bool isSucess, Error error) : BasicReturn(isSucess, error)
{
    public TValue Value { get; set; } = value!;

    public static implicit operator BasicReturn<TValue>(TValue? value) => Create(value);
}