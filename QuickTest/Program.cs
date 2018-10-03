using Konsole;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QuickTest
{
    class Program
    {
        private static string[] Files = new[] { "12520437.cpx", "12520850.cpx", "@AudioToastIcon.png", "@EnrollmentToastIcon.png", "@VpnToastIcon.png", "@WirelessDisplayToast.png", "aadauthhelper.dll", "aadtb.dll", "aadWamExtension.dll", "AboveLockAppHost.dll", "accessibilitycpl.dll", "accountaccessor.dll", "AccountsRt.dll", "AcGenral.dll", "AcLayers.dll", "acledit.dll", "aclui.dll", "acppage.dll", "AcSpecfc.dll", "ActionCenter.dll", "ActionCenterCPL.dll", "ActivationClient.dll", "ActivationManager.dll", "activeds.dll", "activeds.tlb", "ActiveSyncProvider.dll", "actxprxy.dll", "AcWinRT.dll", "acwow64.dll", "AcXtrnal.dll", "adalsql.dll", "AdaptiveCards.dll", "AddressParser.dll", "AdmTmpl.dll", "adprovider.dll", "adrclient.dll", "adsldp.dll", "adsldpc.dll", "adsmsext.dll", "adsnt.dll" };

        static void Main(string[] args)
        {
            var con = new Writer();
            // line below runs the action and resets any global console state
            // eg foreground and background colors after the action runs
            // wrapped in a try finally
            con.Run(con, ()=> Run(con), true);
        }

        static void Run(IConsole con)
        {
            int numDirs = 15;            
            con.ForegroundColor = ConsoleColor.White;
            var r = new Random();
            var dirs = Files.Take(numDirs).Select(f => f.Split(new[] { '.' })[0]);

            var tasks = new List<Task>();
            var bars = new List<ProgressBar>();
            foreach (var d in dirs)
            {
                var dir = d;
                var files = Files.Take(r.Next(30) + 10).ToArray();
                var bar = new ProgressBar(files.Count());
                bars.Add(bar);
                bar.Refresh(0, d);
                tasks.Add(new Task(() => ProcessFakeFiles(d, files, bar)));
            }

            con.WriteLine("Press enter to start");
            Console.ReadLine();
            foreach (var t in tasks) t.Start();
            Task.WaitAll(tasks.ToArray());
            con.ForegroundColor = ConsoleColor.Yellow;
            con.WriteLine("finished.");
        }

        public static void ProcessFakeFiles(string directory, string[] files, ProgressBar bar)
        {
            foreach (var file in files)
            {
                bar.Next(file);
                Thread.Sleep(150);
            }
        }
    }

    public static class QueueExtensions
    {
        public static IEnumerable<T> Dequeue<T>(this ConcurrentQueue<T> src, int x)
        {
            int cnt = 0;
            bool more = true;
            while (more && cnt < x)
            {
                T item;
                more = src.TryDequeue(out item);
                cnt++;
                yield return item;
            }
        }
    }
}

