using Domain.Enums;
using Domain.Models;

namespace UI.Helpers;

public static class DictionaryColorsAddition
{
    public static void SetColorsForDate(this IDictionary<DeliveryInfo, string?> colorDates, IEnumerable<DeliveryInfo> deliveryInfos)
    {
        var firstDayOfMonth = new DateOnly(DateTime.Today.Year, DateTime.Today.Month, 1);
        var lastDayOfMonth = new DateOnly(DateTime.Today.Year, DateTime.Today.Month, DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month));
    
        colorDates.Clear();

        var datesWithColors = deliveryInfos
            .Where(info => info.Date >= firstDayOfMonth && info.Date <= lastDayOfMonth)
            .Select(info => new { DeliveryInfo = info, Color = 
                info.DateStatus.Keys.Last() == DeliveryStatus.InProgress ? "Red" : "Blue" }); 

        foreach (var date in datesWithColors)
        {
            colorDates[date.DeliveryInfo] = date.Color;
        }
        
        for (var date = firstDayOfMonth; date <= lastDayOfMonth; date = date.AddDays(1))
        {
            var deliveryInfo = new DeliveryInfo(date);
            
            if (colorDates.All(cd => cd.Key.Date != date))
            {
                colorDates[deliveryInfo] = "White";
            }
        }
    }
}