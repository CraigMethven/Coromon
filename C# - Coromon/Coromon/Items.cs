using System;

namespace Coromon
{
    public class Items
    {
        private String name;
        private int quantity;
        private Character myCharacter;

        public Items() { }

        public Items(Character tempCharacter, String tempName)
        {
            myCharacter = tempCharacter;
            name = tempName;
            quantity = 0;
            if (!myCharacter.getUs())
            {
                Random rnd = new Random();
                quantity = rnd.Next(0, 3);
            }
        }

        public void setQuantity(int newQuantity)
        {
            this.quantity = newQuantity;
        }

        public int getQuantity()
        {
            return this.quantity;
        }

        public String getName()
        {
            return this.name;
        }
        public void addQuantity(int tempInt)
        {
            quantity += tempInt;
        }
    }
}
