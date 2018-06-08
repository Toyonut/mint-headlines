using System;

namespace mint_headlines
{
    class Program
    {
        static void Main(string[] args)
        {
            var mintScraper = new MintScrape();

            Uri mintFeedUri = new Uri("https://blog.linuxmint.com/?feed=rss2");

            string value = $"Fetching feed data from {mintFeedUri}.\n";
            Console.WriteLine(value);

            mintScraper.GetFeedXml(mintFeedUri);
        }
    }
}
