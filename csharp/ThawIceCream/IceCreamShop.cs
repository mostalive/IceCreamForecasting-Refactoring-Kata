namespace ThawIceCream;

// Scene:
//  When it is warm, ice cream shop staff must wait to remove the ice cream from 
//  the industrial freezer, as it will quickly thaw outside.
//  When it is normal temperature, staff must let the ice 'thaw' in the display
//  freezer a bit longer, otherwise the icecream is too hard to scoop.
// Cast:
//   Thermometer     — hardware-ish dependency. Read() returns a raw
//                     byte value (not calibrated, the client decides
//                     what it means).
//   Warmth          — enum. Reading > 25 => Warm, otherwise Cold.
//                     (Sunny/Shady's cousin from WeatherStation.cs.)
//   ThawAmount      — enum. None / Medium / Low.
//   IceCreamShop    — the client.  Maintains state and mediates interactions

using System;

public enum Warmth
{
    Cold,
    Warm
}

public enum ThawAmount
{
    None,
    Medium,
    Low
}

public class Thermometer
{
    private readonly Random _rng = new();    // stand-in for the hardware

    public byte Read()
    {
        return (byte)_rng.Next(50); // raw sensor value
    }
}

public class IceCreamShop
{
    private readonly Thermometer _thermometer;
    private bool _thawedToday;                      // event-style client state

    public IceCreamShop()
    {
        _thermometer = new();
    }

    public ThawAmount ThawStock()
    {
        if (_thawedToday)                           // guard: client state, stays
        {
            return ThawAmount.None;
        }

        _thawedToday = true;                        // set only after thawing
        byte temperature = _thermometer.Read();     // middle: unwanted dependency
        Warmth warmth = ToWarmth(temperature);
        if (warmth == Warmth.Warm)                  // tail: complex behaviour below
        {                                           // the extraction point
            return ThawAmount.Medium;
        }
        return ThawAmount.Low;
    }

    private static Warmth ToWarmth(byte temperature)
    {
        return temperature > 25 ? Warmth.Warm : Warmth.Cold;
    }
}
