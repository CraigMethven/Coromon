using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * 
 * Health calculated using data from
 * https://ourworldindata.org/grapher/weekly-growth-covid-cases?tab=table&time=2020-03-15..2020-06-10
 * 
 */

namespace Coromon
{
    //character class, parent class for player and country.
    public class Character
    {
        private int health;
        private int power;
        private Items[] playerItems;
        private int defence;
        private bool poison;             //is the player poisoned y/n
        private System.Windows.Forms.Panel myPanel;
        private int maxHealth;
        private bool us = false;

        public Character()
        {
            this.setHealth(1);
            this.setPower(1);
            this.setDefence(100);
            poison = false;
            playerItems = new Items[3];

            playerItems[0] = new Items(this, "mask");
            playerItems[1] = new Items(this, "handSan");
            playerItems[2] = new Items(this, "health");


        }

        //sets the characters health
        public void setHealth(int newHealth = 0)
        {
            this.health = newHealth;
        }

        public int getHealth()
        {
            return this.health;
        }

        public void setPower(int newPower = 0)
        {
            this.power = newPower;
        }

        public int getPower()
        {
            return this.power;
        }

        public void setItems(String itemName, int tempAmount)
        {
            //set the quantity of all items to 0

            Items tempItem = findItem(itemName);
            tempItem.setQuantity(tempAmount);

        }

        public int getQuantity(String itemName)
        {
            Items tempItem = findItem(itemName);

            return tempItem.getQuantity();

            //return the quantity of an item

            //check itemName against every item name
            //return quantity of that item

        }

        public bool hasItem(String itemName)
        {
            Items tempItem = findItem(itemName);
            if(tempItem == null)
            {
                return false;
            }

            if(tempItem.getQuantity() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Items findItem(String tempName)
        {
            for (int i = 0; i < playerItems.Length; i++)
            {
                if (playerItems[i].getName() == tempName)
                {
                    return playerItems[i];
                }
            }

            return null;

        }

        public void setDefence(int newDefence)
        {
            this.defence = newDefence;
        }

        public int getDefence()
        {
            return this.defence;
        }

        public void poisoned(bool areThey = false)
        {
            this.poison = areThey;
        }

        public bool isPoisoned()
        {
            return poison;
        }

        public void setMaxHealth(int newMaxHealth)
        {
            this.maxHealth = newMaxHealth;
            this.health = newMaxHealth;
        }

        public void addToHealth()
        {
            int addition = (maxHealth / 10) * 3;
            Console.WriteLine("Healed by " + addition);
            if (addition < 3)
            {
                addition = 3;
            }
            setHealth(health + addition);

            if(health > maxHealth)
            {
                health = maxHealth;
            }
        }

        public int getMaxHealth()
        {
            return this.maxHealth;
        }

        public void reduceHealth(int reduceBy)
        {
            setHealth(this.getHealth() - reduceBy);

            Console.WriteLine(Convert.ToString("This is the health of " + us + " " + this.health));
        }

        public void addPower(int adder)
        {
            power += adder;
        }

        public void addHealth(int adder)
        {
            maxHealth += adder;
            health = maxHealth;
        }

        public void setPanel(System.Windows.Forms.Panel tempPanel)
        {
            myPanel = tempPanel;
        }

        public System.Windows.Forms.Panel getPanel()
        {
            return myPanel;
        }

        public void changeUs()
        {
            us = true;
        }

        public bool getUs()
        {
            return us;
        }
    }
}
