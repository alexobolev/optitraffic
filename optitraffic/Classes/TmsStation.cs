using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace optitraffic.Classes
{
    public class TmsStation
    {
        public int Id { get; }
        public int MunicipalityCode { get; set; }
        public string MunicipalityName { get; set; }
        public List<int> StationSensorsCodes { get; }

        public TmsStation() : this(-1, -1, null) { }
        public TmsStation(int id, int mun_code, string mun_name)
        {
            this.Id = id;
            this.MunicipalityCode = mun_code;
            this.MunicipalityName = mun_name;
            this.StationSensorsCodes = new List<int>();
        }

        public void AddStationSensorCode(int code)
        {
            this.StationSensorsCodes.Add(code);
        }
    }
}