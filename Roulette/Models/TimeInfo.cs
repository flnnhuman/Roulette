using System;

namespace Roulette.Models
{
	public class TimeInfo
	{
		public long RoundsPassed { get; private set; } 
		public long TicksFromTheStartOfTheRound { get;  private set; } 
		public TimeSpan TimeSpanFromTheStartOfTheRound { get;  private set; } 
		public TimeSpan TimeSpanTillTheEnd { get;  private set; } 
		public DateTime TimeSpanNow { get;  private set; } 
		
		public static TimeInfo GetSomeTimeInfo()
		{
			var timeInfo = new TimeInfo();
			timeInfo.TimeSpanNow = DateTime.UtcNow;
			var ticks = timeInfo.TimeSpanNow.Ticks;
			
			timeInfo.RoundsPassed = ticks / TimeSpan.FromSeconds(30).Ticks;
			timeInfo.TicksFromTheStartOfTheRound = ticks % TimeSpan.FromSeconds(30).Ticks;
			timeInfo.TimeSpanFromTheStartOfTheRound = TimeSpan.FromTicks(timeInfo.TicksFromTheStartOfTheRound);
			timeInfo.TimeSpanTillTheEnd = TimeSpan.FromSeconds(30).Subtract(TimeSpan.FromTicks(timeInfo.TicksFromTheStartOfTheRound));
			return timeInfo;
		}
	}
}