using OfficeOpenXml;
using Microsoft.AspNetCore.Http; // Thêm dòng này để nhận diện IFormFile
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DemoMVC.Services
{
    public class ExcelService
    {
        // Chú ý chữ <T> ở đây, đây chính là "máy đa năng"
        public List<T> ReadExcel<T>(IFormFile file, Func<List<string>, T> mapFunc)
        {
            var resultList = new List<T>();

            if (file == null || file.Length == 0) return resultList;

            // Set license
           ExcelPackage.License.SetNonCommercialOrganization("Demo");

            using (var stream = new MemoryStream())
            {
                file.CopyTo(stream);

                using (var package = new ExcelPackage(stream))
                {
                    var worksheet = package.Workbook.Worksheets.FirstOrDefault(ws => ws.Dimension != null);
                    if (worksheet == null) return resultList;

                    int rowCount = worksheet.Dimension.Rows;
                    int colCount = worksheet.Dimension.Columns;

                    for (int row = 2; row <= rowCount; row++) // Bỏ qua dòng 1 (Header)
                    {
                        var rowData = new List<string>();

                        for (int col = 1; col <= colCount; col++)
                        {
                            rowData.Add(worksheet.Cells[row, col].Text);
                        }

                        // Gọi quy tắc map dữ liệu
                        var item = mapFunc(rowData);
                        
                        if (item != null)
                        {
                            resultList.Add(item);
                        }
                    }
                }
            }

            return resultList;
        }
    }
}