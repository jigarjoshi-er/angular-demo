using Newtonsoft.Json;
using System.Collections.Generic;

namespace AngularDemo.ViewModels
{
    public interface IDataTableResult<T>
    {
        [JsonProperty("data")]
        List<T> Data { get; set; }

        [JsonProperty("draw")]
        int Draw { get; set; }

        [JsonProperty("recordsFiltered")]
        int RecordsFiltered { get; set; }

        [JsonProperty("recordsTotal")]
        int RecordsTotal { get; }
    }
}