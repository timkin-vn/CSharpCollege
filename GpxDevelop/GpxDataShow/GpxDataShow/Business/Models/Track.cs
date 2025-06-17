// Track.cs
// Этот файл представляет модель трека (маршрута), используемую в приложении для отображения и анализа GPX-данных.
// Класс Track содержит список сегментов, из которых состоит весь маршрут. Каждый сегмент представляет собой непрерывную часть трека.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GpxDataShow.Business.Models
{
    // Класс Track используется для представления одного трека (маршрута),
    // который может состоять из одного или нескольких сегментов.
    public class Track
    {
        // Свойство Segments — список сегментов (TrackSegment), составляющих трек.
        // Каждый TrackSegment представляет собой непрерывную последовательность точек.
        // Используется, например, когда запись маршрута прерывалась и возобновлялась.
        public List<TrackSegment> Segments { get; set; }
    }
}