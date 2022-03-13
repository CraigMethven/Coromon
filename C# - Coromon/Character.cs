using System;

public class Character
{
    public int health;
    public int power;
    public Items[] playerItems;
    public int defence;
    public bool poison;
    public int turnCounter;

    public Character()
    {
        this.setHealth(1);
        this.setPower(1);
        this.defence(1);
        this.turnCounter(0);
        this.isPoisoned(false);
    }

    public setHealth(int newHealth = 0)
    {
        this.health = newHealth;
    }

    public getHealth()
    {
        return this.health;
    }

    public setPower(int newPower = 0)
    {
        this.power = newPower;
    }

    public getPower()
    {
        return this.power;
    }

    public setItems(Item[] newItems)
    {
        //set the quantity of all items to 0

    }

    public getQuantity(String itemName, int tempAmount)
    {
        Items tempItem = findItem(itemName);
        tempItem.setQuantity(tempAmount);
        //return the quantity of an item

        //check itemName against every item name
        //return quantity of that item


    }

    public Items findItem(String tempName)
    {
        for(int i = 0; i < playerItems.Length(); i++)
        {
            if (playerItems[i].getName() == tempName)
            {
                return playerItems[i];
            }
        }
    }

    public setDefence(int newDefence)
    {
        this.defence = newDefence;
    }

    public setTurnCounter(int newTurn = 0)
    {
        this.turnCounter = turnCounter++;
    }
     
    public isPoisoned(bool areThey = false)
    {
        this.isPoisoned = areThey;
    }

}
