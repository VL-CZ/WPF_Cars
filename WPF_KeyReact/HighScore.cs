using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_KeyReact
{
    class HighScore
    {
        public int Time { get; set; }
        public string Initials { get; set; }

        public List<HighScore> HighScores { get; set; } = new List<HighScore>();
    }
}
