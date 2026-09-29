using ThawIceCream;

namespace ThawIceCreamTest;

public class IceCreamShopTest
{

    [Fact]
    // it would be nice if we could test both low and medium thaw
    // but we never know what the temperature is at the moment ;-)
    public void CallingTakeStockOnceIsNotNoThaw()
    {
        IceCreamShop shop = new();
        Assert.NotEqual(ThawAmount.None, shop.ThawStock());
    }
    
    // TODO CallingStockWithLowTemperatureResultsInLowThawAmount
    // TODO CallingStockWithHighTemperatureResultsInMediumThawAmount
    
    [Fact]
    public void CallingTakeStockTwiceResultsInNoThaw()
    {
        IceCreamShop shop = new();
        shop.ThawStock();
        Assert.Equal(ThawAmount.None, shop.ThawStock());
    }
}