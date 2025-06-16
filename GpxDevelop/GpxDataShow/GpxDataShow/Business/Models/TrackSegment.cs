using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 TrackSegment.cs — это логическое продолжение модели данных, 
связанной с обработкой GPX-треков. 
Он представляет собой сегмент трека, 
который состоит из упорядоченного списка точек (TrackPoint)
 */

namespace GpxDataShow.Business.Models
{
    public class TrackSegment
    {
        /*Это список объектов типа TrackPoint
         Каждый TrackPoint содержит координаты, высоту, время и дополнительные вычисленные значения.
        В совокупности эти точки описывают фрагмент маршрута (например, часть пути между паузами или логически отдельный участок).*/

        /*
         Track (<trk>) — это полный маршрут.
         */

        /*
         Track Segment (<trkseg>) — это непрерывная последовательность точек внутри трека.

        Один трек может содержать несколько сегментов — например, если запись прерывалась, 
        или если есть несколько отдельных активностей в рамках одного файла.
         */

        /*
         Таким образом, класс TrackSegment — это модель одного такого <trkseg>.
         */

        public List<TrackPoint> Points { get; set; }
    }
}
