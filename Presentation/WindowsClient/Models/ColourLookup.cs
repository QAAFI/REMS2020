using System;
using System.Collections.Generic;
using System.Drawing;

namespace WindowsClient.Models
{
    // Jasons colour lookup class
    public class ColourLookup
    {
        public Dictionary<string, Color> Colors { get; set; }
        public List<Color> AvailableColors { get; set; }
        public int NextColorIndex { get; set; } = 0;
        public int StartColorIndex { get; set; } = 0;

        public (Color colour, bool newEntry) Lookup(string lookupName)
        {
            var newEntry = !Colors.ContainsKey(lookupName.ToLower());
            if (newEntry)
            {
                if (NextColorIndex >= AvailableColors.Count)
                {
                    StartColorIndex += 1;
                    NextColorIndex = StartColorIndex;
                }
                Colors.Add(lookupName.ToLower(), AvailableColors[NextColorIndex]);
                NextColorIndex += 15;
            }
            return (Colors[lookupName.ToLower()], newEntry);
        }
        public void Clear()
        {
            Colors = new Dictionary<string, Color>();
            StartColorIndex = 0;
            NextColorIndex = 0;
        }
        public ColourLookup()
        {
            Colors = new Dictionary<string, Color>();
            AvailableColors = new List<Color>()
            {
                Color.FromArgb(128,0,0),
                Color.FromArgb(139,0,0),
                Color.FromArgb(165,42,42),
                Color.FromArgb(178,34,34),
                Color.FromArgb(220,20,60),
                Color.FromArgb(255,0,0),
                Color.FromArgb(255,99,71),
                Color.FromArgb(255,127,80),
                Color.FromArgb(205,92,92),
                Color.FromArgb(240,128,128),
                Color.FromArgb(233,150,122),
                Color.FromArgb(250,128,114),
                Color.FromArgb(255,160,122),
                Color.FromArgb(255,69,0),
                Color.FromArgb(255,140,0),
                Color.FromArgb(255,165,0),
                Color.FromArgb(255,215,0),
                Color.FromArgb(184,134,11),
                Color.FromArgb(218,165,32),
                Color.FromArgb(238,232,170),
                Color.FromArgb(189,183,107),
                Color.FromArgb(240,230,140),
                Color.FromArgb(128,128,0),
                Color.FromArgb(255,255,0),
                Color.FromArgb(154,205,50),
                Color.FromArgb(85,107,47),
                Color.FromArgb(107,142,35),
                Color.FromArgb(124,252,0),
                Color.FromArgb(127,255,0),
                Color.FromArgb(173,255,47),
                Color.FromArgb(0,100,0),
                Color.FromArgb(0,128,0),
                Color.FromArgb(34,139,34),
                Color.FromArgb(0,255,0),
                Color.FromArgb(50,205,50),
                Color.FromArgb(144,238,144),
                Color.FromArgb(152,251,152),
                Color.FromArgb(143,188,143),
                Color.FromArgb(0,250,154),
                Color.FromArgb(0,255,127),
                Color.FromArgb(46,139,87),
                Color.FromArgb(102,205,170),
                Color.FromArgb(60,179,113),
                Color.FromArgb(32,178,170),
                Color.FromArgb(47,79,79),
                Color.FromArgb(0,128,128),
                Color.FromArgb(0,139,139),
                Color.FromArgb(0,255,255),
                Color.FromArgb(0,255,255),
                Color.FromArgb(224,255,255),
                Color.FromArgb(0,206,209),
                Color.FromArgb(64,224,208),
                Color.FromArgb(72,209,204),
                Color.FromArgb(175,238,238),
                Color.FromArgb(127,255,212),
                Color.FromArgb(176,224,230),
                Color.FromArgb(95,158,160),
                Color.FromArgb(70,130,180),
                Color.FromArgb(100,149,237),
                Color.FromArgb(0,191,255),
                Color.FromArgb(30,144,255),
                Color.FromArgb(173,216,230),
                Color.FromArgb(135,206,235),
                Color.FromArgb(135,206,250),
                Color.FromArgb(25,25,112),
                Color.FromArgb(0,0,128),
                Color.FromArgb(0,0,139),
                Color.FromArgb(0,0,205),
                Color.FromArgb(0,0,255),
                Color.FromArgb(65,105,225),
                Color.FromArgb(138,43,226),
                Color.FromArgb(75,0,130),
                Color.FromArgb(72,61,139),
                Color.FromArgb(106,90,205),
                Color.FromArgb(123,104,238),
                Color.FromArgb(147,112,219),
                Color.FromArgb(139,0,139),
                Color.FromArgb(148,0,211),
                Color.FromArgb(153,50,204),
                Color.FromArgb(186,85,211),
                Color.FromArgb(128,0,128),
                Color.FromArgb(216,191,216),
                Color.FromArgb(221,160,221),
                Color.FromArgb(238,130,238),
                Color.FromArgb(255,0,255),
                Color.FromArgb(218,112,214),
                Color.FromArgb(199,21,133),
                Color.FromArgb(219,112,147),
                Color.FromArgb(255,20,147),
                Color.FromArgb(255,105,180),
                Color.FromArgb(255,182,193),
                Color.FromArgb(255,192,203),
                Color.FromArgb(250,235,215),
                Color.FromArgb(245,245,220),
                Color.FromArgb(255,228,196),
                Color.FromArgb(255,235,205),
                Color.FromArgb(245,222,179),
                Color.FromArgb(255,248,220),
                Color.FromArgb(255,250,205),
                Color.FromArgb(250,250,210),
                Color.FromArgb(255,255,224),
                Color.FromArgb(139,69,19),
                Color.FromArgb(160,82,45),
                Color.FromArgb(210,105,30),
                Color.FromArgb(205,133,63),
                Color.FromArgb(244,164,96),
                Color.FromArgb(222,184,135),
                Color.FromArgb(210,180,140),
                Color.FromArgb(188,143,143),
                Color.FromArgb(255,228,181),
                Color.FromArgb(255,222,173),
                Color.FromArgb(255,218,185),
                Color.FromArgb(255,228,225),
                Color.FromArgb(255,240,245),
                Color.FromArgb(250,240,230),
                Color.FromArgb(253,245,230),
                Color.FromArgb(255,239,213),
                Color.FromArgb(255,245,238),
                Color.FromArgb(245,255,250),
                Color.FromArgb(112,128,144),
                Color.FromArgb(119,136,153),
                Color.FromArgb(176,196,222),
                Color.FromArgb(230,230,250),
                Color.FromArgb(255,250,240),
                Color.FromArgb(240,248,255),
                Color.FromArgb(248,248,255),
                Color.FromArgb(240,255,240),
                Color.FromArgb(255,255,240),
                Color.FromArgb(240,255,255),
                Color.FromArgb(255,250,250),
                Color.FromArgb(0,0,0),
                Color.FromArgb(105,105,105),
                Color.FromArgb(128,128,128),
                Color.FromArgb(169,169,169),
                Color.FromArgb(192,192,192),
                Color.FromArgb(211,211,211),
                Color.FromArgb(220,220,220),
                Color.FromArgb(245,245,245),
                Color.FromArgb(255,255,255),
            };
        }
    }
}
