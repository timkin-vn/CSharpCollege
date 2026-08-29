using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LimerList.DataStore.DataCollection;
using LimerList.DataStore.Dtos;
using OfficeOpenXml;

namespace LimerList.DataStore.FileAccess.FileManagers
{
    public class ExcelFileManager : IFileManager
    {
        public void OpenFromFile(string filePath, LimerCollection collection)
        {
            try
            {
                if (!File.Exists(filePath))
                    throw new FileNotFoundException($"Файл не найден: {filePath}");
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                var records = new List<LimerDto>();
                using (var package = new ExcelPackage(new FileInfo(filePath)))
                {
                    var workSheet = package.Workbook.Worksheets["Data"];
                    if(workSheet == null)
                        throw new Exception("Лист 'Data' не найден в файле");
                    int rowCount = workSheet.Dimension?.Rows ?? 0;
                    if(rowCount < 2)
                    {
                        return;
                    }
                    for(int row = 2; row <= rowCount; row++)
                    {
                        var limer = new LimerDto
                        {
                            Id = int.Parse(workSheet.Cells[row, 1].Value.ToString()),
                            FirstName = workSheet.Cells[row, 2].Value.ToString(),
                            LastName = workSheet.Cells[row, 3].Value.ToString(),
                            MiddleName = workSheet.Cells[row, 4].Value.ToString(),
                            BirthDate = DateTime.Parse(workSheet.Cells[row, 5].Value.ToString()),
                            Group = workSheet.Cells[row, 6].Value.ToString(),
                        };
                        records.Add(limer);
                    }
                    collection.ReplaceAll(records);
                }
            }
            catch
            {

            }
        }

        public void SaveToFile(string filePath, LimerCollection collection)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using(var package = new ExcelPackage())
            {
                var workSheet = package.Workbook.Worksheets.Add("Data");
                workSheet.Cells[1, 1].Value = "ID";
                workSheet.Cells[1, 2].Value = "Имя";
                workSheet.Cells[1, 3].Value = "Фамилия";
                workSheet.Cells[1, 4].Value = "Отчество";
                workSheet.Cells[1, 5].Value = "Дата рождения";
                workSheet.Cells[1, 6].Value = "Группа";
                using(var range = workSheet.Cells[1, 1, 1, 6])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(255, 223, 56));
                }
                var limers = collection.GetAll().ToList();
                for (int i=0;i <limers.Count(); i++)
                {
                    var limer = limers[i];
                    int row = i + 2;
                    workSheet.Cells[row, 1].Value = limer.Id;
                    workSheet.Cells[row, 2].Value = limer.FirstName;
                    workSheet.Cells[row, 3].Value = limer.LastName;
                    workSheet.Cells[row, 4].Value = limer.MiddleName;
                    workSheet.Cells[row, 5].Value = limer.BirthDate.ToShortDateString();
                    workSheet.Cells[row, 6].Value = limer.Group;
                }
                workSheet.Columns.AutoFit();
                FileInfo fileInfo = new FileInfo(filePath);
                package.SaveAs(fileInfo);
            }
        }
    }
}
