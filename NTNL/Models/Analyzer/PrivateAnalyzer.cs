using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTNL.Models.Analyzer
{
    class PrivateAnalyzer
    {
        public List<string> Privatelist { get; set; }
        public string Text;
        public bool hasNGword;
        public bool hasWordlist;
        public PrivateAnalyzer()
        {
            hasNGword = false;
            hasWordlist = false;
            Privatelist = new List<string>();
        }

        public List<string> getContainNGwordList()
        {
            var list = new List<string>();
            foreach (var word in Privatelist)
            {
                if (Text.Contains(word))
                {
                    hasNGword = true;
                    Console.WriteLine(word);
                    list.Add(word);
                }
            }

            return list;
        }

        public void setWordList(List<string> list)
        {
            this.Privatelist = list;
            if (Privatelist != null) { hasWordlist = true; }
        }

        public void setText(string text)
        {
            this.Text = text;
        }
    }
}
