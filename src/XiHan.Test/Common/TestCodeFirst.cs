#region <<版权版本注释>>

//// ----------------------------------------------------------------
//// Copyright ©2022 ZhaiFanhua All Rights Reserved.
//// FileName:TestCodeFirst
//// Guid:10f07272-2415-495a-a87a-5bdccf209bb0
//// Author:zhaifanhua
//// Email:me@zhaifanhua.com
//// CreateTime:2022-06-30 上午 01:39:10
//// ----------------------------------------------------------------

#endregion <<版权版本注释>>

//using System.Text;
//using XiHan.Models.Syses;
//using XiHan.Utils.DirFile;

//namespace XiHan.Test.Common;

///// <summary>
///// 测试代码生成
///// </summary>
//public static class TestCodeFirst
//{
//    public static async Task<bool> CodeFirst()
//    {
//        string filein = @"D:\Project\XiHan_\架构设计\数据库设计.xlsx";
//        string fileout = @"D:\CodeFirstOfSysDictionary.txt";
//        //需要注明非商业用途
//        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

//        var file = new FileInfo(filein);
//        var SysDictionaryList = await LoadExcelFile(file);
//        Console.WriteLine($@"导入完成【{filein}】");
//        SysDictionaryList = SysDictionaryList.OrderByDescending(e => e.TypeKey).OrderByDescending(e => e.StateKey).ToList();
//        StringBuilder sb = new StringBuilder();
//        foreach (var SysDictionary in SysDictionaryList)
//        {
//            string s = @"new SysDictionary{" + Environment.NewLine
//                        + "\t" + @"TypeKey = """ + SysDictionary.TypeKey + @"""," + Environment.NewLine
//                        + "\t" + @"TypeName = """ + SysDictionary.TypeName + @"""," + Environment.NewLine
//                        + "\t" + @"StateKey = " + SysDictionary.StateKey + @"," + Environment.NewLine
//                        + "\t" + @"StateName = """ + SysDictionary.StateName + @"""," + Environment.NewLine
//                        + @"}," + Environment.NewLine;
//            sb.Append(s);
//            DirFileHelper.CreateFile(fileout, sb.ToString(), Encoding.UTF8);
//        }
//        Console.WriteLine($@"导出完成【{fileout}】");
//        return true;
//    }

//    //从Excel读取数据
//    private static async Task<List<SysDictionary>> LoadExcelFile(FileInfo file)
//    {
//        var SysDictionaryList = new List<SysDictionary>();

//        using (var package = new ExcelPackage(file))
//        {
//            await package.LoadAsync(file);
//            var ws = package.Workbook.Worksheets[1];
//            int row = 2;
//            int col = 1;
//            while (string.IsNullOrWhiteSpace(ws.Cells[row, col].Value?.ToString()) == false)
//            {
//                var SysDictionary = new SysDictionary
//                {
//                    //TypeKey = ws.Cells[row, col].Value.ToString(),
//                    //TypeName = ws.Cells[row, col + 1].Value.ToString(),
//                    //StateKey = int.Parse(ws.Cells[row, col + 2].Value.ToString()),
//                    //StateName = ws.Cells[row, col + 3].Value.ToString()
//                };
//                SysDictionaryList.Add(SysDictionary);
//                row += 1;
//                Console.WriteLine(SysDictionary.TypeKey + " " + SysDictionary.TypeName + " " + SysDictionary.StateKey + " " + SysDictionary.StateName);
//            }
//        }
//        return SysDictionaryList;
//    }
//}