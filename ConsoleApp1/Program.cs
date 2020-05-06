using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace ConsoleApp1
{
    class Program
    {
        class NMAP : IDisposable
        {
            private ProcessStartInfo ps = new ProcessStartInfo();
            private Process Processss = new Process();
            private string xmlciktisi;
            private string script;
            public NMAP(string script)
            {
                this.script = script;
                ps.Arguments = "-p 80 --script " + this.script + " testphp.vulnweb.com -oX -";
                ps.RedirectStandardOutput = true;
                ps.FileName = "nmap";
                Processss.StartInfo = ps;
            }
            public string DisariAktar
            {
                get
                {
                    XMLCiktisiAl();
                    return xmlciktisi;
                }
            }

            private void XMLCiktisiAl()
            {
                if (string.IsNullOrEmpty(xmlciktisi))
                {
                    Processss.Start();
                    xmlciktisi = Processss.StandardOutput.ReadToEnd();
                    Processss.WaitForExit();
                    Processss.Close();
                }
            }

            public void Dispose()
            {
                Processss.Dispose();
            }
        }
        static void Main(String[] args)
        {
            List<NMAP> nmaps = new List<NMAP>();
            nmaps.Add(new NMAP("http-sql-injection"));
            nmaps.Add(new NMAP("ssl-ccs-injection"));
            nmaps.Add(new NMAP("http-csrf"));
            StreamWriter streamwriter = new StreamWriter("result.xml");
            nmaps.ForEach(x => streamwriter.WriteLine(x.DisariAktar));
            streamwriter.Flush();
            streamwriter.Close();
        }
    }
}
