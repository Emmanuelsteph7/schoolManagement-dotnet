using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace SchoolManagement.Api.Extensions
{
    public static class ValidationExtensions
    {
        public static async Task<IResult?> ValidateAsync<T>(
            this T request,
            IValidator<T> validator,
            CancellationToken cancellationToken = default
        )
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (validationResult.IsValid)
            {
                return null;
            }

            return Results.ValidationProblem(validationResult.ToDictionary());
        }
    }
}
