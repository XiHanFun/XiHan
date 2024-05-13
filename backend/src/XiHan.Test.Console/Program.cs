using System.Diagnostics;
using XiHan.Test.Console.FrameworkCore.Hardware;

Console.WriteLine("Hello, World!");

//Console.WriteLine("Press any key to test hardware, or press 'q' to exit.");
//var key = Console.ReadKey();
//if (key.KeyChar == 'q')
//{
//    break;
//}

//Console.WriteLine();
//HardwareTset.TestBoard();
//HardwareTset.TestCpu();
//HardwareTset.TestNetwork();
//HardwareTset.TestDisk();
//HardwareTset.TestRam();
//HardwareTset.TestOsPlatform();
//Console.Clear();

PerformanceCounter cpuCounter;
PerformanceCounter ramCounter;

cpuCounter = new PerformanceCounter();
cpuCounter.CategoryName = "Processor";
cpuCounter.CounterName = "% Processor Time";
cpuCounter.InstanceName = "_Total";
cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
ramCounter = new PerformanceCounter("Memory", "Available MBytes");

Console.WriteLine("电脑CPU使用率：" + cpuCounter.NextValue() + "%");
Console.WriteLine("电脑可使用内存：" + ramCounter.NextValue() + "MB");
Console.WriteLine();

while (true)
{
    System.Threading.Thread.Sleep(1000);
    Console.WriteLine("电脑CPU使用率：" + cpuCounter.NextValue() + " %");
    Console.WriteLine("电脑可使用内存：" + ramCounter.NextValue() + "MB");
    Console.WriteLine();

    if ((int)cpuCounter.NextValue() > 80)
    {
        System.Threading.Thread.Sleep(1000 * 60);
    }
}