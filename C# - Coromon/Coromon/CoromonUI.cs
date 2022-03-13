using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace Coromon
{
    public partial class CoromonUI : Form
    {
        int spacing = 20;
        int barLength;
        int barHeight;
        Country[] enemies;
        int phase;
        Player corona;
        int enemyCounter = 0;
        int playerStartingTurn = 0;
        int enemyStartingTurn = 0;

        //main menu - phase 0
        //attack menu - phase 1
        //item menu - phase 2

        public void fullScreen()
        {
            this.WindowState = FormWindowState.Maximized;
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;
        }
        private int lineLength;
        public void setBars()
        {
            endScreen.Visible = false;
            loseScreen.Visible = false;
            //Set transparency
            CoronaHPBar.BackColor = Color.Transparent;
            EnemyHPBar.BackColor = Color.Transparent;
            HPWriting.BackColor = Color.Transparent;
            EnemyHPWriting.BackColor = Color.Transparent;
            HPLine.BackColor = Color.Transparent;
            EnemyHPLine.BackColor = Color.Transparent;
            //Set lengths
            barLength = this.Width / 5 * 2;
            barHeight = this.Height / 8;
            lineLength = barLength - spacing * 4;
            CoronaHPBar.Size = new Size(barLength, barHeight);
            EnemyHPBar.Size = new Size(barLength, barHeight);
            HPWriting.Size = new Size(barHeight, barHeight);
            EnemyHPWriting.Size = new Size(barHeight, barHeight);
            HPLine.Size = new Size(barLength - spacing * 4, barHeight - spacing * 3);
            EnemyHPLine.Size = new Size(barLength - spacing * 4, barHeight - spacing * 3);
            //Set Locations
            CoronaHPBar.Location = new Point(spacing, this.Height - (barHeight * 3) / 2);
            HPWriting.Location = new Point(barLength, this.Height - (barHeight * 3) / 2);
            EnemyHPBar.Location = new Point(this.Width - (spacing + barLength), spacing);
            EnemyHPWriting.Location = new Point(this.Width - (spacing + barLength + barHeight), spacing);
            HPLine.Location = new Point(spacing * 5 / 2, (spacing * 3 / 2) + this.Height - (barHeight * 3) / 2);
            EnemyHPLine.Location = new Point(this.Width - (barLength) + spacing / 2, spacing * 5 / 2);
        }

        public void setCharacter()
        {
            itemPanel.Visible = false;
            animeHurt.Visible = false;
            //Corona
            CoronaCharacter.BackColor = Color.Transparent;
            int characterSize = this.Width / 3;
            CoronaCharacter.Size = new Size(characterSize, characterSize);
            CoronaCharacter.Location = new Point((barLength - spacing - characterSize) / 2, this.Height / 5);
            //Enemy
            EnemyCharacter.BackColor = Color.Transparent;
            EnemyCharacter.BackgroundImage = new Bitmap(enemies[enemyCounter].getImgPath());
            EnemyCharacter.BackgroundImage = new Bitmap(enemies[enemyCounter].getImgPath());
            int enemySize = this.Width / 6;
            EnemyCharacter.Size = new Size(enemySize, enemySize);
            EnemyCharacter.Location = new Point(Width / 8 * 5, this.Height / 7);
        }

        public void setSelectionBar()
        {
            infoPanel.Visible = false;
            //Set transparancy
            SelectionBarFrame.BackColor = Color.Transparent;
            Btn1.BackColor = Color.Transparent;
            Btn2.BackColor = Color.Transparent;
            Btn3.BackColor = Color.Transparent;
            Btn4.BackColor = Color.Transparent;
            //Set sizes
            int btnWidth = this.Width / 6;
            SelectionBarFrame.Size = new Size(this.Width / 7 * 3, this.Height / 7 * 3);
            Btn1.Size = new Size(btnWidth, this.Height / 6);
            Btn2.Size = new Size(btnWidth, this.Height / 6);
            Btn3.Size = new Size(btnWidth, this.Height / 6);
            Btn4.Size = new Size(btnWidth, this.Height / 6);
            //Set Locations
            SelectionBarFrame.Location = new Point(this.Width / 2, this.Height - spacing - this.Height / 2);
            Btn1.Location = new Point(this.Width / 2 + spacing * 2, spacing + this.Height - this.Height / 2);
            Btn2.Location = new Point(this.Width / 2 + spacing * 3 + btnWidth, spacing + this.Height - this.Height / 2);
            Btn3.Location = new Point(this.Width / 2 + spacing * 3, this.Height - this.Height / 2 + this.Height / 6 + spacing);
            Btn4.Location = new Point(this.Width / 2 + spacing * 3 + btnWidth, this.Height - this.Height / 2 + this.Height / 6 + spacing);
            //Set Buttons to be clickable
            Btn1.Click += new EventHandler(this.Button1Click);
            Btn1.MouseEnter += new EventHandler(this.Button1Hover);
            Btn1.MouseLeave += new EventHandler(this.Button1Leave);
            Btn2.Click += new EventHandler(this.Button2Click);
            Btn2.MouseEnter += new EventHandler(this.Button2Hover);
            Btn2.MouseLeave += new EventHandler(this.Button2Leave);
            Btn3.Click += new EventHandler(this.Button3Click);
            Btn3.MouseEnter += new EventHandler(this.Button3Hover);
            Btn3.MouseLeave += new EventHandler(this.Button3Leave);
            Btn4.Click += new EventHandler(this.Button4Click);
            Btn4.MouseEnter += new EventHandler(this.Button4Hover);
            Btn4.MouseLeave += new EventHandler(this.Button4Leave);
            //info button
            infoBtn.Size = new Size(60, 60);
            infoBtn.Location = new Point(this.Width - 80, this.Height-120);
            infoBtn.Click += new EventHandler(this.info);
            infoBtn.Visible = true;
            infoBtn.Refresh();
        }

        public void info(object sender, EventArgs e)
        {
            infoPanel.Size = new Size(this.Width, this.Height);
            infoPanel.Location = new Point(0, 0);
            infoPanel.Visible = true;
            NOP(10);
            infoPanel.Visible = false;
            infoPanel.Refresh();

        }

        public void Button1Click(object sender, EventArgs e)
        {
            switch (phase)
            {
                case 0:
                    //when its the main menu
                    changePhase(1);
                    break;
                case 1:
                    //when its the attack menu
                    changePhase(3);
                    removeHealth(corona, enemies[enemyCounter]);
                    enemyTurn();

                    break;
                case 2:
                    //when its the items menu
                    if (corona.hasItem("mask"))
                    {
                        changePhase(3);
                        corona.setDefence(50);
                        playerStartingTurn = 3;
                        corona.setItems("mask", corona.getQuantity("mask") - 1);
                        itemAnimation(true, 1);
                        enemyTurn();
                    }
                    break;
                default:
                    break;

            }

            //damamgeAnimation(CoronaCharacter);
        }

        public void Button2Click(object sender, EventArgs e)
        {
            switch (phase)
            {
                case 0:
                    //when its the main menu
                    changePhase(2);

                    break;
                case 1:
                    //when its the attack menu
                    changePhase(3);
                    removeHealth(corona, enemies[enemyCounter], 0.8, true);
                    enemyTurn();

                    break;
                case 2:
                    if (corona.hasItem("handSan"))
                    {
                        corona.addToHealth();
                        itemAnimation(true, 2);
                        changePhase(3);
                        corona.setItems("handSan", corona.getQuantity("handSan") - 1);
                        enemyTurn();
                    }
                    //when its the items menu
                    break;
                default:
                    break;

            }

            //damamgeAnimation(EnemyCharacter);
        }

        public void Button3Click(object sender, EventArgs e)
        {
            switch (phase)
            {
                case 0:

                    //when its the main menu

                    break;
                case 1:

                    //when its the attack menu
                    changePhase(3);
                    enemies[enemyCounter].poisoned(true);
                    enemyTurn();
                    break;
                case 2:
                    if(corona.hasItem("health")){
                        changePhase(3);
                        removeHealth(corona, enemies[enemyCounter], 1.5, false);
                        corona.setItems("health", corona.getQuantity("health") - 1);
                        itemAnimation(true, 3);
                        enemyTurn();
                    }
                    //when its the items menu
                    break;
                default:
                    break;

            }
        }

        public void Button4Click(object sender, EventArgs e)
        {
            switch (phase)
            {
                case 0:

                    //when its the main menu

                    break;
                case 1:

                    //when its the attack menu
                    changePhase(0);
                    break;
                case 2:

                    //when its the items menu
                    changePhase(0);
                    break;
                default:
                    break;
            }
        }

        public void Button1Hover(object sender, EventArgs e)
        {
            switch (phase)
            {
                case 0:
                    Btn1.BackgroundImage = new Bitmap(Directory.GetCurrentDirectory() + @"\..\..\..\images\AttackBTNRed.png");
                    break;
                case 1:
                    Btn1.BackgroundImage = new Bitmap(Directory.GetCurrentDirectory() + @"\..\..\..\images\HeavyBreathingBTNRed.png");
                    break;
                case 2:
                    if (corona.hasItem("mask"))
                    {
                        Btn1.BackgroundImage = new Bitmap(Directory.GetCurrentDirectory() + @"\..\..\..\images\FaceMaskBTNGreen.png");
                    }
                    else
                    {
                        Btn1.BackgroundImage = new Bitmap(Directory.GetCurrentDirectory() + @"\..\..\..\images\FaceMaskBTNRed.png");
                    }
                    break;
                default:
                    break;
            }
        }
        public void Button1Leave(object sender, EventArgs e)
        {
            switch (phase)
            {
                case 0:
                    Btn1.BackgroundImage = new Bitmap(Directory.GetCurrentDirectory() + @"\..\..\..\images\AttackBTN.png");
                    break;
                case 1:
                    Btn1.BackgroundImage = new Bitmap(Directory.GetCurrentDirectory() + @"\..\..\..\images\HeavyBreathingBTN.png");
                    break;
                case 2:
                    Btn1.BackgroundImage = new Bitmap(Directory.GetCurrentDirectory() + @"\..\..\..\images\FaceMaskBTN.png");
                    break;
                default:
                    break;
            }
        }
        public void Button2Hover(object sender, EventArgs e)
        {
            switch (phase)
            {
                case 0:
                    Btn2.BackgroundImage = new Bitmap(Directory.GetCurrentDirectory() + @"\..\..\..\images\ItemBTNRed.png");
                    break;
                case 1:
                    Btn2.BackgroundImage = new Bitmap(Directory.GetCurrentDirectory() + @"\..\..\..\images\FakeNewsBTNRed.png");
                    break;
                case 2:
                    if (corona.hasItem("handSan"))
                    {
                        Btn2.BackgroundImage = new Bitmap(Directory.GetCurrentDirectory() + @"\..\..\..\images\HanSanBTNGreen.png");
                    }
                    else
                    {
                        Btn2.BackgroundImage = new Bitmap(Directory.GetCurrentDirectory() + @"\..\..\..\images\HanSanBTNRed.png");
                    }
                    break;
                default:
                    break;
            }
        }
        public void Button2Leave(object sender, EventArgs e)
        {
            switch (phase)
            {
                case 0:
                    Btn2.BackgroundImage = new Bitmap(Directory.GetCurrentDirectory() + @"\..\..\..\images\ItemBTN.png");
                    break;
                case 1:
                    Btn2.BackgroundImage = new Bitmap(Directory.GetCurrentDirectory() + @"\..\..\..\images\FakeNewsBTN.png");
                    break;
                case 2:
                    Btn2.BackgroundImage = new Bitmap(Directory.GetCurrentDirectory() + @"\..\..\..\images\HanSanBTN.png");
                    break;
                default:
                    break;
            }
        }
        public void Button3Hover(object sender, EventArgs e)
        {
            switch (phase)
            {
                case 1:
                    Btn3.BackgroundImage = new Bitmap(Directory.GetCurrentDirectory() + @"\..\..\..\images\ConspiracyTheoryRed.png");
                    break;
                case 2:
                    if (corona.hasItem("health"))
                    {
                        Btn3.BackgroundImage = new Bitmap(Directory.GetCurrentDirectory() + @"\..\..\..\images\HealthCareBTNGreen.png");
                    }
                    else
                    {
                        Btn3.BackgroundImage = new Bitmap(Directory.GetCurrentDirectory() + @"\..\..\..\images\HealthCareBTNRed.png");
                    }
                    break;
                default:
                    break;
            }
        }
        public void Button3Leave(object sender, EventArgs e)
        {
            switch (phase)
            {
                case 1:
                    Btn3.BackgroundImage = new Bitmap(Directory.GetCurrentDirectory() + @"\..\..\..\images\ConspiracyTheory.png");
                    break;
                case 2:
                    Btn3.BackgroundImage = new Bitmap(Directory.GetCurrentDirectory() + @"\..\..\..\images\HealthCareBTN.png");
                    break;
                default:
                    break;
            }
        }
        public void Button4Hover(object sender, EventArgs e)
        {
            switch (phase)
            {
                case 1:
                    Btn4.BackgroundImage = new Bitmap(Directory.GetCurrentDirectory() + @"\..\..\..\images\BackBTNRed.png");
                    break;
                case 2:
                    Btn4.BackgroundImage = new Bitmap(Directory.GetCurrentDirectory() + @"\..\..\..\images\BackBTNRed.png");
                    break;
                default:
                    break;
            }
        }
        public void Button4Leave(object sender, EventArgs e)
        {
            switch (phase)
            {
                case 1:
                    Btn4.BackgroundImage = new Bitmap(Directory.GetCurrentDirectory() + @"\..\..\..\images\BackBTN.png");
                    break;
                case 2:
                    Btn4.BackgroundImage = new Bitmap(Directory.GetCurrentDirectory() + @"\..\..\..\images\BackBTN.png");
                    break;
                default:
                    break;
            }
        }

        public void changePhase(int newPhase)
        {
            phase = newPhase;
            String filePath = Directory.GetCurrentDirectory() + @"\..\..\..\images\";

            switch (phase)
            {
                case 0:
                    Btn1.BackgroundImage = new Bitmap(filePath + "AttackBTN.png");
                    Btn2.BackgroundImage = new Bitmap(filePath + "ItemBTN.png");
                    Btn3.BackgroundImage = new Bitmap(filePath + "null.png");
                    Btn4.BackgroundImage = new Bitmap(filePath + "null.png");

                    break;

                case 1:
                    //attack names + back ??
                    Btn1.BackgroundImage = new Bitmap(filePath + "HeavyBreathingBTN.png");
                    Btn2.BackgroundImage = new Bitmap(filePath + "FakeNewsBTN.png");
                    Btn3.BackgroundImage = new Bitmap(filePath + "ConspiracyTheory.png");
                    Btn4.BackgroundImage = new Bitmap(filePath + "BackBTN.png");

                    break;

                case 2:
                    //item names + back :)
                    Btn1.BackgroundImage = new Bitmap(filePath + "FaceMaskBTN.png");
                    Btn2.BackgroundImage = new Bitmap(filePath + "HanSanBTN.png");
                    Btn3.BackgroundImage = new Bitmap(filePath + "HealthCareBTN.png");
                    Btn4.BackgroundImage = new Bitmap(filePath + "BackBTN.png");

                    break;

                case 3:
                    Btn1.BackgroundImage = new Bitmap(filePath + "null.png");
                    Btn2.BackgroundImage = new Bitmap(filePath + "null.png");
                    Btn3.BackgroundImage = new Bitmap(filePath + "null.png");
                    Btn4.BackgroundImage = new Bitmap(filePath + "null.png");

                    break;

                default:
                    break;

            }
            Btn1.Refresh();
            Btn2.Refresh();
            Btn3.Refresh();
            Btn4.Refresh();
            NOP(0.1);
        }

        public void createPlayer()
        {
            corona = new Player();
            corona.setMaxHealth(10);
            corona.setPower(3);
            corona.setPanel(CoronaCharacter);
            corona.changeUs();
        }

        public void enemyTurn()
        {
            if (enemies[enemyCounter].getHealth() < 1)
            {
                if(enemyCounter > 8)
                {
                    //show win screen
                    showEndScreen();
                }
                powerInherit(corona, enemies[enemyCounter]);
                enemyCounter++;
                setCharacter();
                setBars();
                vsBanner();

            }
            else if (enemies[enemyCounter].getHealth() < (enemies[enemyCounter].getMaxHealth() / 2))
            {
                if (enemies[enemyCounter].hasItem("handSan") && useItem())
                {
                    //use handSan (+ health)
                    enemies[enemyCounter].addToHealth();
                    enemies[enemyCounter].setItems("handSan", enemies[enemyCounter].getQuantity("handSan") - 1);
                    itemAnimation(false, 2);
                }
                else
                {
                    //use regular attack
                    removeHealth(enemies[enemyCounter], corona, 1, false);
                }
            } 
            else if (enemies[enemyCounter].hasItem("health"))
            {
                if (useItem())
                {
                    //use healthcare item
                    removeHealth(enemies[enemyCounter], corona, 1.5, false);
                    enemies[enemyCounter].setItems("health", enemies[enemyCounter].getQuantity("health") - 1);
                    itemAnimation(false, 3);
                }
                else
                {
                    //regular attack
                    removeHealth(enemies[enemyCounter], corona, 1, false);
                }
            }
            else if (enemies[enemyCounter].hasItem("mask"))
            {
                if (useItem())
                {
                    //use mask item (+ defense)
                    enemies[enemyCounter].setDefence(50);
                    enemies[enemyCounter].setItems("mask", enemies[enemyCounter].getQuantity("mask") - 1);
                    enemyStartingTurn = 3;
                    itemAnimation(false, 1);
                }
                else
                {
                    //regular attack
                    removeHealth(enemies[enemyCounter], corona, 1, false);
                }
            }
            else
            {
                //regular attack
                removeHealth(enemies[enemyCounter], corona, 1, false);
            }

            if (enemyStartingTurn != 0)
            {
                enemyStartingTurn--;
            }
            else
            {
                enemies[enemyCounter].setDefence(100);
            }

            endTurn();

        }

        public bool useItem()
        {
            Random rnd = new Random();
            int num = rnd.Next(1, 100);

            if (num % 2 == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void showEndScreen()
        {
            endScreen.Size = new Size(this.Width, this.Height);
            endScreen.Location = new Point(0, 0);
            endScreen.BackColor = Color.HotPink;
            endScreen.Visible = true;
        }

        public void showLoseScreen()
        {
            loseScreen.Size = new Size(this.Width, this.Height);
            loseScreen.Location = new Point(0, 0);
            loseScreen.BackColor = Color.HotPink;
            loseScreen.Visible = true;
        }

        //1 = mask, 2 = hanSan, 3 = cross
        public void itemAnimation(bool us, int item)
        {
            
            itemPanel.Size = new Size(this.Width / 5, this.Height / 5);
            if (item == 1)
            {
                itemPanel.BackgroundImage = new Bitmap(Directory.GetCurrentDirectory() + @"\..\..\..\images\Mask.png");
            }
            else if (item == 2)
            {
                itemPanel.BackgroundImage = new Bitmap(Directory.GetCurrentDirectory() + @"\..\..\..\images\HandSan.png");
                if (us)
                {
                    int tempInt = (lineLength / corona.getMaxHealth()) * corona.getHealth();
                    if(tempInt < lineLength)
                    {
                        tempInt = lineLength;
                    }
                    HPLine.Size = new Size(tempInt, HPLine.Size.Height);
                    HPLine.Refresh();
                }
                else
                {
                    int tempInt = (lineLength / enemies[enemyCounter].getMaxHealth()) * enemies[enemyCounter].getHealth();
                    if (tempInt < lineLength)
                    {
                        tempInt = lineLength;
                    }
                    EnemyHPLine.Size = new Size(tempInt, HPLine.Size.Height);
                    EnemyHPLine.Refresh();
                }
            }
            else
            {
                itemPanel.BackgroundImage = new Bitmap(Directory.GetCurrentDirectory() + @"\..\..\..\images\Cross.png");
            }
            if (us)
            {
                itemPanel.Location = new Point(this.Width / 2 - itemPanel.Size.Width, this.Height / 2 + itemPanel.Size.Height/2);
            }
            else
            {
                itemPanel.Location = new Point(this.Width / 2 + itemPanel.Size.Width, this.Height);
            }
            itemPanel.Visible = true;
            itemPanel.Refresh();
            NOP(0.8);
            itemPanel.Visible = false;
            itemPanel.Refresh();
            CoronaCharacter.Refresh();
        }

        public void endTurn()
        {
            if (corona.isPoisoned())
            {
                corona.reduceHealth(corona.getMaxHealth() / 5);
                damamgeAnimation(CoronaCharacter);
                healthBarDecrease(corona, HPLine);
            }
            if (enemies[enemyCounter].isPoisoned())
            {
                enemies[enemyCounter].reduceHealth(enemies[enemyCounter].getMaxHealth() / 5);
                damamgeAnimation(EnemyCharacter);
                healthBarDecrease(enemies[enemyCounter], EnemyHPLine);
            }
            if (corona.getHealth() < 1)
            {
                //show lose screen
                showLoseScreen();
                NOP(10);
                Application.Exit();
            }
            else
            {
                changePhase(0);
                if (playerStartingTurn != 0)
                {
                    playerStartingTurn--;
                }
                else
                {
                    corona.setDefence(100);
                }
            }
        }

        private void readFile()
        {
            String filepath = Directory.GetCurrentDirectory() + @"\..\..\countrys.txt";

            if (File.Exists(filepath))
            {
                StreamReader s = File.OpenText(filepath);
                String read = null;

                int counter = 0;
                int index = 0;

                while ((read = s.ReadLine()) != null)
                {
                    counter++;
                    if (counter % 4 == 1)
                    {
                        enemies[index] = new Country();
                        enemies[index].setName(read);
                    }
                    else if (counter % 4 == 2)
                    {
                        enemies[index].setMaxHealth(System.Convert.ToInt32(read));

                    }
                    else if (counter % 4 == 3)
                    {
                        enemies[index].setPower(System.Convert.ToInt32(read));
                    }
                    else if (counter % 4 == 0)
                    {
                        enemies[index].setImg(read);
                        enemies[index].setPanel(EnemyCharacter);
                        index++;
                    }
                    else
                    {
                        Console.WriteLine("??????????? WHY ?????????");
                    }

                }
                s.Close();
            }
        }

        public void createEnemy()
        {
            enemies = new Country[9];
            readFile();
        }

        public void removeHealth(Character attacker, Character defender, Double multiplier = 1, bool forgetDefence = false)
        {
            int reduceBy;
            Console.WriteLine("Power: " + attacker.getPower() + " multiplier: " + multiplier + " defence: " + defender.getDefence());

            if (!forgetDefence)
            {
                reduceBy = Convert.ToInt32(attacker.getPower() * multiplier * (defender.getDefence() / 100));
            }
            else
            {
                reduceBy = Convert.ToInt32(attacker.getPower() * multiplier);
            }

            defender.reduceHealth(reduceBy);

            if (defender.getUs())
            {
                attackAnimationEnemy();
            }
            else
            {
                attackAnimationCorona();
            }
            damamgeAnimation(defender.getPanel());
            if (defender.getUs())
            {
                healthBarDecrease(defender, HPLine);
            }
            else
            {
                healthBarDecrease(defender, EnemyHPLine);
            }
        }

        public CoromonUI()
        {
            InitializeComponent();
        }

        public void powerInherit(Character receiving, Character giving)
        {
            receiving.addHealth(giving.getMaxHealth());
            receiving.addPower(giving.getPower());
            receiving.findItem("mask").addQuantity(giving.getQuantity("mask"));
            receiving.findItem("handSan").addQuantity(giving.getQuantity("handSan"));
            receiving.findItem("health").addQuantity(giving.getQuantity("health"));
        }

        public void attackAnimationCorona()
        {
            int loopLength = 21;
            int tempX = CoronaCharacter.Location.X;
            int tempY = CoronaCharacter.Location.Y;
            for (int i = 0; i < loopLength; i++)
            {
                tempY -= 2;
                tempX += 3;
                CoronaCharacter.Location = new Point(tempX, tempY);
                NOP(0.01);
                CoronaCharacter.Refresh();
            }
            for (int i = 0; i < loopLength / 3; i++)
            {
                tempY += 6;
                tempX -= 9;
                CoronaCharacter.Location = new Point(tempX, tempY);
                NOP(0.01);
                CoronaCharacter.Refresh();
            }
        }

        public void attackAnimationEnemy()
        {
            int loopLength = 21;
            int tempX = EnemyCharacter.Location.X;
            int tempY = EnemyCharacter.Location.Y;
            for (int i = 0; i < loopLength; i++)
            {
                tempY += 2;
                tempX -= 3;
                EnemyCharacter.Location = new Point(tempX, tempY);
                NOP(0.01);
                EnemyCharacter.Refresh();
                SelectionBarFrame.Refresh();
            }
            for (int i = 0; i < loopLength / 3; i++)
            {
                tempY -= 6;
                tempX += 9;
                EnemyCharacter.Location = new Point(tempX, tempY);
                NOP(0.01);
                EnemyCharacter.Refresh();
                SelectionBarFrame.Refresh();
            }
        }

        public void damamgeAnimation(Panel animator)
        {
            int tempY;
            int upBy = 40;
            for (int i = 0; i < upBy / 4; i++)
            {
                tempY = animator.Location.Y - 8;
                animator.Location = new Point(animator.Location.X, tempY);
                NOP(0.001);
                animator.Refresh();
            }
            for (int i = 0; i < upBy / 2; i++)
            {
                tempY = animator.Location.Y + 4;
                animator.Location = new Point(animator.Location.X, tempY);
                NOP(0.001);
                animator.Refresh();
            }
            EnemyHPBar.Refresh();
            EnemyHPLine.Refresh();
            animeHurt.Size = new Size(animator.Size.Width / 5, animator.Size.Height / 5);
            int animeX = animator.Location.X + animator.Size.Width - animeHurt.Size.Width / 2;
            int animeY = animator.Location.Y + animeHurt.Size.Height / 2;
            animeHurt.Location = new Point(animeX, animeY);
            animeHurt.Visible = true;
            animeHurt.Refresh();
            NOP(0.5);
            animeHurt.Visible = false;
            animeHurt.Refresh();
            animator.Refresh();
        }

        public void healthBarDecrease(Character tempCharacter, Panel theLine)
        {
            if (tempCharacter.getUs())
            {
                theLine.BackgroundImage = new Bitmap(Directory.GetCurrentDirectory() + @"\..\..\..\images\HealthLineRed.png");
            }
            else
            {
                theLine.BackgroundImage = new Bitmap(Directory.GetCurrentDirectory() + @"\..\..\..\images\EnemyHealthLineRed.png");
            }
            int target = (lineLength / tempCharacter.getMaxHealth()) * tempCharacter.getHealth();
            int reduction = theLine.Size.Width - target;
            if (theLine.Size.Width - reduction < 4)
            {
                reduction = theLine.Size.Width;
            }
            reduction /= 4;
            int theHeight = theLine.Size.Height;
            for (int i = 0; i < reduction; i++)
            {
                theLine.Size = new Size(theLine.Size.Width - 4, theHeight);
                theLine.Refresh();
                NOP(0.1);
                theLine.Refresh();
            }
            if (tempCharacter.getUs())
            {
                theLine.BackgroundImage = new Bitmap(Directory.GetCurrentDirectory() + @"\..\..\..\images\HealthLine.png");
            }
            else
            {
                theLine.BackgroundImage = new Bitmap(Directory.GetCurrentDirectory() + @"\..\..\..\images\EnemyHealthLine.png");
            }

        }

        //Stole this code from online as we remembered that pausing in c# was a pain in the booty 
        //(https://stackoverflow.com/questions/6254703/thread-sleep-for-less-than-1-millisecond)
        private static void NOP(double durationSeconds)
        {
            var durationTicks = Math.Round(durationSeconds * Stopwatch.Frequency);
            var sw = Stopwatch.StartNew();
            while (sw.ElapsedTicks < durationTicks)
            {
            }
        }

        private void vsBanner()
        {
            coronaVS.Size = new Size(this.Width / 4, this.Height / 8);
            coronaVS.Location = new Point(0, 0);
            enemyVS.BackgroundImage = new Bitmap(Directory.GetCurrentDirectory() + @"\..\..\..\images\" + enemies[enemyCounter].getName() + ".png");
            enemyVS.Size = new Size(this.Width / 6, this.Height / 8);
            enemyVS.Location = new Point(this.Width / 4, 0);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //set up background stuff
            createPlayer();
            createEnemy();
            //set UI 
            fullScreen();
            this.Visible = false;
            setBars();
            setCharacter();
            setSelectionBar();
            vsBanner();
            this.Visible = true;
            //Start game
            changePhase(0);

        }
    }
}
