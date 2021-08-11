
public class GameData
{
    private string[] animal = new string[] { "BEAVER", "CHICKEN", "CHICKS", "COW", "DOG", "DONKEY", "DUCK", "ELEPHANT", "LION", "MONKEY", "PENGUIN", "PIG", "SEALS", "ZEBRA" };
    private string[] clothes = new string[] { "DRESS", "EYEGLASSES", "HAT", "SHIRT", "SHOES", "SKIRT", "SOCKS", "SUIT", "TIE", "TSHIRT" };
    private string[] fruit = new string[] { "APPLE", "BANANA", "COCONUT", "DRAGONFRUIT", "GRAPE", "LEMON", "LONGAN", "LYCHEE", "MANGO", "MANGOSTEEN", "MELON", "ORANGE", "PAPAYA", "PEAR", "STARFRUIT", "TOMATO", "WATERMELON" };
    private string[] furniture = new string[] { "CANDLE", "CHAIR", "CUPBOARD", "DESK", "DOOR", "FAN", "LAMP", "MATTRESS", "PIPE", "REFIGERATOR", "SOFA", "VASE", "WINDOW" };
    private string[] kitchenSet = new string[] { "BOTTLE", "BOTTLEOPENER", "BOWL", "CHOPSTICK", "CUP", "EGGBEATER", "FORK", "GLASS", "MUG", "OVEN", "PAN", "PLATE", "SPATULA", "SPOON" };
    private string[] stationary = new string[] { "BAG", "BOOK", "ERASER", "GLUE", "HIGHLIGHTER", "LETTER", "MAP", "MARKER", "NOTE", "PAPER", "PAPERCLIP", "PEN", "PENCIL", "PENCILCASE", "RULER", "SCISSORS", "SHARPENER" };
    private string[] vegetable = new string[] { "BROCCOLI", "CABBAGE", "CARROT", "CORN", "CUCUMBER", "EGGPLANT", "GARLIC", "GINGER", "LEEK", "LONGBEANS", "PEAS", "POTATO", "PUMPKIN", "SHALLOT" };
    private char[] alpha = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };

    public char[] GetAlpha()
    {
        return alpha;
    }
    public char GetDetailAlpha(int a)
    {
        return alpha[a];
    }

    public string GetAnimal(int a)
    {
        return animal[a];
    }
    public int GetAnimalLength()
    {
        return animal.Length;
    }
    public string GetClothes(int a)
    {
        return clothes[a];
    }
    public int GetClothesLength()
    {
        return clothes.Length;
    }
    public string GetFruit(int a)
    {
        return fruit[a];
    }
    public int GetFruitLength()
    {
        return fruit.Length;
    }
    public string GetFurniture(int a)
    {
        return furniture[a];
    }
    public int GetFurnitureLength()
    {
        return furniture.Length;
    }
    public string GetKitchenSet(int a)
    {
        return kitchenSet[a];
    }
    public int GetKitchenSetLength()
    {
        return kitchenSet.Length;
    }
    public string GetStationary(int a)
    {
        return stationary[a];
    }
    public int GetStationaryLength()
    {
        return stationary.Length;
    }
    public string GetVegetable(int a)
    {
        return vegetable[a];
    }
    public int GetVegetableLength()
    {
        return vegetable.Length;
    }
}
