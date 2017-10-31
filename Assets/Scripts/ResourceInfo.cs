
public static class ResourceInfo
{
    private static int foodStock;
    private static int waterStock;
    private static int woodStock;

    //get methods
    public static int getFoodStock()
    {
        return foodStock;
    }

    public static int getWaterStock()
    {
        return waterStock;
    }
    
    public static int getWoodStock()
    {
        return woodStock;
    }

    //mutation methods
    //set methods
    public static void setFoodStock(int newFoodStock)
    {
        //If we go under we should end the game
        if (newFoodStock < 0)
        {
            foodStock = 0;
        }
        else
        {
            foodStock = newFoodStock;
        }
    }
    public static void setWaterStock(int newWaterStock)
    {
        //If we go under we should end the game
        if (newWaterStock < 0)
        {
            waterStock = 0;
        }
        else
        {
            waterStock = newWaterStock;
        }
    }
    public static void setWoodStock(int newWoodStock)
    {
        //If we go under we should end the game
        if (newWoodStock < 0)
        {
            woodStock = 0;
        }
        else
        {
            woodStock = newWoodStock;
        }
    }
    //add methods
    public static void addWaterStock(int addWaterStock)
    {
        waterStock += addWaterStock;
    }
    public static void addFoodStock(int addFoodStock)
    {
        foodStock += addFoodStock;
    }
    public static void addWoodStock(int addWoodStock)
    {
        woodStock += addWoodStock;
    }

    //subtraction methods
    //these return true if you can subtract and still get zero or above false if otherwise
    public static bool subWaterStock(int subWaterStock)
    {
        if(waterStock - subWaterStock >= 0)
        {
            waterStock -= subWaterStock;
            return true;
        }
        return false;
    }
    public static bool subFoodStock(int subFoodStock)
    {
        if(foodStock - subFoodStock >= 0)
        {
            foodStock -= subFoodStock;
            return true;
        }
        return false;
    }
    public static bool subWoodStock(int subWoodStock)
    {
        if(woodStock - subWoodStock >= 0)
        {
            woodStock -= subWoodStock;
            return true;
        }
        return false;
    }
}