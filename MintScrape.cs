using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Xml;
using System.Xml.Linq;
using System.Linq;

namespace mint_headlines
{
    class MintScrape
    {
        public void GetFeedXml(Uri feedUri, int headlinesToShow)
        {
            var xmlFeedData = GetXmlFeedData(feedUri);

            XmlDocument rssXmlDoc = new XmlDocument();

            try
            {
                rssXmlDoc.LoadXml(xmlFeedData);
            }
            catch (Exception err)
            {
                Console.WriteLine($"Error loading XML: {err}");
            }

            if (rssXmlDoc.HasChildNodes)
            {
                var rssXmlDocNodes = rssXmlDoc.SelectNodes("/rss/channel/item");

                var count = getCount(rssXmlDocNodes.Count, headlinesToShow);

                for (var i = 0; i < count; i++)
                {
                    Console.WriteLine(rssXmlDocNodes.Item(i).SelectSingleNode("title").InnerXml);
                    Console.WriteLine(rssXmlDocNodes.Item(i).SelectSingleNode("link").InnerXml);
                    Console.WriteLine();

                    var cdataComment = extractCData(rssXmlDocNodes.Item(i).SelectSingleNode("description").OuterXml);
                    Console.WriteLine(cdataComment);
                    Console.WriteLine();
                }
            }
        }

        private string GetXmlFeedData(Uri feedUri)
        {
            string feedXml = "";

            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders
                    .Accept
                    .Add(new MediaTypeWithQualityHeaderValue("text/xml"));

                    var request = new HttpRequestMessage(HttpMethod.Get, feedUri);

                    feedXml = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }

            return feedXml;
        }

        public string extractCData(string cdataSection)
        {
            XDocument message = new XDocument(XDocument.Parse(cdataSection));

            XCData cdata = message.DescendantNodes().OfType<XCData>().ToList()[0];
            string cDataContent = cdata.Value;

            // Some character normalization - yes this is a dirty hack...
            cDataContent = cDataContent.Replace(" [&#8230;]", "...");
            cDataContent = cDataContent.Replace("&#8217;", "'");
            cDataContent = cDataContent.Replace("&#8220;", "\"");
            cDataContent = cDataContent.Replace("&#8221;", "\"");
            cDataContent = cDataContent.Replace("&#8211;", "-");

            return cDataContent;
        }

        public int getCount(int rssReturned, int countWanted) {
            if (countWanted == 0) {
                return rssReturned;
            } else if (countWanted > rssReturned) {
                return rssReturned;
            } else {
                return countWanted;
            }
        }
    }
}
