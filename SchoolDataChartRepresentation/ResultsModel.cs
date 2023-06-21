using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDataChartRepresentation
{
    internal class ResultsModel
    {
        [JsonProperty("nazwa")]
        public string Nazwa { get; set; }

        [JsonProperty("tablicaLata")]
        public List<int> TablicaLata { get; set; }

        [JsonProperty("wynikiWKolejnychLatach")]
        public List<double> WynikiWKolejnychLatach { get; set; }

        [JsonProperty("dotacjeWKolejnychLatach")]
        public List<double> DotacjeWKolejnychLatach { get; set; }
    }

}
