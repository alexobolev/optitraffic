using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace optitraffic
{
    /// <summary>
    /// Summary description for AjaxSearchSuggestions
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class AjaxSearchSuggestions : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public string GetSuggestions(string value)
        {
            List<string> Municipalities = new List<string>()
            {
                "Porvoo", "Espoo", "Vantaa", "Helsinki", "Järvenpää",
                "Vihti", "Lohja", "Nurmijärvi", "Mäntsälä", "Loviisa",
                "Karkkila", "Raasepori", "Tuusula", "Hyvinkää", "Inkoo",
                "Sipoo", "Kirkkonummi", "Salo", "Hämeenkyrö", "Ikaalinen",
                "Raisio", "Pyhäranta", "Pori", "Lieto", "Marttila",
                "Sastamala", "Rauma", "Nakkila", "Eurajoki", "Ulvila",
                "Parainen", "Kankaanpää", "Kaarina", "Turku", "Paimio",
                "Masku", "Hamina", "Lappeenranta", "Virolahti", "Lempäälä",
                "Valkeakoski", "Humppila", "Kangasala", "Hämeenlinna", "Nokia",
                "Hollola", "Hausjärvi", "Ruovesi", "Ylöjärvi", "Akaa",
                "Tammela", "Loppi", "Riihimäki", "Asikkala", "Janakkala",
                "Tampere", "Heinola", "Lahti", "Orivesi", "Pirkkala",
                "Pyhtää", "Iitti", "Kouvola", "Luumäki", "Rautjärvi",
                "Mikkeli", "Ruokolahti", "Kotka", "Imatra", "Mäntyharju",
                "Joroinen", "Juva", "Savonlinna", "Pieksämäki", "Kangasniemi",
                "Hartola", "Pertunmaa", "Liperi", "Kitee", "Lieksa",
                "Tohmajärvi", "Juuka", "Kontiolahti", "Joensuu", "Leppävirta",
                "Siilinjärvi", "Tuusniemi", "Rautavaara", "Pielavesi", "Kiuruvesi",
                "Suonenjoki", "Vieremä", "Kuopio", "Iisalmi", "Jyväskylä",
                "Äänekoski", "Laukaa", "Saarijärvi", "Keuruu", "Kuhmoinen",
                "Muurame", "Viitasaari", "Joutsa", "Jämsä", "Petäjävesi",
                "Toivakka", "Isokyrö", "Mustasaari", "Kurikka", "Ilmajoki",
                "Lapua", "Närpiö", "Kokkola", "Alavus", "Uusikaarlepyy",
                "Alajärvi", "Evijärvi", "Vaasa", "Kauhava", "Seinäjoki",
                "Pedersören kunta", "Kaustinen", "Ähtäri", "Kannus", "Kauhajoki",
                "Kuortane", "Teuva", "Kristiinankaupunki", "Laihia", "Kalajoki",
                "Vöyri", "Perho", "Pyhäjärvi", "Sievi", "Alavieska",
                "Haapajärvi", "Kärsämäki", "Veteli", "Oulainen", "Oulu",
                "Ii", "Kuusamo", "Raahe", "Liminka", "Siikalatva",
                "Pudasjärvi", "Kempele", "Muhos", "Hailuoto", "Kajaani",
                "Ristijärvi", "Kuhmo", "Sotkamo", "Vaala", "Puolanka",
                "Suomussalmi", "Paltamo", "Tervola", "Inari", "Kemijärvi",
                "Keminmaa", "Rovaniemi", "Kolari", "Posio", "Ranua",
                "Ylitornio", "Salla", "Tornio", "Pello", "Muonio",
                "Enontekiö", "Utsjoki", "Sodankylä", "Kemi", "Kittilä"
            };

            List<string> suggestionsList = new List<string>();
            foreach (var m in Municipalities)
                if (null != m && m.StartsWith(value))
                    suggestionsList.Add(m);

            string resultHtml = "";

            foreach (var suggesion in suggestionsList)
                resultHtml += String.Format("<li data-val=\"{0}\">{1}</li>\n", suggesion, suggesion);

            return "<ul>" + resultHtml + "</ul>";
        }
    }
}
