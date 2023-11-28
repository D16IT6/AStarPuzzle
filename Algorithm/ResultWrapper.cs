using System;
using System.Collections.Generic;

namespace AStarPuzzle.Algorithm
{
    public class ResultWrapper
    { 
        public Stack<int[,]> Result { get; set; }
        public TimeSpan Timing { get; set; }
        public HeuristicOption Heuristic { get; set; } = HeuristicOption.MisplacedTiles;

        public string TimingMessage()
        {
            string daysPart = Timing.Days != 0 ? $"{Timing.Days} ngày : " : "";
            string hoursPart = Timing.Hours != 0 ? $"{Timing.Hours} giờ : " : "";
            string minutesPart = Timing.Minutes != 0 ? $"{Timing.Minutes} phút : " : "";
            string secondsPart = Timing.Seconds != 0 ? $"{Timing.Seconds} giây : " : "";
            string millisecondsPart = Timing.Milliseconds != 0 ? $"{Timing.Milliseconds} mili giây" : "";

            return $"{daysPart}{hoursPart}{minutesPart}{secondsPart}{millisecondsPart}".Trim();
        }

    }
}
