//// ----------------------------------------------------------------
//// Copyright ©2022 ZhaiFanhua All Rights Reserved.
//// FileName:TestCodeFirst
//// Guid:10f07272-2415-495a-a87a-5bdccf209bb0
//// Author:zhaifanhua
//// Email:me@zhaifanhua.com
//// CreateTime:2022-06-30 上午 01:39:10
//// ----------------------------------------------------------------

//using System.Text;
//using ZhaiFanhuaBlog.Models.Roots;
//using ZhaiFanhuaBlog.Utils.DirFile;

//namespace ZhaiFanhuaBlog.Test.Common;

///// <summary>
///// 测试代码生成
///// </summary>
//public static class TestCodeFirst
//{
//    public static async Task<bool> CodeFirst()
//    {
//        string filein = @"D:\Project\ZhaiFanhuaBlog_\架构设计\数据库设计.xlsx";
//        string fileout = @"D:\CodeFirstOfSiteDictionary.txt";
//        //需要注明非商业用途
//        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

//        var file = new FileInfo(filein);
//        var SiteDictionaryList = await LoadExcelFile(file);
//        Console.WriteLine($@"导入完成【{filein}】");
//        SiteDictionaryList = SiteDictionaryList.OrderByDescending(e => e.TypeKey).OrderByDescending(e => e.StateKey).ToList();
//        StringBuilder sb = new StringBuilder();
//        foreach (var SiteDictionary in SiteDictionaryList)
//        {
//            string s = @"new SiteDictionary{" + Environment.NewLine
//                        + "\t" + @"TypeKey = """ + SiteDictionary.TypeKey + @"""," + Environment.NewLine
//                        + "\t" + @"TypeName = """ + SiteDictionary.TypeName + @"""," + Environment.NewLine
//                        + "\t" + @"StateKey = " + SiteDictionary.StateKey + @"," + Environment.NewLine
//                        + "\t" + @"StateName = """ + SiteDictionary.StateName + @"""," + Environment.NewLine
//                        + @"}," + Environment.NewLine;
//            sb.Append(s);
//            DirFileHelper.CreateFile(fileout, sb.ToString(), Encoding.UTF8);
//        }
//        Console.WriteLine($@"导出完成【{fileout}】");
//        return true;
//    }

//    //从Excel读取数据
//    private static async Task<List<SiteDictionary>> LoadExcelFile(FileInfo file)
//    {
//        var SiteDictionaryList = new List<SiteDictionary>();

//        using (var package = new ExcelPackage(file))
//        {
//            await package.LoadAsync(file);
//            var ws = package.Workbook.Worksheets[1];
//            int row = 2;
//            int col = 1;
//            while (string.IsNullOrWhiteSpace(ws.Cells[row, col].Value?.ToString()) == false)
//            {
//                var SiteDictionary = new SiteDictionary
//                {
//                    //TypeKey = ws.Cells[row, col].Value.ToString(),
//                    //TypeName = ws.Cells[row, col + 1].Value.ToString(),
//                    //StateKey = int.Parse(ws.Cells[row, col + 2].Value.ToString()),
//                    //StateName = ws.Cells[row, col + 3].Value.ToString()
//                };
//                SiteDictionaryList.Add(SiteDictionary);
//                row += 1;
//                Console.WriteLine(SiteDictionary.TypeKey + " " + SiteDictionary.TypeName + " " + SiteDictionary.StateKey + " " + SiteDictionary.StateName);
//            }
//        }
//        return SiteDictionaryList;
//    }
//}