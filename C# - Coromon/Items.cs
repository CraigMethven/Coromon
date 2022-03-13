using System;

public class Items
{

    private String name;
    private int quantity;
    private Character myCharacter;

	public Items(Character tempCharacter, String tempName)
	{
        myCharacter = tempCharacter;
        quantity = 0;
        name = tempName;
	}

    public setQuantity(int newQuantity)
    {
        this.quantity = newQuantity;
    }

    public getName()
    {
        return this.name;
    }
}
