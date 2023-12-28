using LanguageExt.Common;

namespace WebApi.Apis.GolangServiceApi;

public interface IGolangService
{
    Task<GetRandomNumberResponseDto> GetRandomNumber(GetRandomNumberRequestDto requestDto);
}