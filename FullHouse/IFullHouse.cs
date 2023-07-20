using System.Collections.Concurrent;

namespace FullHouse.Interface
{
    public interface IFullHouse
    {
        public string[] suits { get; }
        public string[] ranks { get; }
        public HashSet<string> cards { get; }
        public ConcurrentDictionary<string, int> cardRanks { get; }

        string Hand(string[] cards);
    }
}
