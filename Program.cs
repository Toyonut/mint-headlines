using System;
using McMaster.Extensions.CommandLineUtils;

namespace mint_headlines
{
    class Program
    {
        static int Main(string[] args)
        {
            var app = new CommandLineApplication();

            app.HelpOption();
            var headlineCount = app.Option("-c|--count <COUNT>", "Headline Count", CommandOptionType.SingleValue);

            app.OnExecute(() =>
            {
                Uri mintFeedUri = new Uri("https://blog.linuxmint.com/?feed=rss2");

                string value = $"Fetching feed data from {mintFeedUri}. \n";

                var headlineToShow = headlineCount.HasValue()
                    ? headlineCount.Value()
                    : "0";

                int num;
                if (int.TryParse(headlineToShow, out num)) {
                    Console.WriteLine($"Showing top {num} headlines.");
                } else {
                    Console.WriteLine($"count parameter must be a number");
                    return 1;
                }

                var mintScraper = new MintScrape();

                Console.WriteLine(value);

                mintScraper.GetFeedXml(mintFeedUri, num);
                return 0;
            });

            return app.Execute(args);
        }
    }
}
