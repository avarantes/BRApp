using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BRApp
{
    public class Questions
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("question")]
        public string Question { get; set; }

        [JsonProperty("image_url")]
        public string Image { get; set; }

        [JsonProperty("thumb_url")]
        public string Thumb { get; set; }

        [JsonProperty("published_at")]
        public DateTime Publish { get; set; }

        public List<Choices> Choices { get; set; }

        public string QuestionListNames
        {
            get
            {
                return $"Question {Id}: {Question}";
            }
        }
    }

    public class Choices
    {
        [JsonProperty("choice")]
        public string Choice { get; set; }

        [JsonProperty("votes")]
        public int Votes { get; set; }

        public string ChoicesQuestionVotes
        {
            get
            {
                return $"{Choice} - Votes {Votes}";
            }
        }

    }
}
