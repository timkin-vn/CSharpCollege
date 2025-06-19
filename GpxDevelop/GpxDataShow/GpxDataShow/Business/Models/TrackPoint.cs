using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GpxDataShow.Business.Models
{
    //Класс TrackPoint описывает отдельную точку трека (маршрута), содержащую информацию
    public class TrackPoint
    {
        //Широта и долгота точки — координаты на карте
        public double Latitude { get; set; }

        public double Longitude { get; set; }
        //Высота над уровнем моря (элевация)
        public double Elevation { get; set; }
        //Время, в которое была зафиксирована эта точка
        public DateTime DateTime { get; set; }
        //Флаг, указывающий, возможно, на неточность направления движения
        public bool IsDirectionUnreliable { get; set; }
        //Расстояние до предыдущей точки (или другая логика расчёта — зависит от реализации)
        public double? Distance { get; set; }
        //Скорость, рассчитанная по соседней точке
        public double? NearestVelocity { get; set; }
        //Направление (азимут), рассчитанное между этой и соседней точкой
        public double? NearestHeading { get; set; }
        //Вертикальная скорость (подъём/спуск) между точками
        public double? NearestVerticalSpeed { get; set; }
        //Средняя скорость по участку (возможно, усреднённая по нескольким соседним точкам)
        public double? MeanVelocity { get; set; }
        //Усреднённое направление
        public double? MeanHeading { get; set; }
        //Усреднённая вертикальная скорость
        public double? MeanVerticalSpeed { get; set; }
        //Среднее значение высоты в некотором окне вокруг точки
        public double? MeanElevation { get; set; }
        //Округленное значение скорости
        public double? MeanVelocityRounded { get; set; }
        //Округленное значение высоты
        public double? MeanElevationRounded { get; set; }
        //Округленное значение направления (азимута)
        public double? MeanHeadingRounded { get; set; }
    }
}
