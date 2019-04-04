using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using optitraffic.Classes;

namespace optitraffic
{

    public partial class FrontPage : System.Web.UI.Page
    {
        protected List<TmsStation> TmsStations = new List<TmsStation>();
        protected List<Municipality> Municipalities = new List<Municipality>();

        protected ResourceManager LocaleRes = null;


        public string Locale
        {
            get
            {
                HttpCookie langInfo = Request.Cookies["lang"];
                if (null == langInfo)
                {
                    langInfo = new HttpCookie("lang");
                    langInfo.Value = "en";
                }

                return langInfo.Value;
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            this.LocaleRes = new ResourceManager(
                String.Format("optitraffic.assets.locals.{0}", this.Locale),
                Assembly.GetExecutingAssembly()
            );  // must be before anything else!
            LocationName.Attributes.Add("placeholder", this.LocaleRes.GetString("SearchBoxPlaceholder"));

            #region Municipalities initialization
            List<string> municipalityNames = new List<string>() {
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

            foreach (var name in municipalityNames)
                this.Municipalities.Add(new Municipality(name, 0));

            this.Municipalities = this.Municipalities.OrderBy(o => o.Name).ToList();
            #endregion
        }

        protected void RetrieveStationsAndMunicipalities()
        {
            // Retrieve TMS stations and municipalities
            string stationsJson = HttpHelper.Get(HttpHelper.TmsStationsUrl);
            JObject stationsDataObj = JObject.Parse(stationsJson);

            TmsStation tmsStation;
            int id_, munCode_;
            string munName_;

            IEnumerable<JToken> featureObjs = stationsDataObj.SelectTokens("$.features[*]");
            foreach (JToken featureObj in featureObjs)
            {
                if (!int.TryParse((string)featureObj.SelectToken("id"), out id_))
                    id_ = -1;

                if (!int.TryParse((string)featureObj.SelectToken("properties.municipalityCode"), out munCode_))
                    munCode_ = -1;

                munName_ = (string)(featureObj.SelectToken("properties.municipality") ?? "");

                if (!string.IsNullOrWhiteSpace(munName_) && this.Municipalities.FindIndex(x => x.Name == munName_) == -1)
                    this.Municipalities.Add(new Municipality(munName_, munCode_));

                tmsStation = new TmsStation(
                    id_, munCode_, munName_
                );

                this.TmsStations.Add(tmsStation);

                this.Municipalities = this.Municipalities.OrderBy(o => o.Name).ToList();
            }
        }
    }
}