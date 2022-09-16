//// ----------------------------------------------------------------
//// Copyright ©2022 ZhaiFanhua All Rights Reserved.
//// FileName:TestCodeFirst
//// Guid:10f07272-2415-495a-a87a-5bdccf209bb0
//// Author:zhaifanhua
//// Email:me@zhaifanhua.com
//// CreateTime:2022-06-30 上午 01:39:10
//// ----------------------------------------------------------------

//using OfficeOpenXml;
//using System.Text;
//using ZhaiFanhuaBlog.Models.Roots;
//using ZhaiFanhuaBlog.Utils.DirFile;

//namespace ZhaiFanhuaBlog.Test.Common;

///// <summary>
///// 测试代码生成
///// </summary>
//[TestClass]
//public static class TestCodeFirst
//{
//    [TestMethod]
//    public static async Task<bool> CodeFirst()
//    {
//        string filein = @"D:\Project\ZhaiFanhuaBlog_\架构设计\数据库设计.xlsx";
//        string fileout = @"D:\CodeFirstOfRootState.txt";
//        //需要注明非商业用途
//        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

//        var file = new FileInfo(filein);
//        var rootStateList = await LoadExcelFile(file);
//        Console.WriteLine($@"导入完成【{filein}】");
//        rootStateList = rootStateList.OrderByDescending(e => e.TypeKey).OrderByDescending(e => e.StateKey).ToList();
//        StringBuilder sb = new StringBuilder();
//        foreach (var rootState in rootStateList)
//        {
//            string s = @"new RootState{" + Environment.NewLine
//                        + "\t" + @"TypeKey = """ + rootState.TypeKey + @"""," + Environment.NewLine
//                        + "\t" + @"TypeName = """ + rootState.TypeName + @"""," + Environment.NewLine
//                        + "\t" + @"StateKey = " + rootState.StateKey + @"," + Environment.NewLine
//                        + "\t" + @"StateName = """ + rootState.StateName + @"""," + Environment.NewLine
//                        + @"}," + Environment.NewLine;
//            sb.Append(s);
//            DirFileHelper.CreateFile(fileout, sb.ToString(), Encoding.UTF8);
//        }
//        Console.WriteLine($@"导出完成【{fileout}】");
//        return true;
//    }

//    //从Excel读取数据
//    private static async Task<List<RootState>> LoadExcelFile(FileInfo file)
//    {
//        var rootStateList = new List<RootState>();

//        using (var package = new ExcelPackage(file))
//        {
//            await package.LoadAsync(file);
//            var ws = package.Workbook.Worksheets[1];
//            int row = 2;
//            int col = 1;
//            while (string.IsNullOrWhiteSpace(ws.Cells[row, col].Value?.ToString()) == false)
//            {
//                var rootState = new RootState
//                {
//                    //TypeKey = ws.Cells[row, col].Value.ToString(),
//                    //TypeName = ws.Cells[row, col + 1].Value.ToString(),
//                    //StateKey = int.Parse(ws.Cells[row, col + 2].Value.ToString()),
//                    //StateName = ws.Cells[row, col + 3].Value.ToString()
//                };
//                rootStateList.Add(rootState);
//                row += 1;
//                Console.WriteLine(rootState.TypeKey + " " + rootState.TypeName + " " + rootState.StateKey + " " + rootState.StateName);
//            }
//        }
//        return rootStateList;
//    }
//}