﻿using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace GraphEditor.Business.Models.File
{
    [Serializable]
    [XmlRoot("Picture")]
    public class XmlPictureModel
    {
        [XmlArray("Rectangles")]
        [XmlArrayItem("Rectangle")]
        public List<XmlRectangleModel> Rectangles { get; set; }
    }
}
