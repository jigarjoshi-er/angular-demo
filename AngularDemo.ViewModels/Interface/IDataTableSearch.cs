using Newtonsoft.Json;

namespace AngularDemo.ViewModels
{
    public interface IDataTableSearch
    {
        [JsonProperty("draw")]
        int Draw { get; set; }

        [JsonProperty("length")]
        int Length { get; set; }

        [JsonProperty("start")]
        int Start { get; set; }
    }
}