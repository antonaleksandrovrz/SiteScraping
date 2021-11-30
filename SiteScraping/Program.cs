using HtmlAgilityPack;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace SiteScraping
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            GetHtmlAsync();
            Console.ReadLine();
        }

        private static async void GetHtmlAsync()
        {
            string url = "https://www.emag.bg/nastolni-kompjutri/c?ref=hp_menu_quick-nav_21_1&type=category";

            HttpClient httpClient = new HttpClient();
            var html = await httpClient.GetStringAsync(url);

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            var productHtml = htmlDoc.DocumentNode.Descendants("div").Where(node => node.GetAttributeValue("id", "").Equals("card_grid")).ToList();
            var productList = productHtml[0].Descendants("div").Where(node => node.GetAttributeValue("class","").Equals("card-item card-standard js-product-data")).ToList();

            foreach (var item in productList)
            {
                Console.WriteLine(item.InnerText);
            }
        }
    }
}
