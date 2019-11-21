using System.Collections.Generic;

namespace Epilog.Taggers.MaxEntropy
{
    public class TaggerConfig
    {
        public TaggerMode Mode { get; set; } = TaggerMode.Train;

        public string Search { get; set; } = "qn";

        public string TagSeparator { get; set; } = "/";

        public bool Tokenize { get; set; } = true;

        public bool Debug { get; set; } = false;

        public int IterationCount { get; set; } = 100;

        public string Arch { get; set; } = "";

        public string WordFunction { get; set; } = "";

        public int RareWordThreshold { get; set; } = 5;

        public int MinFeatureThreshold { get; set; } = 5;

        public int CurrentWordMinFeatureThreshold { get; set; } = 2;

        public int RareWordMinFeatureThreshold { get; set; } = 10;

        public int VeryCommonWordThreshold { get; set; } = 250;

        public bool OccurringTagsOnly { get; set; } = false;

        public bool PossibleTagsOnly { get; set; } = false;

        public double SigmaSquared { get; set; } = 0.5;

        public string Encoding { get; set; } = "UTF-8";

        public bool LearnClosedClass { get; set; } = false;

        public int ClosedClassThreshold { get; set; } = 40;

        public bool Verbose { get; set; } = false;

        public bool VerboseResults { get; set; } = true;

        public bool Sgml { get; set; } = false;

        public string Language { get; set; } = "";

        public string TokenizerFactory { get; set; } = "";

        public string XmlInput { get; set; } = "";

        public string TagInside { get; set; } = "";

        public double Approximate { get; set; } = -1.0;

        public string TokenizerOptions { get; set; } = "";

        public string DefaultRegL1 { get; set; } = "1.0";

        public string OutputFile { get; set; } = "";

        public string OutputFormat { get; set; } = "slashTags";

        public string OutputFormatOptions { get; set; } = "";

        public int NumberOfThreads { get; set; } = 1;

        public List<string> ClosedClassTags { get; set; } = new List<string>();

        public List<string> OpenClassTags { get; set; } = new List<string>();

        public string EndOfSentenceTag { get; set; } = ".$$.";

        public string EndOfSentenceWord { get; set; } = ".$.";

        public bool ExpandTags { get; set; } = true;

        public double DefaultScore { get; set; } = 1.0;
    }

    public enum TaggerMode
    {
        Unspecified = 0,
        Train,
        Test,
        Tag,
        Dump
    }
}
