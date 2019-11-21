using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace Epilog.Taggers.MaxEntropy
{
    /// <summary>
    /// A Maximum-Entropy Part-of-Speech Tagger.
    /// </summary>
    /// <remarks>
    /// Based heavily on the Stanford NLP MaxentTagger.
    /// </remarks>
    public class MaxEntropyTagger
    {
        public TaggerConfig Config { get; set; } = new TaggerConfig();

        private ILogger Log { get; }

        ///// <summary>
        ///// Determines which words are considered rare. All words with count less than this number are considered rare.
        ///// </summary>
        //public int RareWordThreshold => Config.RareWordThreshold;

        ///// <summary>
        ///// Determines which features are included in the model.
        ///// </summary>
        ///// <remarks>
        ///// The model only includes features that occurred more than this number in the training data.
        ///// </remarks>
        //public int MinFeatureThreshold => Config.MinFeatureThreshold;

        ///// <summary>
        ///// The threshold to generate word features. Words occurring at least this number of times will generate word features with all their occuring tags.
        ///// </summary>
        //public int CurrentWordMinFeatureThreshold => Config.CurrentWordMinFeatureThreshold;

        ///// <summary>
        ///// Determines which rare word features are included in the model.
        ///// </summary>
        ///// <remarks>
        ///// The model only includes features for rare words that occured more than this number in the training data.
        ///// </remarks>
        //public int RareWordMinFeatureThreshold => Config.RareWordMinFeatureThreshold;

        ///// <summary>
        ///// If using Tag Equivalence classes, words occurring at least this number of times will form equivalence classes of their own.
        ///// </summary>
        //public int VeryCommonWordThreshold => Config.VeryCommonWordThreshold;

        //public bool OccurringTagsOnly => Config.OccurringTagsOnly;

        //public bool PossibleTagsOnly => Config.PossibleTagsOnly;

        public IEnumerable<Func<string, string>> WordFunctions { get; set; } = Enumerable.Empty<Func<string, string>>();

        private double defaultScore;

        private List<double> DefaultScores { get; } = new List<double>();

        private int leftContext;

        private int rightContext;

        private int xSize;

        private int ySize;

        private bool Initialized { get; set; }

        private TagManager Tags { get; set; }

        public int AddTag(string tag) => Tags.Add(tag);

        public void Initialize(TaggerConfig config = default)
        {

            Config = config ?? new TaggerConfig();

            if (!ValidClassTags(Config.OpenClassTags, Config.ClosedClassTags, Config.Language))
            {
                throw new TaggerException($"At least two of lang (\"{Config.Language}\"), {nameof(Config.OpenClassTags)} (length {Config.OpenClassTags.Count}: {string.Join(", ", Config.OpenClassTags)})," +
                    $"and {nameof(Config.ClosedClassTags)} (length {Config.ClosedClassTags.Count}: {string.Join(", ", Config.ClosedClassTags)}) specified---you must choose one!");
            }
            else if (NoLanguageSet(Config.OpenClassTags, Config.ClosedClassTags, Config.Language, Config.LearnClosedClass))
            {
                Log.LogWarning("No language set, no open-class tags specified, and no closed-class tags specified; assuming ALL tags are open class tags");
            }

            Tags = new TagManager { Config = Config };

            // TODO - Initialize DefaultScores

            // TODO - IF Config.Mode == Train, Initialize Extractors

            // TODO - Initialize AmbiguityClasses
            Initialized = true;
        }

        private static bool ValidClassTags(IList<string> openTags, IList<string> closedTags, string lang)
            => (openTags.Count > 0 && !string.IsNullOrWhiteSpace(lang)) 
            || ((closedTags.Count > 0) && !string.IsNullOrWhiteSpace(lang)) 
            || ((closedTags.Count > 0) && (openTags.Count > 0));

        private static bool NoLanguageSet(IList<string> openTags, IList<string> closedTags, string lang, bool learnClosedTagsEnabled)
            => openTags.Count == 0 && string.IsNullOrWhiteSpace(lang) && closedTags.Count == 0 && !learnClosedTagsEnabled;
    }
}
