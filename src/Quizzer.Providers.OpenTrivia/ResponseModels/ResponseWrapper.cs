using System.Text.Json.Serialization;

namespace Quizzer.Providers.OpenTrivia.ResponseModels
{
    public class ResponseWrapper
    {
        [JsonPropertyName("response_code")]
        public int ResponseCode { get; set; }

        [JsonPropertyName("results")]
        public List<ResponseQuestion> Results { get; set; }
    }
}
