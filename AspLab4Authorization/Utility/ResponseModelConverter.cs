using AspLab4Authorization.Models;

namespace AspLab4Authorization.Utility
{
    public class ResponseModelConverter
    {
        public static IResult Convert(IResponseModel response, string? uri = null)
            => response.StatusCode switch
            {
                StatusCodes.Status200OK => TypedResults.Ok(response),
                StatusCodes.Status201Created => TypedResults.Created(uri, response),
                StatusCodes.Status204NoContent => TypedResults.NoContent(),
                StatusCodes.Status400BadRequest => TypedResults.BadRequest(response),
                StatusCodes.Status404NotFound => TypedResults.NotFound(response),
                _ => throw new ArgumentOutOfRangeException(nameof(response.StatusCode), $"Not expected StatusCode value: {response.StatusCode}"),
            };
    }
}
