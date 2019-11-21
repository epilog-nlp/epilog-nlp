using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System;
using System.Threading.Tasks;
using System.Threading;

namespace Epilog.Taggers.MaxEntropy
{
    public class TagManager
    {
        private int currentIndex = -1;

        private int NextIndex => Interlocked.Increment(ref currentIndex);

        private IDictionary<int, string> Index { get; set; }
            = new Dictionary<int, string>();

        private IDictionary<string, int> HashIndex { get; set; }
            = new Dictionary<string, int>();

        public TaggerConfig Config { get; set; } = new TaggerConfig();

        //private ISet<string> Index { get; set; } = new HashSet<string>();

        private ISet<string> ClosedTags { get; set; } = new HashSet<string>{".", ", ", "``", "''", ":", "$", "EX", "(", ")", "#", "MD", "CC", "DT", "LS", "PDT", "POS", "PRP", "PRP$", "RP", "TO", "UH", "WDT", "WP", "WP$", "WRB", "-LRB-", "-RRB-"};

        public ISet<string> OpenTags => new HashSet<string>(HashIndex.Keys.Except(ClosedTags));
        //private ISet<string> OpenTags { get; set; } = new HashSet<string>();

        private bool DoDeterministicTagExpansion => Config.ExpandTags;

        private bool openFixed = false;

        public bool LearnClosedTags { get; set; } = false;

        public ISet<string> TagSet => new HashSet<string>(HashIndex.Keys);

        public int Add(string tag)
        {
            if (!HashIndex.TryGetValue(tag, out var index))
            {
                index = NextIndex;
                Index[index] = tag;
                HashIndex[tag] = index;
            }
            return index;
        }

        protected bool IsClosed(string tag)
            => openFixed
            ? !OpenTags.Contains(tag)
            : ClosedTags.Contains(tag);

        private void MarkClosed(string tag)
        {
            Add(tag);
            ClosedTags.Add(tag);
        }


        public void MakeImmutable()
        {
            HashIndex = HashIndex.ToImmutableDictionary();
            Index = Index.ToImmutableDictionary();
            ClosedTags = ClosedTags.ToImmutableHashSet();
            //OpenTags = OpenTags.ToImmutableHashSet();
        }

        private IEnumerable<string> ExpandTags(params string[] tags)
        {
            var intersect = GetExpandedTags(tags);

            foreach(var tag in tags)
                yield return tag;

            foreach (var tag in intersect.Result)
                yield return tag;

        }

        private Task<IEnumerable<string>> GetExpandedTags(IEnumerable<string> tags) => Task.Run(() =>
        {
            var intersect = tags.Intersect(new[] { "VBD", "VBN", "VB", "VBP" }).ToList();
            if (intersect.Contains("VBN") ^ intersect.Contains("VBD"))
                intersect.Add(intersect.Contains("VBN") ? "VBD" : "VBN");
            if (intersect.Contains("VB") ^ intersect.Contains("VBP"))
                intersect.Add(intersect.Contains("VB") ? "VBP" : "VB");
            return intersect.AsEnumerable();
        });

    }
}
