using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Repository.Rates
{
    class XmlClasses
    {
    }


    // Примечание. Для запуска созданного кода может потребоваться NET Framework версии 4.5 или более поздней версии и .NET Core или Standard версии 2.0 или более поздней.
    /// <remarks/>
    [Serializable()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false, ElementName = "document")]
    public class RatesXml
    {
        /// <remarks/>
        [XmlElement("data")]
        public List<RateXml> Rates { get; set; }
    }

    /// <remarks/>
    [Serializable()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public class RateXml
    {
        [XmlElement("code")]
        public string Code { get; set; }

        [XmlElement("rate")]
        public string Rate { get; set; }

        [XmlElement("base")]
        public string Base { get; set; }

        /// <remarks/>
        [XmlElement("date", typeof(DateTime))]
        public DateTime Date { get; set; }
    }


}
